using AutoMapper;
using R6T.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6T.Model
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            Mapper.CreateMap<Player, PlayerVm>();
            Mapper.CreateMap<GameStat, GameStatsVm>()
                .ForMember(f => f.Player, opt => opt.Ignore())
                .ForMember(f => f.MatchType, opt => opt.Ignore());
        }
    }
}
