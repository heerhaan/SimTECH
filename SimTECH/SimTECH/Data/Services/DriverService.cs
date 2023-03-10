using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class DriverService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public DriverService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Driver>> GetDrivers()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Driver.ToListAsync();
        }

        public async Task<Driver?> GetDriverById(long driverId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Driver.FirstOrDefaultAsync(e => e.Id == driverId);
        }

        public async Task UpdateDriver(Driver driver)
        {
            using var context = _dbFactory.CreateDbContext();

            if (driver.Id == 0)
                context.Add(driver);
            else
                context.Update(driver);

            await context.SaveChangesAsync();
        }
    }
}
