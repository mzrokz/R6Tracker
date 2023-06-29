using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R6T.Model.Context.R6Tracker
{
    public partial class MatchType
    {
        public MatchType()
        {
            this.GameStats = new HashSet<GameStat>();
        }

        public int MatchTypeId { get; set; }
        public string MatchTypeName { get; set; }

        public virtual ICollection<GameStat> GameStats { get; set; }
    }
}
