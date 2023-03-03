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

        public async Task CreateManufacturer(Manufacturer manufacturer)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Add(manufacturer);

            await context.SaveChangesAsync();
        }
    }
}
