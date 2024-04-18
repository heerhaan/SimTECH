using SimTECH.Common.Enums;

namespace SimTECH.Data.Models;

public class DevelopmentRange : ModelBase
{
    public Aspect Type { get; set; }
    public int Comparer { get; set; }
    public int Minimum { get; set; }
    public int Maximum { get; set; }

    public long LeagueId { get; set; }
    public League League { get; set; }
}
