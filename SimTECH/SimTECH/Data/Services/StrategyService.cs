using Microsoft.EntityFrameworkCore;
using SimTECH.Data.EditModels;
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

            // WARNING: Underneath could have a big performance impact, if that seems to be the case then refactor pls
            return await context.Strategy
                .Include(e => e.StrategyTyres)
                .ThenInclude(e => e.Tyre)
                .ToListAsync();
        }

        // Doesnt work
        public async Task CreateStrategy(Strategy strategy)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Add(strategy);

            await context.SaveChangesAsync();
        }

        public async Task TempWorkingCreate(EditStrategyModel model)
        {
            using var context = _dbFactory.CreateDbContext();

            Strategy strategy = new();
            if (model.Id == 0)
            {
            }
            else
            {
                var removeables = await context.StrategyTyre.Where(e => e.StrategyId == model.Id).ToListAsync();
                if (removeables.Any())
                    context.RemoveRange(removeables);
            }
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
