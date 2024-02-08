using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.PageModels;
using SimTECH.PageModels.Entrants.Drivers;

namespace SimTECH.Data.Services;

public sealed class DriverService : StateService<Driver>
{
    public DriverService(IDbContextFactory<SimTechDbContext> factory) : base(factory) { }

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

    public async Task<List<CurrentDriver>> GetDriversInActiveSeason()
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.SeasonDriver
            .Where(sd => sd.Season.State == State.Active)
            .Select(sd => new CurrentDriver
            {
                SeasonDriverId = sd.Id,
                DriverId = sd.DriverId,
                LeagueName = sd.Season.League.Name,
                Colour = sd.SeasonTeam == null ? Globals.DefaultColour : sd.SeasonTeam.Colour
            })
            .ToListAsync();
    }

    public async Task<List<DriverListItem>> GetIndexListDrivers(bool archivedOnly = false)
    {
        using var context = _dbFactory.CreateDbContext();

        var activeDrivers = await context.SeasonDriver
            .Where(e => e.Season.State == State.Active)
            .ToListAsync();

        return [];
    }

    public async Task<List<Driver>> GetDriversFromLeague(long leagueId) => await GetDriversFromLeague(leagueId, StateFilter.Default);
    public async Task<List<Driver>> GetDriversFromLeague(long leagueId, StateFilter filter)
    {
        using var context = _dbFactory.CreateDbContext();

        var seasons = await context.Season
            .Where(e => e.LeagueId == leagueId)
            .Include(e => e.SeasonDrivers)
            .ToListAsync();

        var mostRecent = seasons.Find(e => e.State == State.Active)
            ?? seasons.OrderByDescending(e => e.Year).FirstOrDefault();

        if (mostRecent == null)
        {
            return await context.Driver
                .Where(e => filter.StatesForFilter().Contains(e.State))
                .Include(e => e.DriverTraits)
                .ToListAsync();
        }

        return await context.Driver
            .Where(e => filter.StatesForFilter().Contains(e.State)
                && e.SeasonDrivers.Any(e => e.SeasonId == mostRecent.Id))
            .Include(e => e.DriverTraits)
            .ToListAsync();
    }

    public async Task<List<Driver>> GetAvailableDrivers(long seasonId, StateFilter filter = StateFilter.Active)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Driver
            .Where(e => filter.StatesForFilter().Contains(e.State)
                && !e.SeasonDrivers.Any(sd => sd.SeasonId == seasonId))
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

    public async Task BulkAddDrivers(Driver[] drivers)
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

