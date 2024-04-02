using SimTECH.Common.Enums;

namespace SimTECH.PageModels.Seasons.Development;

public class DevelopSummary
{
    public Entrant EntrantGroup { get; set; }

    public List<EntrantDevelopLog> EntrantLogs { get; set; }

    public Dictionary<Aspect, (int, int)> MinMaxValues { get; set; }

    public string SummaryElementId => $"entrant-summary-{EntrantGroup.ToString().ToLowerInvariant()}";
}

public class AspectDevelopment
{
    public Aspect Aspect { get; set; }

    public int Changed { get; set; }

    public int Current { get; set; }

    public int Initial => Current - Changed;
}
