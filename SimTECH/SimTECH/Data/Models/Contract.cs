namespace SimTECH.Data.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public int Cost { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int LeagueId { get; set; }
        public League League { get; set; }
    }
}
