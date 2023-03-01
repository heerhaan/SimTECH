using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class StrategyService
    {
        private readonly List<Strategy> _strategies = new()
        {
            new Strategy()
            {
                Id = 1,
            }
        };

        public List<Strategy> GetTestData()
        {
            return _strategies;
        }

        public void CreateStrategy(Strategy strategy)
        {
            _strategies.Add(strategy);
        }
    }
}
