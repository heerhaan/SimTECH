using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class StrategyService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

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

        public StrategyService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Strategy>> GetStrategies()
        {
            using var context = _dbFactory.CreateDbContext();

            // WARNING: Underneath could have a big performance impact, if that seems to be the case then refactor pls
            return await context.Strategy
                .Include(e => e.StrategyTyres)
                .ThenInclude(e => e.Tyre)
                .ToListAsync();
        }

        public async Task CreateStrategy(Strategy strategy)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Add(strategy);

            await context.SaveChangesAsync();
        }

        public async Task<List<Tyre>> GetTyres()
        {
            using var context = _dbFactory.CreateDbContext();

            var tyres = await context.Tyre.ToListAsync();

            return _tyres;
        }

        public async Task CreateTyre(Tyre tyre)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Add(tyre);

            await context.SaveChangesAsync();
        }
    }
}
