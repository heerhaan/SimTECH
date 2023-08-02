using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Pages.Season;

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
}
