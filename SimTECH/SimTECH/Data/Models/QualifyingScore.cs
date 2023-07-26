using System.ComponentModel.DataAnnotations.Schema;

namespace SimTECH.Data.Models
{
    public sealed class QualifyingScore : ModelBase
    {
        public int Index { get; set; }
        public int[]? Scores { get; set; }
        public int Position { get; set; }

        public long RaceId { get; set; }
        public long ResultId { get; set; }
        public Result Result { get; set; }

        [NotMapped]
        public int PenaltyPunish { get; set; }
    }

    public static class ExtendQualifyingScore
    {
        public static int PenaltyPosition(this QualifyingScore score)
        {
            return score.Position + score.PenaltyPunish;
        }
    }
}
