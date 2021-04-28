using AutoMapper;
using R6T.Model;
using R6T.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using PuppeteerSharp;
using R6T.Scraper;

namespace R6T.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlayerController : ApiController
    {
        private string _pathAppData;

        public PlayerController()
        {
            _pathAppData = HttpContext.Current.Server.MapPath("~/App_Data/");
        }

        [HttpGet]
        public IHttpActionResult GetPlayers()
        {
            using (var oEntity = new R6TrackerEntities())
            {
                var players = oEntity.Players.OrderByDescending(o => o.IsActive).ThenBy(o => o.PlayerName).ToList().Select(s =>
                    new PlayerVm()
                    {
                        PlayerId = s.PlayerId,
                        Alias = s.Alias,
                        PlayerName = s.PlayerName,
                        IsActive = s.IsActive,
                        Url = s.Url,
                        RankUrl = s.RankUrl
                    });
                return Ok(players);
            }
        }

        [HttpGet]
        public IHttpActionResult GetGameStats(Guid playerId)
        {
            using (var oEntity = new R6TrackerEntities())
            {
                //var gameStats = oEntity.GameStats.Where(w => w.PlayerId == playerId).ToList();

                var gameStats = oEntity.GetPlayerGameStats(playerId);

                //var lstGameStatsVm = Mapper.Map<List<GameStatsVm>>(gameStats);

                return Ok(gameStats);
            }
        }

        [HttpPost, Route("api/Player/SyncPlayerData")]
        public async Task<IHttpActionResult> SyncPlayerData(Player oPlayer)
        {
            if (oPlayer != null)
            {
                try
                {
                    var oScraper = new Main();
                    oScraper.InitSelenium(_pathAppData);

                    //await oScraper.FetchChromium(new BrowserFetcherOptions(
                    //{
                    //    Path = Server.MapPath
                    //}));
                    var result = await oScraper.ScrapeUserData(oPlayer, _pathAppData);
                    if (!result)
                    {
                        return BadRequest("Not Synced");
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
                    Console.WriteLine(ex);
                    return BadRequest(ex.Message);
                }
            }

            return Ok();
        }

        [HttpPost, Route("api/Player/AddPlayer")]
        public IHttpActionResult AddPlayer(Player oPlayer)
        {
            if (oPlayer != null)
            {
                try
                {
                    var oEntity = new R6TrackerEntities();
                    var doesPlayerExists = oEntity.Players.Any(a => a.Alias.ToLower() == oPlayer.Alias.ToLower());
                    if (!doesPlayerExists)
                    {
                        var oScraper = new Main();
                        oScraper.InitSelenium(_pathAppData);
                        var player = oScraper.GetPlayer(oPlayer.Alias);

                        if (player != null)
                        {
                            oPlayer.PlayerId = Guid.NewGuid();
                            oPlayer.Url = player.Url;
                            oPlayer.IsActive = true;
                            oEntity.Players.Add(oPlayer);
                            oEntity.SaveChanges();
                            return Ok(oPlayer);
                        }
                        else
                        {
                            return BadRequest("Alias doesn't exist");
                        }
                    }
                    else
                    {
                        return BadRequest("Player already exists");
                    }
                    return BadRequest();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return BadRequest(ex.Message);
                }
            }

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
                    oPlayer.PlayerId = player.PlayerId;
                    oPlayer.PlayerName = player.PlayerName;
                    oPlayer.Alias = player.Alias;
                    oPlayer.IsActive = player.IsActive;
                    oPlayer.Url = player.Url;
                    oPlayer.RankUrl = player.RankUrl;
                }
                return Ok(oPlayer);
            }
        }
    }
}
