using Microsoft.EntityFrameworkCore;

namespace R6T.Model.Context.R6Tracker
{
    public class R6TrackerEntities : DbContext
    {
        public R6TrackerEntities(DbContextOptions<R6TrackerEntities> options) : base(options)
        {
        }

        public virtual DbSet<GameStat> GameStats { get; set; }
        public virtual DbSet<MatchType> MatchTypes { get; set; }
        public virtual DbSet<Player> Players { get; set; }
    }
}