using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class EngineService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public EngineService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Engine>> GetEngines()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Engine.ToListAsync();
        }

        public async Task UpdateEngine(Engine engine)
        {
            ValidateEngine(engine);

            using var context = _dbFactory.CreateDbContext();

            if (engine.Id == 0)
                context.Add(engine);
            else
                context.Update(engine);

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
