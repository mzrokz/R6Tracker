using AutoMapper;
using R6T.Model;
using R6T.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

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
                var gameStats = oEntity.GameStats.Where(w => w.PlayerId == playerId).ToList();

                var lstGameStatsVm = Mapper.Map<List<GameStatsVm>>(gameStats);

                return Ok(lstGameStatsVm);
            }
        }
    }
}
