namespace SimTECH.Data.Models
{
    public class Stint
    {
        public long Id { get; set; }
        public int Order { get; set; }
        public bool ApplyDriver { get; set; }
        public bool ApplyTeam { get; set; }
        public bool ApplyEngine { get; set; }
        public bool ApplyQualifying { get; set; }
        public bool CheckReliability { get; set; }
        public int RNGMaximum { get; set; }
        public int RNGMinimum { get; set; }

        public IList<StintResult> StintResults { get; set; }

        public long RaceId { get; set; }
        public Race Race { get; set; } = null!;
    }
}
