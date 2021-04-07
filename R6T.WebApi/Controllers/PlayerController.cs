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
        public PlayerController()
        {

        }

        [HttpGet]
        public IHttpActionResult GetPlayers()
        {
            using (var oEntity = new R6TrackerEntities())
            {
                var players = oEntity.Players.ToList().Select(s =>
                new PlayerVm()
                {
                    PlayerId = s.PlayerId,
                    Alias = s.Alias,
                    PlayerName = s.PlayerName,
                    IsActive = s.IsActive
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
                    var path = HttpContext.Current.Server.MapPath("~/App_Data/");
                    var oScraper = new Main();
                    oScraper.InitSelenium(path);

                    //await oScraper.FetchChromium(new BrowserFetcherOptions(
                    //{
                    //    Path = Server.MapPath
                    //}));
                    var result = oScraper.ScrapeUserData(oPlayer);
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
    }
}
