using SimTECH.Common.Enums;

namespace SimTECH.Data.Models
{
    public sealed class League : ModelState
    {
        public string Name { get; set; } = default!;
        public int RaceLength { get; set; }
        public LeagueOptions Options { get; set; }

        // TODO: Consider adding minimum and maximum values for skill, reliability, team, etc...

        public IList<DevelopmentRange> DevelopmentRanges { get; set; }
        public IList<Season> Seasons { get; set; }
        public IList<Contract> Contracts { get; set; }
        public IList<LeagueTyre> LeagueTyres { get; set; }

        public override int GetHashCode() => Name?.GetHashCode() ?? 0;
        public override string ToString() => string.IsNullOrEmpty(Name) ? "[Unknown]" : Name;
    }

    public static class LeagueExtensions
    {
        public static List<DevelopmentRange> GetAspectRanges(this IList<DevelopmentRange> ranges, Aspect aspect)
        {
            return ranges.Where(e => e.Type == aspect).OrderBy(e => e.Comparer).ToList();
        }
    }
}
