using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PuppeteerSharp;
using R6T.Model;
using R6T.Model.ViewModels;

namespace R6T.Scraper
{
    public class Main
    {
        private Browser browser;
        public Main()
        {

        }
        public async Task<RevisionInfo> FetchChromium()
        {
            return await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
        }

        public async void Start()
        {
            try
            {
                await InitPuppeteer();

                using (var r6Model = new R6TrackerEntities())
                {
                    var lstPlayers = r6Model.Players.Where(w => w.IsActive.Value).ToList();
                    foreach (var player in lstPlayers)
                    {
                        await ScrapeUserData(player);
                    }
                }
            }
            catch (Exception e)
            {
                await browser.CloseAsync();
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task InitPuppeteer()
        {
            await FetchChromium();
            // Create an instance of the browser and configure launch options
            browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
        }

        public async Task<Boolean> ScrapeUserData(Player oPlayer)
        {
            // Create a new page and go to Bing Maps
            Page page = await browser.NewPageAsync();
            await page.GoToAsync($"https://r6.tracker.network/profile/pc/{oPlayer.Alias}");
            string html = await page.GetContentAsync();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");

            ExtractGeneralStats(oPlayer, htmlDoc);
            ExtractRankedStats(oPlayer, htmlDoc);
            ExtractUnRankedStats(oPlayer, htmlDoc);
            ExtractCasualStats(oPlayer, htmlDoc);

            await page.CloseAsync();
            return true;
        }

        public void ExtractGeneralStats(Player oPlayer, HtmlDocument htmlDoc)
        {
            //----------------------------------------------- General Stats -----------------------------------------------
            var oGeneralGameStat = new GameStat();
            oGeneralGameStat.GameStatId = Guid.NewGuid();
            oGeneralGameStat.PlayerId = oPlayer.PlayerId;
            oGeneralGameStat.MatchTypeId = (int)EMatchType.General;
            oGeneralGameStat.PlayerLevel = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='trn-defstat__value']").InnerHtml.ToInt32();
            oGeneralGameStat.Kills = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='PVPKills']").InnerHtml.ToInt32();
            oGeneralGameStat.HeadshotPercent = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='PVPAccuracy']").InnerHtml;
            oGeneralGameStat.KD = htmlDoc.DocumentNode.SelectSingleNode("(//div[@data-stat='PVPKDRatio'])[2]").InnerHtml.ToDecimal();
            oGeneralGameStat.Deaths = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='PVPDeaths']").InnerHtml.ToInt32();
            oGeneralGameStat.Headshots = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='PVPHeadshots']").InnerHtml.ToInt32();
            oGeneralGameStat.Wins = htmlDoc.DocumentNode.SelectSingleNode("(//div[@data-stat='PVPMatchesWon'])[2]").InnerHtml.ToInt32();
            oGeneralGameStat.Losses = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='PVPMatchesLost']").InnerHtml.ToInt32();
            oGeneralGameStat.WinPercent = htmlDoc.DocumentNode.SelectSingleNode("(//div[@data-stat='PVPWLRatio'])[2]").InnerHtml;
            oGeneralGameStat.TimePlayed = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='PVPTimePlayed']").InnerHtml;
            oGeneralGameStat.MatchesPlayed = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='PVPMatchesPlayed']").InnerHtml.ToInt32();
            oGeneralGameStat.TotalXp = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='PVPTotalXp']").InnerHtml;
            oGeneralGameStat.MeleeKills = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='PVPMeleeKills']").InnerHtml.ToInt32();
            oGeneralGameStat.BlindKills = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='PVPBlindKills']").InnerHtml.ToInt32();
            oGeneralGameStat.CreatedDate = DateTime.Now;

            using (var r6Model = new R6TrackerEntities())
            {
                r6Model.GameStats.Add(oGeneralGameStat);
                r6Model.SaveChanges();
            }
        }

        public void ExtractRankedStats(Player oPlayer, HtmlDocument htmlDoc)
        {
            //----------------------------------------------- Ranked Stats -----------------------------------------------
            var oRankedGameStat = new GameStat();
            oRankedGameStat.GameStatId = Guid.NewGuid();
            oRankedGameStat.PlayerId = oPlayer.PlayerId;
            oRankedGameStat.MatchTypeId = (int)EMatchType.Ranked;
            oRankedGameStat.PlayerLevel = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='trn-defstat__value']").InnerHtml.ToInt32();
            oRankedGameStat.TimePlayed = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='RankedTimePlayed']").InnerHtml;
            oRankedGameStat.Wins = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='RankedWins']").InnerHtml.ToInt32();
            oRankedGameStat.Losses = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='RankedLosses']").InnerHtml.ToInt32();
            oRankedGameStat.MatchesPlayed = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='RankedMatches']").InnerHtml.ToInt32();
            oRankedGameStat.Deaths = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='RankedDeaths']").InnerHtml.ToInt32();
            oRankedGameStat.Kills = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='RankedKills']").InnerHtml.ToInt32();
            oRankedGameStat.WinPercent = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='RankedWLRatio']").InnerHtml;
            oRankedGameStat.KD = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='RankedKDRatio']").InnerHtml.ToDecimal();
            oRankedGameStat.KillPerMatch = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='RankedKillsPerMatch']").InnerHtml.ToDecimal();
            oRankedGameStat.KillPerMin = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='RankedKillsPerMinute']").InnerHtml.ToDecimal();
            oRankedGameStat.CreatedDate = DateTime.Now;

            using (var r6Model = new R6TrackerEntities())
            {
                r6Model.GameStats.Add(oRankedGameStat);
                r6Model.SaveChanges();
            }
        }

        public void ExtractUnRankedStats(Player oPlayer, HtmlDocument htmlDoc)
        {
            //----------------------------------------------- Un-Ranked Stats -----------------------------------------------
            var oUnrankedGameStat = new GameStat();
            oUnrankedGameStat.GameStatId = Guid.NewGuid();
            oUnrankedGameStat.PlayerId = oPlayer.PlayerId;
            oUnrankedGameStat.MatchTypeId = (int)EMatchType.Unranked;
            oUnrankedGameStat.PlayerLevel = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='trn-defstat__value']").InnerHtml.ToInt32();
            oUnrankedGameStat.TimePlayed = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='UnRankedTimePlayed']").InnerHtml;
            oUnrankedGameStat.Wins = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='UnRankedWins']").InnerHtml.ToInt32();
            oUnrankedGameStat.Losses = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='UnRankedlLosses']").InnerHtml.ToInt32();
            oUnrankedGameStat.MatchesPlayed = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='UnRankedMatches']").InnerHtml.ToInt32();
            oUnrankedGameStat.Deaths = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='UnRankedDeaths']").InnerHtml.ToInt32();
            oUnrankedGameStat.Kills = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='UnRankedKills']").InnerHtml.ToInt32();
            oUnrankedGameStat.WinPercent = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='UnRankedWLRatio']").InnerHtml;
            oUnrankedGameStat.KD = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='UnRankedKDRatio']").InnerHtml.ToDecimal();
            oUnrankedGameStat.KillPerMatch = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='UnRankedKillsPerMatch']").InnerHtml.ToDecimal();
            oUnrankedGameStat.KillPerMin = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='UnRankedKillsPerMinute']").InnerHtml.ToDecimal();
            oUnrankedGameStat.CreatedDate = DateTime.Now;

            using (var r6Model = new R6TrackerEntities())
            {
                r6Model.GameStats.Add(oUnrankedGameStat);
                r6Model.SaveChanges();
            }
        }

        public void ExtractCasualStats(Player oPlayer, HtmlDocument htmlDoc)
        {
            //----------------------------------------------- Casual Stats -----------------------------------------------
            var oQuickMatch = new GameStat();
            oQuickMatch.GameStatId = Guid.NewGuid();
            oQuickMatch.PlayerId = oPlayer.PlayerId;
            oQuickMatch.MatchTypeId = (int)EMatchType.Casual;
            oQuickMatch.PlayerLevel = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='trn-defstat__value']").InnerHtml.ToInt32();
            oQuickMatch.TimePlayed = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='CasualTimePlayed']").InnerHtml;
            oQuickMatch.Wins = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='CasualWins']").InnerHtml.ToInt32();
            oQuickMatch.Losses = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='CasualLosses']").InnerHtml.ToInt32();
            oQuickMatch.MatchesPlayed = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='CasualMatches']").InnerHtml.ToInt32();
            oQuickMatch.Deaths = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='CasualDeaths']").InnerHtml.ToInt32();
            oQuickMatch.Kills = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='CasualKills']").InnerHtml.ToInt32();
            oQuickMatch.WinPercent = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='CasualWLRatio']").InnerHtml;
            oQuickMatch.KD = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='CasualKDRatio']").InnerHtml.ToDecimal();
            oQuickMatch.KillPerMatch = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='CasualKillsPerMatch']").InnerHtml.ToDecimal();
            oQuickMatch.KillPerMin = htmlDoc.DocumentNode.SelectSingleNode("//div[@data-stat='CasualKillsPerMinute']").InnerHtml.ToDecimal();
            oQuickMatch.CreatedDate = DateTime.Now;

            using (var r6Model = new R6TrackerEntities())
            {
                r6Model.GameStats.Add(oQuickMatch);
                r6Model.SaveChanges();
            }
        }
    }
}
