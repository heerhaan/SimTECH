namespace SimTECH.Data.Models
{
    public class Strategy
    {
        public long Id { get; set; }
        public State State { get; set; }

        public int AmountStints { get; set; }

        public IList<StrategyTyre> StrategyTyres { get; set; } = null!;
    }
}
