using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public sealed class ManufacturerService : StateService<Manufacturer>
    {
        public ManufacturerService(IDbContextFactory<SimTechDbContext> factory) : base(factory) { }

        public async Task<List<Manufacturer>> GetManufacturers() => await GetManufacturers(StateFilter.Default);
        public async Task<List<Manufacturer>> GetManufacturers(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Manufacturer
                .Where(e => filter.StatesForFilter().Contains(e.State))
                .ToListAsync();
        }

        public async Task UpdateManufacturer(Manufacturer manufacturer)
        {
            using var context = _dbFactory.CreateDbContext();

            if (manufacturer.Id == 0)
            {
                manufacturer.State = State.Active;
                context.Add(manufacturer);
            }
            else
                context.Update(manufacturer);

            await context.SaveChangesAsync();
        }

        public async Task DeleteManufacturer(Manufacturer manufacturer)
        {
            using var context = _dbFactory.CreateDbContext();

            if (context.SeasonTeam.Any(e => e.ManufacturerId == manufacturer.Id))
            {
                manufacturer.State = State.Archived;
                context.Update(manufacturer);
            }
            else
            {
                context.Remove(manufacturer);
            }

            await context.SaveChangesAsync();
        }
    }
}
