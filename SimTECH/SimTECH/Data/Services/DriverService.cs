using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels;
using System.Linq.Expressions;

namespace SimTECH.Data.Services
{
    public sealed class DriverService : StateService<Driver>
    {
        private readonly SimConfig _config;

        public DriverService(IDbContextFactory<SimTechDbContext> factory, IOptions<SimConfig> config) : base(factory)
        {
            _config = config.Value;
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
        // First test whether this impacts the performance significantly before (possibly) replacing the above
        //public async Task<List<Driver>> GetDrivers(Expression<Func<Driver, bool>>? selector = null, StateFilter filter = StateFilter.Default)
        //{
        //    selector ??= _ => true;

        //    using var context = _dbFactory.CreateDbContext();

        //    return await context.Driver
        //        .Where(e => filter.StatesForFilter().Contains(e.State))
        //        .Where(selector)
        //        .ToListAsync();
        //}

        public async Task<List<Driver>> GetAvailableDrivers()
        {
            using var context = _dbFactory.CreateDbContext();

            var query = context.Driver.Where(e => e.State == State.Active);
            if (!_config.AllowMultiLeagueEntry)
                query = query.Where(e => !e.SeasonDrivers!.Any(e => e.Season.State == State.Active));

            return await query.ToListAsync();
        }

        public async Task<List<CurrentDriver>> GetCurrentDrivers()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonDriver
                .Where(sd => sd.Season.State == State.Active)
                .Select(sd => new CurrentDriver
                {
                    SeasonDriverId = sd.Id,
                    DriverId = sd.DriverId,
                    League = sd.Season.League.Name,
                    Colour = sd.SeasonTeam == null ? Constants.DefaultColour : sd.SeasonTeam.Colour
                })
                .ToListAsync();
        }

        public async Task<List<Driver>> GetLeagueDrivers(long leagueId) => await GetLeagueDrivers(leagueId, StateFilter.Default);
        public async Task<List<Driver>> GetLeagueDrivers(long leagueId, StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Driver
                .Where(e => filter.StatesForFilter().Contains(e.State) && e.SeasonDrivers!.Any(e => e.Season.LeagueId == leagueId))
                .Include(e => e.DriverTraits)
                .ToListAsync();
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

        public async Task ArchiveDriver(Driver driver)
        {
            using var context = _dbFactory.CreateDbContext();

            if (driver.State == State.Archived)
            {
                driver.State = State.Active;
            }
            else
            {
                if (context.SeasonDriver.Any(e => e.DriverId == driver.Id))
                {
                    driver.State = State.Archived;
                    context.Update(driver);
                }
                else
                {
                    context.Remove(driver);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
