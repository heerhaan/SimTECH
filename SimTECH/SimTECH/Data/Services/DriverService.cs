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

            return await context.Driver.Include(e => e.DriverTraits).ToListAsync();
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
            {
                context.Add(driver);
            }
            else
            {
                var removeables = await context.DriverTrait
                        .Where(e => e.DriverId == driver.Id)
                        .ToListAsync();

                if (removeables.Any())
                    context.RemoveRange(removeables);

                if (driver.DriverTraits?.Any() ?? false)
                    context.AddRange(driver.DriverTraits);

                context.Update(driver);
            }

            await context.SaveChangesAsync();
        }

        public static void ValidateDriver(Driver driver)
        {
            throw new NotImplementedException();
        }
    }
}
