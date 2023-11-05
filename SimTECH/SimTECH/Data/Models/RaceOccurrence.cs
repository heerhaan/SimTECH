using SimTECH.Common.Enums;

namespace SimTECH.Data.Models
{
    public sealed class RaceOccurrence : ModelBase
    {
        public int Order { get; set; }
        public SituationOccurrence Occurrences { get; set; }

        public long RaceId { get; set; }
        public Race Race { get; set; }
    }
}
