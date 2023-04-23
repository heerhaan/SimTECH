using Microsoft.EntityFrameworkCore;
using SimTECH.Data.EditModels;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public sealed class DriverService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public DriverService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Driver>> GetDrivers()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Driver
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
                // Looks a bit clumsy, but the model behind driver is record which are immutable
                var editModel = new EditDriverModel(driver)
                {
                    State = State.Archived
                };
                var modified = editModel.Record;

                context.Update(modified);
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

        public static void ValidateDriver(Driver driver)
        {
            throw new NotImplementedException();
        }
    }
}
