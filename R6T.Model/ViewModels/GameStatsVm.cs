using System;

namespace R6T.Model.ViewModels
{
    [AttachXPath(
        EMatchType.General,
        EMatchType.Ranked,
        EMatchType.Unranked,
        EMatchType.Casual)]
    public class GameStatsVm
    {

        public GameStatsVm()
        {
        }

        [AttachXPath(
            "(//div[@class='trn-defstat__value-stylized'])[2]",
            "(//div[@class='trn-defstat__value-stylized'])[2]",
            "(//div[@class='trn-defstat__value-stylized'])[2]",
            "(//div[@class='trn-defstat__value-stylized'])[2]")]
        public new int? PlayerLevel { get; set; }

        [AttachXPath(
            "//div[@data-stat='PVPMatchesPlayed']",
            "//div[@data-stat='RankedMatches']",
            "//div[@data-stat='UnRankedMatches']",
            "//div[@data-stat='CasualMatches']")]
        public new int? MatchesPlayed { get; set; }

        [AttachXPath(
            "(//div[@data-stat='PVPMatchesWon'])[2]",
            "//div[@data-stat='RankedWins']",
            "//div[@data-stat='UnRankedWins']",
            "//div[@data-stat='CasualWins']")]
        public new int? Wins { get; set; }

        [AttachXPath(
            "//div[@data-stat='PVPMatchesLost']",
            "//div[@data-stat='RankedLosses']",
            "//div[@data-stat='UnRankedlLosses']",
            "//div[@data-stat='CasualLosses']")]
        public new int? Losses { get; set; }

        [AttachXPath(
            "//div[@data-stat='PVPKills']",
            "//div[@data-stat='RankedKills']",
            "//div[@data-stat='UnRankedKills']",
            "//div[@data-stat='CasualKills']")]
        public new int? Kills { get; set; }

        [AttachXPath(
            "//div[@data-stat='PVPDeaths']",
            "//div[@data-stat='RankedDeaths']",
            "//div[@data-stat='UnRankedDeaths']",
            "//div[@data-stat='CasualDeaths']")]
        public new int? Deaths { get; set; }

        [AttachXPath("//div[@data-stat='PVPHeadshots']", "", "")]
        public new int? Headshots { get; set; }

        [AttachXPath("//div[@data-stat='PVPMeleeKills']", "", "")]
        public new int? MeleeKills { get; set; }

        [AttachXPath("//div[@data-stat='PVPBlindKills']", "", "")]
        public new int? BlindKills { get; set; }

        [AttachXPath(
            "(//div[@data-stat='PVPKDRatio'])[2]",
            "//div[@data-stat='RankedKDRatio']",
            "//div[@data-stat='UnRankedKDRatio']",
            "//div[@data-stat='CasualKDRatio']")]
        public new decimal? KD { get; set; }

        [AttachXPath(
            "//div[@data-stat='PVPTimePlayed']",
            "//div[@data-stat='RankedTimePlayed']",
            "//div[@data-stat='UnRankedTimePlayed']",
            "//div[@data-stat='CasualTimePlayed']")]
        public new string TimePlayed { get; set; }

        [AttachXPath("//div[@data-stat='PVPTotalXp']", "", "")]
        public new string TotalXp { get; set; }

        [AttachXPath(
            "(//div[@data-stat='PVPWLRatio'])[2]",
            "//div[@data-stat='RankedWLRatio']",
            "//div[@data-stat='UnRankedWLRatio']",
            "//div[@data-stat='CasualWLRatio']")]
        public new string WinPercent { get; set; }

        [AttachXPath("//div[@data-stat='PVPAccuracy']", "", "")]
        public new string HeadshotPercent { get; set; }

        [AttachXPath(
            "",
            "//div[@data-stat='RankedKillsPerMatch']",
            "//div[@data-stat='UnRankedKillsPerMatch']",
            "//div[@data-stat='CasualKillsPerMatch']")]
        public new decimal? KillPerMatch { get; set; }

        [AttachXPath(
            "",
            "//div[@data-stat='RankedKillsPerMinute']",
            "//div[@data-stat='UnRankedKillsPerMinute']",
            "//div[@data-stat='CasualKillsPerMinute']")]
        public new decimal? KillPerMin { get; set; }

        [AttachXPath(
            "",
            "//div[contains(@class,'trn-card__content trn-card--light')]//img",
            "",
            "//*[@id='profile']//div[3]//div[1]//div[4]//div[2]//div[2]//div[1]//svg//g//image")]
        [ElementValue("src")]
        public string RankUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long LatestRecord { get; set; }
        public Nullable<int> MatchTypeId { get; set; }
        public string MatchTypeName { get; set; }

        public GameStatsVm Difference { get; set; }
        public MatchType MatchType { get; set; }
        public Player Player { get; set; }

    }
}
