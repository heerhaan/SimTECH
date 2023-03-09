using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class ManufacturerService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public ManufacturerService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Manufacturer>> GetManufacturers()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Manufacturer.ToListAsync();
        }

        public async Task UpdateManufacturer(Manufacturer manufacturer)
        {
            using var context = _dbFactory.CreateDbContext();

            if (manufacturer.Id == 0)
                context.Add(manufacturer);
            else
                context.Update(manufacturer);

            await context.SaveChangesAsync();
        }
    }
}
