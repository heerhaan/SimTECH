namespace SimTECH.Data.Models
{
    public sealed class Strategy
    {
        public long Id { get; set; }
        public State State { get; set; }

        public IList<StrategyTyre> StrategyTyres { get; set; } = default!;
    }

    public static class ExtendStrategy
    {
        public static int AveragePace(this Strategy strategy)
        {
            var pace = 0;

            if (strategy.StrategyTyres?.Any() == true)
            {
                foreach (var strategyTyre in strategy.StrategyTyres.OrderBy(e => e.Order))
                {
                    var mimicLife = strategyTyre.Tyre.Pace;
                    var averageWear = (strategyTyre.Tyre.WearMax + strategyTyre.Tyre.WearMin) / 2;

                    while (mimicLife > Math.Abs(strategyTyre.Tyre.WearMin))
                    {
                        pace += mimicLife;
                        mimicLife -= averageWear;
                    }
                }
            }

            return pace;
        }
    }
}
