using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.PageModels.Leagues;

public class LeagueListItem
{
    public League League { get; set; }

    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int RaceLength { get; set; }
    public LeagueOptions Options { get; set; }
    public State State { get; set; }

    public List<DevelopmentRange> DevelopmentRanges { get; set; }
    public List<Tyre> LeagueTyres { get;set; }
}
