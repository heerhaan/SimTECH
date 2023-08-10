namespace SimTECH.Data.Models
{
    public abstract class ContractBase : ModelBase
    {
        public int Duration { get; set; }

        public long LeagueId { get; set; }
        public League League { get; set; }
    }
}
