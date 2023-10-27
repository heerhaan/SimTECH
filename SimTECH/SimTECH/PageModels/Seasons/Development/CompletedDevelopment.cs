namespace SimTECH.PageModels.Seasons.Development
{
    public class DevelopSummary
    {
        public Entrant EntrantGroup { get; set; }
        public Aspect[] Aspects { get; set; }
        public List<EntrantDevelopLog> EntrantLogs { get; set; }
        public Dictionary<Aspect, (int, int)> MinMaxValues { get; set; }

        public string SummaryElementId => $"entrant-summary-{EntrantGroup.ToString().ToLowerInvariant()}";
    }

    public class EntrantDevelopLog
    {
        public long EntrantId { get; set; }
        public Entrant Entrant { get; set; }
        public string Name { get; set; }
        public Country? Nationality { get; set; }
        public Dictionary<Aspect, (int, int)> DevelopedAspects { get; set; }
    }
}
