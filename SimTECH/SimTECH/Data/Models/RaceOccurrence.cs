namespace SimTECH.Data.Models
{
    public sealed class RaceOccurrence
    {
        public long Id { get; set; }
        public int Order { get; set; }
        public SituationOccurrence Occurrences { get; set; }

        public long RaceId { get; set; }
        public Race Race { get; set; }
    }
}
