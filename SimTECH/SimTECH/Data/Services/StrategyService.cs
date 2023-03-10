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

            // WARNING: Underneath could have a noteable performance impact, if so then refactor
            return await context.Strategy
                .Include(e => e.StrategyTyres)
                    .ThenInclude(e => e.Tyre)
                .ToListAsync();
        }

        public async Task UpdateStrategy(Strategy strategy)
        {
            using var context = _dbFactory.CreateDbContext();

            if (strategy.Id == 0)
                context.Add(strategy);
            else
            {
                var removeables = await context.StrategyTyre
                    .Where(e => e.StrategyId == strategy.Id 
                        && !strategy.StrategyTyres.Select(st => st.Id).Contains(e.Id))
                    .ToListAsync();
                if (removeables.Any())
                    context.RemoveRange(removeables);

                context.Update(strategy);
            }

            await context.SaveChangesAsync();
        }

        public async Task<List<Tyre>> GetTyres()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Tyre.ToListAsync();
        }

        public async Task UpdateTyre(Tyre tyre)
        {
            using var context = _dbFactory.CreateDbContext();

            if (tyre.Id == 0)
                context.Add(tyre);
            else
                context.Update(tyre);

            await context.SaveChangesAsync();
        }
    }
}
