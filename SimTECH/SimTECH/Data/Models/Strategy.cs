namespace SimTECH.Data.Models
{
    public record Strategy
    {
        public long Id { get; set; }
        public State State { get; set; }

        public IList<StrategyTyre> StrategyTyres { get; set; } = default!;
    }
}
