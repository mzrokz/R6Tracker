using AutoMapper;
using R6T.Model;
using R6T.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using System.Web.UI.WebControls;
using PuppeteerSharp;
using R6T.Scraper;
using R6T.WebApi.App_Start;

namespace R6T.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlayerController : ApiController
    {
        private string _pathAppData;

        public PlayerController()
        {
            _pathAppData = HttpContext.Current.Server.MapPath("~/chrome-driver/");
        }

        [HttpGet]
        public IHttpActionResult GetPlayers()
        {
            Logger.LogInfo("GetPlayers");
            using (var oEntity = new R6TrackerEntities())
            {
                var players =
                    oEntity.Players
                        .OrderByDescending(o => o.IsActive)
                        .ThenBy(o => o.SortOrder)
                        .ThenBy(o => o.PlayerName)
                    .Select(s =>
                      new PlayerVm()
                      {
                          PlayerId = s.PlayerId,
                          Alias = s.Alias,
                          PlayerName = s.PlayerName,
                          IsActive = s.IsActive,
                          Url = s.Url,
                          RankUrl = s.RankUrl,
                          SortOrder = s.SortOrder,
                          LatestAlias = s.LatestAlias
                      }).ToList();
                return Ok(players);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetGameStats(Guid playerId)
        {
            using (var oEntity = new R6TrackerEntities())
            {
                //var gameStats = oEntity.GameStats.Where(w => w.PlayerId == playerId).ToList();

                var gameStats = oEntity.GetPlayerGameStats(playerId).ToList();

                var mapper = MapperProfile.Configuration.CreateMapper();
                var lstGameStatsVm = mapper.Map<List<GameStatsVm>>(gameStats);

                if (lstGameStatsVm.Any())
                {
                    var maxLatestRecord = lstGameStatsVm.Max(m => m.LatestRecord);
                    for (int i = 1; i <= maxLatestRecord; i++)
                    {
                        var records = lstGameStatsVm.Where(w => w.LatestRecord == i).ToList();
                        foreach (var record in records)
                        {
                            var nextRecord = lstGameStatsVm.SingleOrDefault(s =>
                                s.LatestRecord == (record.LatestRecord + 1) && s.MatchTypeId == record.MatchTypeId);
                            if (nextRecord != null)
                            {
                                record.Difference = new GameStatsVm();
                                record.Difference.MatchesPlayed = record.MatchesPlayed - nextRecord.MatchesPlayed;
                                record.Difference.Kills = record.Kills - nextRecord.Kills;
                                record.Difference.Deaths = record.Deaths - nextRecord.Deaths;
                                record.Difference.Wins = record.Wins - nextRecord.Wins;
                                record.Difference.Losses = record.Losses - nextRecord.Losses;
                            }
                        }
                    }
                }

                return Ok(lstGameStatsVm);
            }
        }

        [HttpPost, Route("api/Player/SyncPlayerData")]
        public async Task<IHttpActionResult> SyncPlayerData(Player oPlayer)
        {
            Logger.LogInfo($"SyncPlayerData - Starting for {oPlayer.PlayerName}");

            if (oPlayer != null)
            {
                try
                {
                    Logger.LogInfo("SyncPlayerData - InitSelenium");
                    var oScraper = new Main();
                    oScraper.InitSelenium(_pathAppData);

                    //await oScraper.FetchChromium(new BrowserFetcherOptions(
                    //{
                    //    Path = Server.MapPath
                    //}));
                    Logger.LogInfo($"Start - SyncPlayerData - ScrapeUserData for {oPlayer.PlayerName}");
                    var result = await oScraper.ScrapeUserData(oPlayer, _pathAppData);
                    Logger.LogInfo($"End - SyncPlayerData - ScrapeUserData for {oPlayer.PlayerName}");
                    if (!result)
                    {
                        return BadRequest("Not Synced");
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"SyncPlayerData - {oPlayer.PlayerName} | {ex.Message}");
                    Console.WriteLine(ex);
                    return BadRequest(ex.Message);
                }
            }

            return Ok();
        }

        [HttpPost, Route("api/Player/SetActive")]
        public IHttpActionResult SetActive(Player oPlayer)
        {
            if (oPlayer != null)
            {
                try
                {
                    using (var oEntity = new R6TrackerEntities())
                    {
                        var player = oEntity.Players.SingleOrDefault(s => s.PlayerId == oPlayer.PlayerId);
                        if (player != null)
                        {
                            player.IsActive = !player.IsActive;
                            oEntity.SaveChanges();
                        }
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogError($"SetActive - {oPlayer.PlayerName} | {ex.Message}");
                    Console.WriteLine(ex);
                    return BadRequest(ex.Message);
                }
            }

            return Ok();
        }

        [HttpPost, Route("api/Player/AddPlayer")]
        public IHttpActionResult AddPlayer(Player oPlayer)
        {
            Logger.LogInfo($"Start - AddPlayer - {oPlayer.PlayerName}");
            if (oPlayer != null)
            {
                try
                {
                    var oEntity = new R6TrackerEntities();
                    var doesPlayerExists = oEntity.Players.Any(a => a.Alias.ToLower() == oPlayer.Alias.ToLower());
                    if (doesPlayerExists)
                    {
                        Logger.LogError($"AddPlayer - {oPlayer.PlayerName} | Player already exists");
                        return BadRequest("Player already exists");
                    }

                    var oScraper = new Main();
                    oScraper.InitSelenium(_pathAppData);
                    var player = oScraper.GetPlayer(oPlayer.Alias);

                    if (player != null)
                    {
                        oPlayer.PlayerId = Guid.NewGuid();
                        oPlayer.Url = player.Url;
                        oPlayer.IsActive = true;
                        oPlayer.SortOrder = 1;
                        oEntity.Players.Add(oPlayer);
                        oEntity.SaveChanges();
                        return Ok(oPlayer);
                    }
                    else
                    {
                        return BadRequest("Alias doesn't exist");
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"AddPlayer - {oPlayer.PlayerName} | {ex.Message}");
                    Console.WriteLine(ex);
                    return BadRequest(ex.Message);
                }
            }
            Logger.LogError($"End - AddPlayer - {oPlayer.PlayerName}");
            return Ok();
        }

        [HttpGet, Route("api/Player/GetPlayer")]
        public IHttpActionResult GetPlayer(Guid playerId)
        {
            Player oPlayer = new Player();
            using (var oEntity = new R6TrackerEntities())
            {
                var player = oEntity.Players.SingleOrDefault(s => s.PlayerId == playerId);
                if (player != null)
                {
                    Logger.LogInfo($"GetPlayer - Fetched Player from DB - {player.PlayerName}");
                    oPlayer.PlayerId = player.PlayerId;
                    oPlayer.PlayerName = player.PlayerName;
                    oPlayer.Alias = player.Alias;
                    oPlayer.IsActive = player.IsActive;
                    oPlayer.Url = player.Url;
                    oPlayer.RankUrl = player.RankUrl;
                    oPlayer.LatestAlias = player.LatestAlias;
                }
                return Ok(oPlayer);
            }
        }

        [HttpPost, Route("api/Player/SetSort")]
        public IHttpActionResult SetSort(PlayerVm oPlayer)
        {
            if (oPlayer != null)
            {
                try
                {
                    var oEntity = new R6TrackerEntities();
                    if (oPlayer.SortType == "up")
                    {
                        if (oPlayer.SortOrder > 1)
                        {
                            var players = oEntity.Players.Where(w => w.SortOrder == oPlayer.SortOrder - 1);
                            foreach (var player in players)
                            {
                                player.SortOrder = player.SortOrder + 1;
                            }

                            oPlayer.SortOrder -= 1;
                            oEntity.Players.AddOrUpdate(oPlayer);
                            oEntity.SaveChanges();
                        }
                    }
                    else if (oPlayer.SortType == "down")
                    {
                        var maxSortOrder = oEntity.Players.Max(m => m.SortOrder);
                        if (maxSortOrder.HasValue && oPlayer.SortOrder < maxSortOrder.Value)
                        {
                            var players = oEntity.Players.Where(w => w.SortOrder == oPlayer.SortOrder + 1);
                            foreach (var player in players)
                            {
                                if (player.SortOrder > 1)
                                {
                                    player.SortOrder = player.SortOrder.Value - 1;
                                }
                            }

                            oPlayer.SortOrder += 1;
                            oEntity.Players.AddOrUpdate(oPlayer);
                            oEntity.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return BadRequest(ex.Message);
                }
            }
            return Ok();
        }
    }
}
