using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.PageModels.Seasons;

namespace SimTECH.Data.Services;

public class SeasonEngineService
{
    private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

    public SeasonEngineService(IDbContextFactory<SimTechDbContext> factory)
    {
        _dbFactory = factory;
    }

    public async Task<List<SeasonEngine>> GetSeasonEngines(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.SeasonEngine.Where(e => e.SeasonId == seasonId).ToListAsync();
    }

    public async Task<List<PreviousEntrantSetup<SeasonEngine>>> PreviousEngineSetups(long engineId)
    {
        using var context = _dbFactory.CreateDbContext();

        var seasonEngines = await context.SeasonEngine
            .Include(e => e.Season)
                .ThenInclude(e => e.League)
            .Where(e => e.EngineId == engineId && e.Season.State == State.Closed)
            .GroupBy(e => e.Season.LeagueId)
            .Take(5)
            .SelectMany(e => e)
            .ToListAsync();

        if (seasonEngines.Count > 0)
        {
            return seasonEngines
                .Select(e => new PreviousEntrantSetup<SeasonEngine>
                {
                    LeagueId = e.Season.LeagueId,
                    LeagueName = e.Season.League.Name,
                    SeasonId = e.SeasonId,
                    SeasonYear = e.Season.Year,
                    Entrant = e
                })
                .ToList();
        }

        return [];
    }

    public async Task<SeasonEngine?> FindRecentSeasonEngine(long engineId, long leagueId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.SeasonEngine
            .Where(e => e.EngineId == engineId && e.Season.LeagueId == leagueId && e.Season.State == State.Closed)
            .OrderByDescending(e => e.SeasonId)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateSeasonEngine(SeasonEngine engne)
    {
        using var context = _dbFactory.CreateDbContext();

        if (engne.Id == 0)
            context.Add(engne);
        else
            context.Update(engne);

        await context.SaveChangesAsync();
    }

    public async Task SaveEngineDevelopment(Dictionary<long, int> developmentDict, Aspect target)
    {
        using var context = _dbFactory.CreateDbContext();

        var engines = await context.SeasonEngine.Where(e => developmentDict.Keys.Contains(e.Id)).ToListAsync();

        foreach (var engine in engines)
        {
            if (target == Aspect.Engine)
                engine.Power = developmentDict[engine.Id];
            else
                engine.Reliability = developmentDict[engine.Id];
        }

        context.UpdateRange(engines);

        await context.SaveChangesAsync();
    }
}
