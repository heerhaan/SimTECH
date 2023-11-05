using SimTECH.Common.Enums;

namespace SimTECH.PageModels.Seasons.Development;

public class DevelopModel
{
    public long ActiveRaceClassId { get; set; }
    public Entrant ActiveEntrant { get; set; } = Entrant.Driver;
    public Aspect ActiveAspect { get; set; } = Aspect.Reliability;
    public Quantifier ActiveQuantifier { get; set; } = Quantifier.Range;
    public int MinChange { get; set; }
    public int MaxChange { get; set; }
    public bool IsOptionalColumnVisible { get; set; }
    public bool IsDirectRangeReadOnly => ActiveQuantifier != Quantifier.Direct;
}
