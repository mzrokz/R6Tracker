

namespace R6T.Model.Context.R6Tracker
{
    public partial class GameStat
    {
        public Guid GameStatId { get; set; }
        public Guid? PlayerId { get; set; }
        public int? MatchTypeId { get; set; }
        public int? PlayerLevel { get; set; }
        public int? MatchesPlayed { get; set; }
        public int? Wins { get; set; }
        public int? Losses { get; set; }
        public int? Kills { get; set; }
        public int? Deaths { get; set; }
        public int? Headshots { get; set; }
        public int? MeleeKills { get; set; }
        public int? BlindKills { get; set; }
        public decimal? KD { get; set; }
        public string? TimePlayed { get; set; }
        public string? TotalXp { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? WinPercent { get; set; }
        public string? HeadshotPercent { get; set; }
        public decimal? KillPerMatch { get; set; }
        public decimal? KillPerMin { get; set; }
        public string? RankUrl { get; set; }
        public int? MMR { get; set; }

        public virtual MatchType MatchType { get; set; }
        public virtual Player Player { get; set; }
    }
}
