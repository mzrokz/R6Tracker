using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private ScraperFunctions _scraperFunction;
        public Main()
        {
            MapperProfile.CreateConfiguration();
            _scraperFunction = new ScraperFunctions();
        }

        public async Task<RevisionInfo> FetchChromium(BrowserFetcherOptions options = null)
        {
            if (options != null)
            {
                return await new BrowserFetcher(options).DownloadAsync(BrowserFetcher.DefaultRevision);
            }

            return await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
        }

        public async void Start()
        {
            try
            {
                InitSelenium();

                using (var r6Model = new R6TrackerEntities())
                {
                    var lstPlayers = r6Model.Players.Where(w => w.IsActive.Value).ToList();
                    foreach (var player in lstPlayers)
                    {
                        await ScrapeUserData(player);
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

        public void InitSelenium(string path = "")
        {
            ChromeOptions options = new ChromeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Eager;
            options.AddArgument("--headless");
            if (!String.IsNullOrEmpty(path))
            {
                browser = new ChromeDriver(path, options);
            }
            else
            {
                browser = new ChromeDriver(options);
            }
        }

        public async Task<bool> ScrapeUserData(Player oPlayer, string pathAppData = "")
        {
            // Create a new page and go to Bing Maps
            // Page page = await browser.NewPageAsync();
            try
            {
                if (String.IsNullOrEmpty(oPlayer.Url))
                {
                    return false;
                }

                browser.Url = oPlayer.Url;
                //oScraperFunction.MonkeyPatchInterval(browser);

                string html = browser.PageSource;
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                //var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");

                if (DoesPlayerExists(htmlDoc, oPlayer))
                {
                    ExtractRank(htmlDoc, oPlayer, pathAppData);
                    if (DoesNewDataExists(oPlayer, htmlDoc))
                    {
                        await Task.Run(() => { ExtractStats(htmlDoc, oPlayer, typeof(GameStatsVm)); });
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
                browser.Quit();
            }

            return true;
        }

        public bool DoesPlayerExists(HtmlDocument htmlDoc, Player oPlayer)
        {
            var playerName = htmlDoc.DocumentNode.SelectSingleNode("//h1[@class='trn-profile-header__title']//span[1]");
            if (playerName != null && !String.IsNullOrEmpty(playerName.InnerText))
            {
                if (oPlayer != null && oPlayer.LatestAlias != playerName.InnerText)
                {
                    using (var r6Model = new R6TrackerEntities())
                    {
                        var player = r6Model.Players.SingleOrDefault(s => s.PlayerId == oPlayer.PlayerId);
                        if (player != null)
                        {
                            player.LatestAlias = playerName.InnerText;
                            r6Model.SaveChanges();
                        }
                    }
                }
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
                if (oGameStat == null || oGameStat.MatchesPlayed < oGeneralGameStat.MatchesPlayed)
                {
                    return true;
                }
            }

            return false;
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
                        if (!string.IsNullOrEmpty(xpath))
                        {
                            if (attr.Length == 2)
                            {
                                var eValue = (attr[1] as ElementValue)?.Value;
                                if (eValue != null)
                                {
                                    if (eValue == "src")
                                    {
                                        var srcNode = htmlDoc.DocumentNode.SelectSingleNode(xpath);
                                        if (srcNode != null)
                                        {
                                            if (srcNode.Attributes.Any(a => a.Name == "src"))
                                            {
                                                var data = srcNode.Attributes["src"].Value;
                                                _scraperFunction.CheckDataType(type, property, instance, data);
                                            }
                                            else if (srcNode.Attributes.Any(a => a.Name == "xlink:href"))
                                            {
                                                var data = srcNode.Attributes["xlink:href"].Value;
                                                _scraperFunction.CheckDataType(type, property, instance, data);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var data = htmlDoc.DocumentNode.SelectSingleNode(xpath).InnerHtml;
                                _scraperFunction.CheckDataType(type, property, instance, data);
                            }
                        }
                    }
                }

                var oGameStatVm = instance as GameStatsVm;

                var mapper = MapperProfile.Configuration.CreateMapper();

                var oGameStat = mapper.Map<GameStat>(oGameStatVm);
                oGameStat.GameStatId = Guid.NewGuid();
                oGameStat.PlayerId = oPlayer.PlayerId;
                oGameStat.MatchTypeId = (int)stat;
                oGameStat.CreatedDate = DateTime.Now;

                using (var r6Model = new R6TrackerEntities())
                {
                    r6Model.GameStats.Add(oGameStat);
                    r6Model.SaveChanges();
                }
            }
        }

        public bool ExtractRank(HtmlDocument htmlDoc, Player oPlayer, string pathAppData)
        {
            var imgRankUrl = "https://trackercdn.com/cdn/r6.tracker.network/ranks/svg/hd-rank0.svg";
            try
            {
                var imgNode = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[4]/div[2]/div[3]/div[2]/div[1]/div[1]/div[2]/div[1]/img");
                if (imgNode != null && imgNode.Attributes.Contains("src"))
                {
                    imgRankUrl = imgNode.Attributes["src"].Value;

                    if (!String.IsNullOrEmpty(imgRankUrl))
                    {
                        if (!imgRankUrl.Contains("trackercdn.com") && !imgRankUrl.Contains("r6.tracker.network"))
                        {
                            imgRankUrl = "http://r6.tracker.network" + imgRankUrl;
                        }
                    }
                }

                using (var r6Model = new R6TrackerEntities())
                {
                    var player = r6Model.Players.SingleOrDefault(s => s.PlayerId == oPlayer.PlayerId);
                    if (player != null)
                    {
                        player.RankUrl = imgRankUrl;
                        r6Model.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public Player GetPlayer(string alias)
        {
            try
            {
                if (String.IsNullOrEmpty(alias))
                {
                    return null;
                }

                browser.Url = $"https://r6.tracker.network/profile/pc/{alias}";
                //oScraperFunction.MonkeyPatchInterval(browser);

                string html = browser.PageSource;
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                if (DoesPlayerExists(htmlDoc, null))
                {
                    Player oPlayer = new Player();
                    var url = htmlDoc.DocumentNode
                        .SelectSingleNode("//input[@id='perm-link']").Attributes["value"].Value;
                    oPlayer.Url = url;
                    return oPlayer;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                browser.Quit();
            }

            return null;
        }
    }
}