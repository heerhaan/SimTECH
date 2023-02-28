namespace SimTECH.Data.Models
{
    public class StintResult
    {
        public long Id { get; set; }
        public int Order { get; set; }
        public int StintScore { get; set; }
        public int TotalScore { get; set; }
        public int Position { get; set; }
        public string Status { get; set; }
        public bool Pitstop { get; set; }

        public int StintId { get; set; }
        public Stint Stint { get; set; }
        public int RaceResultId { get; set; }
        public Result RaceResult { get; set; }
    }
}
