namespace SimTECH.Data.Models
{
    public sealed class League
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public int RaceLength { get; set; }
        public LeagueOptions Options { get; set; }
        public State State { get; set; }

        // TODO: Tiers could be helpful in adding them to the same races but it's a bigger thing, requires total design
        //public int Tier { get; set; }

        // TODO: Consider adding minimum and maximum values for skill, reliability, team, etc...

        public IList<DevelopmentRange>? DevelopmentRanges { get; set; }
        public IList<Season>? Seasons { get; set; }
        public IList<Contract>? Contracts { get; set; }
    }

    public static class LeagueExtensions
    {
        public static List<DevelopmentRange> GetAspectRanges(this IList<DevelopmentRange> ranges, Aspect aspect)
        {
            return ranges.Where(e => e.Type == aspect).OrderBy(e => e.Comparer).ToList();
        }
    }
}
