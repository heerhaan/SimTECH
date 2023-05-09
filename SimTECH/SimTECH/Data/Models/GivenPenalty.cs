namespace SimTECH.Data.Models
{
    public class GivenPenalty : ModelBase
    {
        public bool Consumed { get; set; }
        public long? ConsumedAtRaceId { get; set; }

        public long SeasonDriverId { get; set; }
        public SeasonDriver SeasonDriver { get; set; }
        public long IncidentId { get; set; }
        public Incident Incident { get; set; }
    }
}
