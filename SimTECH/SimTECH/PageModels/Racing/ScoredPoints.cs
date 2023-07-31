namespace SimTECH.PageModels.Racing
{
    public class ScoredPoints
    {
        public long SeasonDriverId { get; set; }
        public long SeasonTeamId { get; set; }

        public int Points { get; set; }
        public int HiddenPoints { get; set; }
    }
}
