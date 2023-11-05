using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public class ClimateService : StateService<Climate>
    {
        public ClimateService(IDbContextFactory<SimTechDbContext> factory) : base(factory) { }

        public async Task<List<Climate>> GetClimates() => await GetClimates(StateFilter.Active);
        public async Task<List<Climate>> GetClimates(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Climate.Where(e => filter.StatesForFilter().Contains(e.State)).ToListAsync();
        }

        public async Task<Climate> GetClimateById(long climateId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Climate.FirstAsync(e => e.Id == climateId);
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
    }
}
