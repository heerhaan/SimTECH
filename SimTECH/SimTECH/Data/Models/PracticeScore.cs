namespace SimTECH.Data.Models
{
    public class PracticeScore
    {
        public long Id { get; set; }
        public int Index { get; set; }
        public int[]? Scores { get; set; }
        public int Position { get; set; }

        public long RaceId { get; set; }
        public long ResultId { get; set; }
        public Result Result { get; set; }
    }
}
