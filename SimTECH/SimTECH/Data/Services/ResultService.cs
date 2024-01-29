using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services;

public class ResultService : BaseService<Result>
{
    public ResultService(IDbContextFactory<SimTechDbContext> factory) : base(factory) { }

    public async Task<List<Result>> GetAllResults()
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Result
            .ToListAsync();
    }

    public async Task<List<Result>> GetResultsOfLeague(long leagueId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Result
            .Where(e => e.Race.Season.LeagueId == leagueId)
            .ToListAsync();
    }

    public async Task<List<Result>> GetResultsOfSeason(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Result
            .Where(e => e.Race.SeasonId == seasonId)
            .ToListAsync();
    }

    public async Task<List<Result>> GetAllResultsOfDriver(long driverId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Result
            .Where(e => e.SeasonDriver.DriverId == driverId)
            .ToListAsync();
    }

    public async Task<List<Result>> GetAllDriverResultsForLeague(long driverId, long leagueId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Result
            .Where(e => e.SeasonDriver.DriverId == driverId
                && e.SeasonDriver.Season.LeagueId == leagueId)
            .ToListAsync();
    }
}
