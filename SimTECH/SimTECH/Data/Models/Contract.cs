namespace SimTECH.Data.Models
{
    public class Contract
    {
        public long Id { get; set; }
        public int Duration { get; set; }
        // TODO: Field {Cost} are related to finance functionality, which isn't implemented yet
        //public int Cost { get; set; }

        public long DriverId { get; set; }
        public Driver Driver { get; set; } = default!;
        public long TeamId { get; set; }
        public Team Team { get; set; } = default!;
        public long LeagueId { get; set; }
        public League League { get; set; } = default!;
    }
}
