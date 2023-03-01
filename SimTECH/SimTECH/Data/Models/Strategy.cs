namespace SimTECH.Data.Models
{
    public class Strategy
    {
        public long Id { get; set; }
        public int StintLength { get; set; }
        public State State { get; set; }

        public IList<StrategyTyre> StrategyTyres { get; set; } = default!;
    }
}
