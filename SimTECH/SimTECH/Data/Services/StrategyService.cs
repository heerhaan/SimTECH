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

        private readonly List<Tyre> _tyres = new()
        {
            new Tyre()
            {
                Id = 1,
                Name = "Hard",
                Colour = "#e4e2e2ff",
                State = State.Active,
                Length = 9,
                Pace = 10,
                WearMax = -2,
                WearMin = -1,
            },
            new Tyre()
            {
                Id = 2,
                Name = "Medium",
                Colour = "#e2a83eff",
                State = State.Active,
                Length = 6,
                Pace = 15,
                WearMax = -6,
                WearMin = -3,
            },
            new Tyre()
            {
                Id = 3,
                Name = "Soft",
                Colour = "#d61616ff",
                State = State.Active,
                Length = 3,
                Pace = 30,
                WearMax = -15,
                WearMin = -10,
            },
        };

        public List<Strategy> GetTestData()
        {
            return _strategies;
        }

        public List<Tyre> GetTyres()
        {
            return _tyres;
        }

        public void CreateStrategy(Strategy strategy)
        {
            _strategies.Add(strategy);
        }

        public void CreateTyre(Tyre tyre)
        {
            _tyres.Add(tyre);
        }
    }
}
