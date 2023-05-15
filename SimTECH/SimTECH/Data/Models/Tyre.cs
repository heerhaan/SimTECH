namespace SimTECH.Data.Models
{
    public sealed class Tyre
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string Colour { get; set; } = default!;
        public State State { get; set; }

        public int Length { get; set; }
        public int Pace { get; set; }
        public int WearMax { get; set; }
        public int WearMin { get; set; }

        public IList<StrategyTyre>? StrategyTyres { get; set; }

        public int PredictedLength()
        {
            if (Pace == 0 || (WearMax == 0 && WearMin == 0))
                return 0;

            var wearAverage = (WearMax + WearMin) / 2;

            return (Pace / wearAverage) * 10;
        }
    }
}
