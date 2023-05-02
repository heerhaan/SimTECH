namespace SimTECH.Data.Models
{
    public sealed class Strategy
    {
        public long Id { get; set; }
        public State State { get; set; }

        public IList<StrategyTyre> StrategyTyres { get; set; } = default!;
    }
}
