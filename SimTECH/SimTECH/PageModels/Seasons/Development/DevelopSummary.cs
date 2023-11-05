using SimTECH.Common.Enums;

namespace SimTECH.PageModels.Seasons.Development;

public class DevelopSummary
{
    public Entrant EntrantGroup { get; set; }
    public Aspect[] Aspects { get; set; }
    public List<EntrantDevelopLog> EntrantLogs { get; set; }
    public Dictionary<Aspect, (int, int)> MinMaxValues { get; set; }

    public string SummaryElementId => $"entrant-summary-{EntrantGroup.ToString().ToLowerInvariant()}";
}
