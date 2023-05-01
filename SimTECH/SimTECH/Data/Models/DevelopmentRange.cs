namespace SimTECH.Data.Models
{
    public class DevelopmentRange
    {
        public long Id { get; set; }
        public RangeType Type { get; set; }
        public int Comparer { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public long LeagueId { get; set; }
        public League League { get; set; } = default!;
    }
}
