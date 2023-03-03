using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class StrategyService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public StrategyService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Strategy>> GetStrategies()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Strategy.ToListAsync();
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

            return await context.Tyre.ToListAsync();
        }

        public async Task CreateTyre(Tyre tyre)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Add(tyre);

            await context.SaveChangesAsync();
        }
    }
}
