using Microsoft.EntityFrameworkCore;
using SimTECH.Data.EditModels;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public class TyreService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public TyreService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
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

        public async Task ChangeState(Tyre tyre, State targetState)
        {
            using var context = _dbFactory.CreateDbContext();

            tyre.State = targetState;
            context.Update(tyre);

            await context.SaveChangesAsync();
        }
    }
}
