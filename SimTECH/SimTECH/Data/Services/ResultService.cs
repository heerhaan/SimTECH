using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services;

public class ResultService : BaseService<Result>
{
    public ResultService(IDbContextFactory<SimTechDbContext> factory) : base(factory) { }

    public async Task<List<Result>> GetResultsOfSeason(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Result
            .Where(e => e.Race.SeasonId == seasonId)
            .ToListAsync();
    }
}
