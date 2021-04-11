using AutoMapper;
using R6T.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6T.Model
{
    public static class MapperProfile
    {
        public static MapperConfiguration Configuration;

        public static void CreateConfiguration()
        {
            Configuration = new MapperConfiguration(cfg =>
             {
                 cfg.CreateMap<Player, PlayerVm>();
                 cfg.CreateMap<GameStat, GameStatsVm>()
                     .ForMember(f => f.Player, opt => opt.Ignore())
                     .ForMember(f => f.MatchType, opt => opt.Ignore())
                     .ReverseMap();
             });
        }
    }
}
