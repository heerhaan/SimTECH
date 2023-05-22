using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public class ClimateService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public ClimateService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Climate>> GetClimates() => await GetClimates(StateFilter.Active);
        public async Task<List<Climate>> GetClimates(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Climate.Where(e => filter.StatesForFilter().Contains(e.State)).ToListAsync();
        }

        public async Task UpdateClimate(Climate climate)
        {
            using var context = _dbFactory.CreateDbContext();

            if (climate.Id == 0)
            {
                climate.State = State.Active;
                context.Add(climate);
            }
            else
            {
                context.Update(climate);
            }

            await context.SaveChangesAsync();
        }

        public async Task ChangeState(Climate climate, State targetState)
        {
            using var context = _dbFactory.CreateDbContext();

            climate.State = targetState;
            context.Update(climate);

            await context.SaveChangesAsync();
        }
    }
}
