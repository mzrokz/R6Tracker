//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace R6T.Model
{
    using System;
    using System.Collections.Generic;
    using R6T.Model.ViewModels;
    
    [AttachXPath(EMatchType.General, EMatchType.Ranked, EMatchType.Unranked, EMatchType.Casual)]
    public partial class GameStat
    {
        public System.Guid GameStatId { get; set; }
        public System.Guid? PlayerId { get; set; }

        public int? MatchTypeId { get; set; }

        [AttachXPath("//div[@class='trn-defstat__value']", "//div[@class='trn-defstat__value']",
            "//div[@class='trn-defstat__value']","//div[@class='trn-defstat__value']")]
        public int? PlayerLevel { get; set; }

        [AttachXPath("//div[@data-stat='PVPMatchesPlayed']", "//div[@data-stat='RankedMatches']",
            "//div[@data-stat='UnRankedMatches']","//div[@data-stat='CasualMatches']")]
        public int? MatchesPlayed { get; set; }

        [AttachXPath("(//div[@data-stat='PVPMatchesWon'])[2]", "//div[@data-stat='RankedWins']",
            "//div[@data-stat='UnRankedWins']","//div[@data-stat='CasualWins']")]
        public int? Wins { get; set; }

        [AttachXPath("//div[@data-stat='PVPMatchesLost']", "//div[@data-stat='RankedLosses']",
            "//div[@data-stat='UnRankedlLosses']","//div[@data-stat='CasualLosses']")]
        public int? Losses { get; set; }

        [AttachXPath("//div[@data-stat='PVPKills']", "//div[@data-stat='RankedKills']",
            "//div[@data-stat='UnRankedKills']","//div[@data-stat='CasualKills']")]
        public int? Kills { get; set; }

        [AttachXPath("//div[@data-stat='PVPDeaths']", "//div[@data-stat='RankedDeaths']",
            "//div[@data-stat='UnRankedDeaths']","//div[@data-stat='CasualDeaths']")]
        public int? Deaths { get; set; }

        [AttachXPath("//div[@data-stat='PVPHeadshots']", "", "")]
        public int? Headshots { get; set; }

        [AttachXPath("//div[@data-stat='PVPMeleeKills']", "", "")]
        public int? MeleeKills { get; set; }

        [AttachXPath("//div[@data-stat='PVPBlindKills']", "", "")]
        public int? BlindKills { get; set; }

        [AttachXPath("(//div[@data-stat='PVPKDRatio'])[2]", "//div[@data-stat='RankedKDRatio']",
            "//div[@data-stat='UnRankedKDRatio']","//div[@data-stat='CasualKDRatio']")]
        public decimal? KD { get; set; }

        [AttachXPath("//div[@data-stat='PVPTimePlayed']", "//div[@data-stat='RankedTimePlayed']",
            "//div[@data-stat='UnRankedTimePlayed']","//div[@data-stat='CasualTimePlayed']")]
        public string TimePlayed { get; set; }

        [AttachXPath("//div[@data-stat='PVPTotalXp']", "", "")]
        public string TotalXp { get; set; }

        [AttachXPath("(//div[@data-stat='PVPWLRatio'])[2]", "//div[@data-stat='RankedWLRatio']",
            "//div[@data-stat='UnRankedWLRatio']","//div[@data-stat='CasualWLRatio']")]
        public string WinPercent { get; set; }

        [AttachXPath("//div[@data-stat='PVPAccuracy']", "", "")]
        public string HeadshotPercent { get; set; }

        [AttachXPath("", "//div[@data-stat='RankedKillsPerMatch']", "//div[@data-stat='UnRankedKillsPerMatch']","//div[@data-stat='CasualKillsPerMatch']")]
        public decimal? KillPerMatch { get; set; }

        [AttachXPath("", "//div[@data-stat='RankedKillsPerMinute']", "//div[@data-stat='UnRankedKillsPerMinute']","//div[@data-stat='CasualKillsPerMinute']")]
        public decimal? KillPerMin { get; set; }

        public System.DateTime? CreatedDate { get; set; }
        public virtual MatchType MatchType { get; set; }
        public virtual Player Player { get; set; }
    }
}
