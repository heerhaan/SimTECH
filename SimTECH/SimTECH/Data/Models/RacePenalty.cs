namespace SimTECH.Data.Models
{
    public class RacePenalty
    {
        public long SeasonDriverId { get; set; }
        public SeasonDriver SeasonDriver { get; set; }
        public long RaceId { get; set; }
        public Race Race { get; set; }
        public long IncidentId { get; set; }
        public Incident Incident { get; set; }
    }
}
