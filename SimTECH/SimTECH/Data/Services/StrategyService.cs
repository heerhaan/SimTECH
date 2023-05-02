using Microsoft.EntityFrameworkCore;
using SimTECH.Data.EditModels;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public class StrategyService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public StrategyService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Strategy>> GetStrategies() => await GetStrategies(StateFilter.Default);
        public async Task<List<Strategy>> GetStrategies(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            // WARNING: Underneath could have a noteable performance impact, if so then refactor
            return await context.Strategy
                .Where(e => filter.StatesForFilter().Contains(e.State))
                .Include(e => e.StrategyTyres)
                    .ThenInclude(e => e.Tyre)
                .ToListAsync();
        }

        public async Task UpdateStrategy(Strategy strategy)
        {
            using var context = _dbFactory.CreateDbContext();

            if (strategy.Id == 0)
            {
                strategy.State = State.Active;
                context.Add(strategy);
            }
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

        public async Task DeleteStrategy(Strategy strategy)
        {
            using var context = _dbFactory.CreateDbContext();

            if (context.Result.Any(e => e.StrategyId == strategy.Id))
            {
                var editModel = new EditStrategyModel(strategy)
                {
                    State = State.Archived
                };
                var modified = editModel.Record;

                context.Update(modified);
            }
            else
            {
                context.Remove(context.StrategyTyre.Where(e => e.StrategyId == strategy.Id));
                context.Remove(strategy);
            }

            await context.SaveChangesAsync();
        }

        public async Task ChangeStrategyState(Strategy strategy, State targetState)
        {
            using var context = _dbFactory.CreateDbContext();

            var editModel = new EditStrategyModel(strategy)
            {
                State = targetState
            };
            var modified = editModel.Record;

            context.Update(modified);

            await context.SaveChangesAsync();
        }

        public async Task<List<Tyre>> GetTyres() => await GetTyres(StateFilter.Default);
        public async Task<List<Tyre>> GetTyres(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Tyre.Where(e => filter.StatesForFilter().Contains(e.State)).ToListAsync();
        }

        public async Task UpdateTyre(Tyre tyre)
        {
            using var context = _dbFactory.CreateDbContext();

            if (tyre.Id == 0)
            {
                tyre.State = State.Active;
                context.Add(tyre);
            }
            else
                context.Update(tyre);

            await context.SaveChangesAsync();
        }

        public async Task DeleteTyre(Tyre tyre)
        {
            using var context = _dbFactory.CreateDbContext();

            if (context.StrategyTyre.Any(e => e.TyreId == tyre.Id))
            {
                var editModel = new EditTyreModel(tyre)
                {
                    State = State.Archived
                };
                var modified = editModel.Record;

                context.Update(modified);
            }
            else
            {
                context.Remove(tyre);
            }

            await context.SaveChangesAsync();
        }
    }
}
