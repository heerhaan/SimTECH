namespace SimTECH.Data.Models
{
    public sealed class Contract : ModelBase
    {
        public int Duration { get; set; }
        // TODO: Field {Cost} are related to finance functionality, which isn't implemented yet
        //public int Cost { get; set; }

        public long DriverId { get; set; }
        public Driver Driver { get; set; }
        public long TeamId { get; set; }
        public Team Team { get; set; }
        public long LeagueId { get; set; }
        public League League { get; set; }

        public bool IsExpired => Duration == 0;
    }
}
