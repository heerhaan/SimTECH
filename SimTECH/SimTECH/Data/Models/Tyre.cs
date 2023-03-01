namespace SimTECH.Data.Models
{
    public class Tyre
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string Colour { get; set; } = default!;
        public State State { get; set; }

        public int Length { get; set; }
        public int Pace { get; set; }
        public int WearMax { get; set; }
        public int WearMin { get; set; }

        public IList<StrategyTyre> StrategyTyres { get; set; }
    }
}
