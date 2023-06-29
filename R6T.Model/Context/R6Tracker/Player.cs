
namespace R6T.Model.Context.R6Tracker
{
    public partial class Player
    {
        public Player()
        {
            this.GameStats = new HashSet<GameStat>();
        }

        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Alias { get; set; }
        public bool? IsActive { get; set; }
        public string Url { get; set; }
        public string RankUrl { get; set; }
        public int? SortOrder { get; set; }
        public string? LatestAlias { get; set; }

        public virtual ICollection<GameStat> GameStats { get; set; }
    }
}
