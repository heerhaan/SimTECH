using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Common.Constants;
using SimTECH.Data.Models;
using SimTECH.PageModels;
using SimTECH.PageModels.Entrants.Drivers;

namespace SimTECH.Data.Services;

public sealed class DriverService(IDbContextFactory<SimTechDbContext> factory) : StateService<Driver>(factory)
{
    public async Task<Driver> GetDriver(long id)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Driver
            .Include(e => e.DriverTraits)
            .SingleAsync(e => e.Id == id);
    }

    public async Task<List<Driver>> GetDrivers() => await GetDrivers(StateFilter.Default);
    public async Task<List<Driver>> GetDrivers(StateFilter filter)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Driver
            .Include(e => e.DriverTraits)
            .Where(e => filter.StatesForFilter().Contains(e.State))
            .ToListAsync();
    }

    public async Task<List<DriverListItem>> GetIndexListDrivers(bool archivedOnly = false)
    {
        using var context = _dbFactory.CreateDbContext();

        var query = context.Driver
            .Include(e => e.DriverTraits)
            .AsQueryable();

        if (archivedOnly)
            query = query.Where(e => e.State == State.Archived);
        else
            query = query.Where(e => e.State != State.Archived);

        var drivers = await query
            .Select(e => e.MapToListItem())
            .ToListAsync();

        var activeDrivers = await context.SeasonDriver
            .Where(e => e.Season.State == State.Active)
            .Select(e => new { e.DriverId, LeagueName = e.Season.League.Name })
            .ToListAsync();

        foreach (var driver in drivers)
        {
            driver.ActiveInLeague = activeDrivers
                .FirstOrDefault(e => e.DriverId == driver.Id)?.LeagueName
                ?? "None";
        }

        return drivers;
    }

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

    public async Task<List<Driver>> GetDriversInActiveLeague(long leagueId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Driver
            .Include(e => e.DriverTraits)
            .Where(e => e.State == State.Active
                && e.SeasonDrivers.Any(e => e.Season.LeagueId == leagueId && e.Season.State == State.Active))
            .ToListAsync();
    }

    public async Task<List<Driver>> GetAvailableDrivers(long seasonId, StateFilter filter = StateFilter.Active)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Driver
            .Include(e => e.DriverTraits)
            .Where(e => filter.StatesForFilter().Contains(e.State)
                && !e.SeasonDrivers.Any(sd => sd.SeasonId == seasonId))
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

            if (removeables.Count != 0)
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

    public async Task ArchiveDriver(long id)
    {
        using var context = _dbFactory.CreateDbContext();

        var driver = await context.Driver.SingleAsync(e => e.Id == id);

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

