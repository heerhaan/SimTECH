namespace SimTECH.Data.Models
{
    public class LapScore
    {
        public long Id { get; set; }
        public int Score { get; set; }
        public RacerEvent RacerEvents { get; set; }

        public long ResultId { get; set; }
        public Result Result { get; set; }
    }
}
