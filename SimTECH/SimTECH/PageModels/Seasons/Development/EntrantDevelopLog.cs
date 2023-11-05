using SimTECH.Common.Enums;

namespace SimTECH.PageModels.Seasons.Development;

public class EntrantDevelopLog
{
    public long EntrantId { get; set; }
    public Entrant Entrant { get; set; }
    public string Name { get; set; }
    public Country? Nationality { get; set; }
    public Dictionary<Aspect, (int, int)> DevelopedAspects { get; set; }
}
