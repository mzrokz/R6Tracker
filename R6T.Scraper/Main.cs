using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PuppeteerSharp;
using PropertyInfo = System.Reflection.PropertyInfo;
using R6T.Model;
using R6T.Model.ViewModels;

namespace R6T.Scraper
{
    public class Main
    {
        private IWebDriver browser;

        public Main()
        {
        }

        public async Task<RevisionInfo> FetchChromium(BrowserFetcherOptions options = null)
        {
            if (options != null)
            {
                return await new BrowserFetcher(options).DownloadAsync(BrowserFetcher.DefaultRevision);
            }

            return await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
        }

        public void Start()
        {
            try
            {
                InitSelenium();

                using (var r6Model = new R6TrackerEntities())
                {
                    var lstPlayers = r6Model.Players.Where(w => w.IsActive.Value).ToList();
                    foreach (var player in lstPlayers)
                    {
                        ScrapeUserData(player);
                    }
                }

                // var player = new Player();
                // player.Alias = "Soldier_1st";
                // ScrapeUserData(player);
            }
            catch (Exception e)
            {
                browser.Close();
                Console.WriteLine(e);
                throw;
            }
        }

        public void InitSelenium()
        {
            browser = new ChromeDriver();
        }

        public bool ScrapeUserData(Player oPlayer)
        {
            // Create a new page and go to Bing Maps
            // Page page = await browser.NewPageAsync();
            try
            {
                var arrWaitUntil = new WaitUntilNavigation[] {WaitUntilNavigation.DOMContentLoaded};
                var timeout = 1000 * 60 * 5;
                browser.Url = $"https://r6.tracker.network/profile/pc/{oPlayer.Alias}";
                // await page.GoToAsync($"https://r6.tracker.network/profile/pc/{oPlayer.Alias}", timeout: timeout, arrWaitUntil);
                // string html = await page.GetContentAsync();
                string html = browser.PageSource;

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");

                if (DoesPlayerExists(htmlDoc))
                {
                    if (DoesNewDataExists(oPlayer, htmlDoc))
                    {
                        ExtractStats(htmlDoc, oPlayer, typeof(GameStat));
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                // await page.CloseAsync();
            }

            return true;
        }

        public bool DoesPlayerExists(HtmlDocument htmlDoc)
        {
            var playerName = htmlDoc.DocumentNode.SelectSingleNode("//h1[@class='trn-profile-header__name']//span[1]");
            if (playerName != null && !String.IsNullOrEmpty(playerName.InnerText))
            {
                return true;
            }

            return false;
        }

        public bool DoesNewDataExists(Player oPlayer, HtmlDocument htmlDoc)
        {
            var oGeneralGameStat = new GameStat();
            oGeneralGameStat.MatchesPlayed = htmlDoc.DocumentNode
                .SelectSingleNode("//div[@data-stat='PVPMatchesPlayed']").InnerHtml.ToInt32();
            using (var r6Model = new R6TrackerEntities())
            {
                var oGameStat = r6Model.GameStats.Where(w => w.PlayerId == oPlayer.PlayerId && w.MatchTypeId == 1)
                    .OrderByDescending(o => o.CreatedDate).FirstOrDefault();
                if (oGameStat.MatchesPlayed < oGeneralGameStat.MatchesPlayed)
                {
                    return true;
                }
            }

            return false;
        }

        public void CheckDataType(Type type, PropertyInfo prop, object instance, string data)
        {
            if (typeof(int?).IsAssignableFrom(prop.PropertyType))
            {
                prop.SetValue(instance, data.ToInt32(), null);
            }
            else if (typeof(decimal?).IsAssignableFrom(prop.PropertyType))
            {
                prop.SetValue(instance, data.ToDecimal(), null);
            }
            else
            {
                prop.SetValue(instance, data, null);
            }
        }

        public void ExtractStats(HtmlDocument htmlDoc, Player oPlayer, Type type)
        {
            var dbi = type.GetCustomAttributes(false).First() as AttachXPath;

            for (var i = 0; i < dbi?.StatType.Length; i++)
            {
                var stat = dbi.StatType[i];
                var instance = Activator.CreateInstance(type);
                foreach (var property in type.GetProperties())
                {
                    var attr = property.GetCustomAttributes(false);

                    var propertyDbi = attr.Length > 0 ? attr.First() as AttachXPath : null;
                    if (propertyDbi?.XPath.Length > 0)
                    {
                        var xpath = propertyDbi.XPath.ElementAtOrDefault(i);
                        if (xpath != "")
                        {
                            var data = htmlDoc.DocumentNode.SelectSingleNode(propertyDbi.XPath[i]).InnerHtml;
                            CheckDataType(type, property, instance, data);
                        }
                    }
                }

                var oGameStat = instance as GameStat;
                oGameStat.GameStatId = Guid.NewGuid();
                oGameStat.PlayerId = oPlayer.PlayerId;
                oGameStat.MatchTypeId = (int) stat;
                oGameStat.CreatedDate = DateTime.Now;

                using (var r6Model = new R6TrackerEntities())
                {
                    r6Model.GameStats.Add(oGameStat);
                    r6Model.SaveChanges();
                }
            }
        }
    }
}