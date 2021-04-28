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
    
    public partial class GetPlayerGameStats_Result
    {
        public Nullable<System.Guid> PlayerId { get; set; }
        public string PlayerName { get; set; }
        public Nullable<int> MatchTypeId { get; set; }
        public string MatchTypeName { get; set; }
        public Nullable<int> PlayerLevel { get; set; }
        public Nullable<int> MatchesPlayed { get; set; }
        public Nullable<int> Wins { get; set; }
        public string WinPercent { get; set; }
        public Nullable<int> Losses { get; set; }
        public Nullable<int> Kills { get; set; }
        public Nullable<int> Deaths { get; set; }
        public Nullable<int> Headshots { get; set; }
        public string HeadshotPercent { get; set; }
        public Nullable<int> MeleeKills { get; set; }
        public Nullable<int> BlindKills { get; set; }
        public Nullable<decimal> KD { get; set; }
        public Nullable<decimal> KillPerMatch { get; set; }
        public Nullable<decimal> KillPerMin { get; set; }
        public string TimePlayed { get; set; }
        public string TotalXp { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> LatestRecord { get; set; }
        public string RankUrl { get; set; }
    }
}
