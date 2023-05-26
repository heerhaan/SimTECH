namespace SimTECH.PageModels.Racing
{
    public class QualifyingStage : StageBase
    {
        public int QualyRng { get; set; }
        public int MaximumRaceDrivers { get; set; }

        public List<long> PenaltiesToConsume { get; set; } = new();
    }
}
