using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.PageModels.Seasons;

namespace SimTECH.Data.Services;

public class SeasonDriverService
{
    private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

    public SeasonDriverService(IDbContextFactory<SimTechDbContext> factory)
    {
        _dbFactory = factory;
    }

    public async Task<List<SeasonDriver>> GetAllSeasonDrivers()
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.SeasonDriver
            .ToListAsync();
    }

    public async Task<List<SeasonDriver>> GetSeasonDrivers(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.SeasonDriver
            .Include(e => e.Driver)
                .ThenInclude(e => e.DriverTraits)
            .Where(e => e.SeasonId == seasonId)
            .ToListAsync();
    }

    public async Task<SeasonDriver?> FindRecentSeasonDriver(long driverId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.SeasonDriver
            .Where(e => e.DriverId == driverId && e.Season.State == State.Closed)
            .OrderByDescending(e => e.SeasonId)
            .FirstOrDefaultAsync();
    }

    public async Task<List<PreviousEntrantSetup<SeasonDriver>>> PreviousDriverSetups(long driverId)
    {
        using var context = _dbFactory.CreateDbContext();

        var seasonDrivers = await context.SeasonDriver
            .Include(e => e.SeasonTeam)
            .Where(e => e.DriverId == driverId && e.Season.State == State.Closed)
            .ToListAsync();

        var seasons = await context.Season
            .Include(e => e.League)
            .ToListAsync();

        var results = new List<PreviousEntrantSetup<SeasonDriver>>();

        foreach (var seasonDriver in seasonDrivers)
        {
            var season = seasons.Find(e => e.Id == seasonDriver.SeasonId);

            if (season == null)
                continue;

            results.Add(new()
            {
                LeagueId = season.LeagueId,
                LeagueName = season.League.Name,
                SeasonId = season.Id,
                SeasonYear = season.Year,
                MemberId = seasonDriver.SeasonTeam?.TeamId,
                Entrant = seasonDriver
            });
        }

        return results.OrderByDescending(e => e.SeasonYear).ToList();
    }

    public async Task UpdateSeasonDriver(SeasonDriver driver)
    {
        using var context = _dbFactory.CreateDbContext();

        if (driver.Id == 0)
            context.Add(driver);
        else
            context.Update(driver);

        await context.SaveChangesAsync();
    }

    public async Task SaveDriverDevelopment(Dictionary<long, int> developmentDict, Aspect target)
    {
        using var context = _dbFactory.CreateDbContext();

        var drivers = await context.SeasonDriver.Where(e => developmentDict.Keys.Contains(e.Id)).ToListAsync();

        foreach (var driver in drivers)
        {
            switch (target)
            {
                case Aspect.Skill: driver.Skill = developmentDict[driver.Id]; break;
                case Aspect.Reliability: driver.Reliability = developmentDict[driver.Id]; break;
                case Aspect.Attack: driver.Attack = developmentDict[driver.Id]; break;
                case Aspect.Defense: driver.Defense = developmentDict[driver.Id]; break;
                default: throw new InvalidOperationException("thats a wrong enum value buddy");
            }
        }

        context.UpdateRange(drivers);

        await context.SaveChangesAsync();
    }
}
