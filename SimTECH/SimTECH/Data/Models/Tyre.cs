namespace SimTECH.Data.Models
{
    public class Tyre
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public State State { get; set; }

        public string Colour { get; set; }
        public int Length { get; set; }
        public int Pace { get; set; }
        public int WearMaximum { get; set; }
        public int WearMinimum { get; set; }

        public IList<StrategyTyre> StrategyTyres { get; set; } = null!;
    }
}
