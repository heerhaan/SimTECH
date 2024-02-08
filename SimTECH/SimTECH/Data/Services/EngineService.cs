using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.EditModels;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public class EngineService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public EngineService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Engine>> GetEngines() => await GetEngines(StateFilter.Default);
        public async Task<List<Engine>> GetEngines(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Engine
                .Where(e => filter.StatesForFilter().Contains(e.State))
                .ToListAsync();
        }

        public async Task<List<Engine>> GetAvailableEngines(long seasonId, StateFilter filter = StateFilter.Active)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Engine
                .Where(e => filter.StatesForFilter().Contains(e.State)
                    && !e.SeasonEngines.Any(se => se.SeasonId == seasonId))
                .ToListAsync();
        }

        public async Task UpdateEngine(Engine engine)
        {
            ValidateEngine(engine);

            using var context = _dbFactory.CreateDbContext();

            if (engine.Id == 0)
            {
                engine.State = State.Active;
                context.Add(engine);
            }
            else
                context.Update(engine);

            await context.SaveChangesAsync();
        }

        public async Task ArchiveEngine(Engine engine)
        {
            using var context = _dbFactory.CreateDbContext();

            if (engine.State == State.Archived)
            {
                engine.State = State.Active;
            }
            else
            {
                if (context.SeasonEngine.Any(e => e.EngineId == engine.Id))
                {
                    var editModel = new EditEngineModel(engine)
                    {
                        State = State.Archived
                    };

                    var modified = editModel.Record;
                    context.Update(modified);
                }
                else
                {
                    context.Remove(engine);
                }
            }

            await context.SaveChangesAsync();
        }

        #region validation

        private static void ValidateEngine(Engine engine)
        {
            if (engine == null)
                throw new NullReferenceException("Engine is very null here, yes");
        }

        #endregion
    }
}
