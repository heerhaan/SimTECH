using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public sealed class DriverService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public DriverService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Driver>> GetDrivers() => await GetDrivers(StateFilter.Default);
        public async Task<List<Driver>> GetDrivers(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Driver
                .Where(e => filter.StatesForFilter().Contains(e.State))
                .Include(e => e.DriverTraits)
                .ToListAsync();
        }

        public async Task<Driver> GetDriverById(long driverId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Driver
                .Include(e => e.DriverTraits)
                .SingleAsync(e => e.Id == driverId);
        }

        public async Task UpdateDriver(Driver driver)
        {
            using var context = _dbFactory.CreateDbContext();

            if (driver.Id == 0)
            {
                driver.State = State.Active;
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

        public async Task AddNewDrivers(Driver[] drivers)
        {
            using var context = _dbFactory.CreateDbContext();

            context.AddRange(drivers);

            await context.SaveChangesAsync();
        }

        public async Task DeleteDriver(Driver driver)
        {
            using var context = _dbFactory.CreateDbContext();

            if (context.SeasonDriver.Any(e => e.DriverId == driver.Id))
            {
                driver.State = State.Archived;
                context.Update(driver);
            }
            else
            {
                context.Remove(driver);
            }

            await context.SaveChangesAsync();
        }

        public async Task<List<Result>> GetAllResults(long driverId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Result
                .Where(e => e.Race.State == State.Closed && e.SeasonDriver.Driver.Id == driverId)
                .ToListAsync();
        }
    }
}
