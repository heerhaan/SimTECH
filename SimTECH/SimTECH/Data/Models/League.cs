using SimTECH.Common.Enums;
using SimTECH.PageModels.Leagues;

namespace SimTECH.Data.Models;

public sealed class League : ModelState
{
    public string Name { get; set; } = string.Empty;
    public int RaceLength { get; set; }
    public LeagueOptions Options { get; set; }
    public int DisqualificationOdds { get; set; } = 100;
    public int FatalityOdds { get; set; } = 250;
    public int SafetyCarOdds { get; set; } = 5;
    public int SafetyCarReturnOdds { get; set; } = 2;
    public int SafetyCarGap { get; set; } = 25;
    public int SafetyCarGapCloser { get; set; } = 200;
    public int BattleRng { get; set; } = 5;
    public int DriverStatusPaceModifier { get; set; } = 3;
    public int SetupRng { get; set; }

    // TODO: Consider adding minimum and maximum values for skill, reliability, team, etc...

    public IList<DevelopmentRange> DevelopmentRanges { get; set; }
    public IList<Season> Seasons { get; set; }
    public IList<Contract> Contracts { get; set; }
    public IList<LeagueTyre> LeagueTyres { get; set; }

    // Next time leave a comment why this was added here, bellend
    public override int GetHashCode() => Name?.GetHashCode() ?? 0;
    public override string ToString() => string.IsNullOrEmpty(Name) ? "[Unknown]" : Name;
}

public static class LeagueExtensions
{
    public static List<DevelopmentRange> GetAspectRanges(this IList<DevelopmentRange> ranges, Aspect aspect)
    {
        return ranges.Where(e => e.Type == aspect).OrderBy(e => e.Comparer).ToList();
    }

    public static LeagueListItem ToListItem(this League league)
    {
        return new LeagueListItem
        {
            League = league,

            Id = league.Id,
            Name = league.Name,
            RaceLength = league.RaceLength,
            Options = league.Options,
            State = league.State,
            DevelopmentRanges = league.DevelopmentRanges?.ToList() ?? [],
        };
    }
}
