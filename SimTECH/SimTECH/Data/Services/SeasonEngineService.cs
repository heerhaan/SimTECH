﻿using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.PageModels.Seasons;
using SimTECH.PageModels.Stats;

namespace SimTECH.Data.Services;

public class SeasonEngineService(IDbContextFactory<SimTechDbContext> factory)
{
    private const int amountPreviousLeagues = 5;

    private readonly IDbContextFactory<SimTechDbContext> _dbFactory = factory;

    public async Task<List<SeasonEngine>> GetSeasonEngines(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.SeasonEngine.Include(e => e.Engine).Where(e => e.SeasonId == seasonId).ToListAsync();
    }

    public async Task<List<PreviousEntrantSetup<SeasonEngine>>> PreviousEngineSetups(long engineId)
    {
        using var context = _dbFactory.CreateDbContext();

        var seasonEngines = await context.SeasonEngine
            .Include(e => e.Season)
                .ThenInclude(e => e.League)
            .Where(e => e.EngineId == engineId && e.Season.State == State.Closed)
            .GroupBy(e => e.Season.LeagueId)
            .Take(amountPreviousLeagues)
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
            .Where(e => e.EngineId == engineId
                && e.Season.LeagueId == leagueId
                && e.Season.State == State.Closed)
            .OrderByDescending(e => e.SeasonId)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateSeasonEngine(SeasonEngine engine)
    {
        using var context = _dbFactory.CreateDbContext();

        if (engine.Id == 0)
            context.Add(engine);
        else
            context.Update(engine);

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

    public async Task<List<ChartData>> GetSeasonDevelopmentProgressionData(long seasonId, Aspect aspect)
    {
        using var context = _dbFactory.CreateDbContext();

        var dataSets = new List<ChartData>();

        var developLog = await context.DevelopmentLog
            .Where(e => e.SeasonId == seasonId
                && e.EntrantGroup == Entrant.Engine
                && e.DevelopedAspect == aspect)
            .ToListAsync();

        if (developLog.Count == 0)
            return dataSets;

        var seasonEngines = await context.SeasonEngine
            .Where(e => e.SeasonId == seasonId)
            .ToListAsync();

        var maxRoundCount = developLog.Select(e => e.AfterRound).Max();

        foreach (var engine in seasonEngines)
        {
            var dataSet = new ChartData
            {
                Label = engine.Name,
                Stroke = new ApexCharts.SeriesStroke
                {
                    Width = 3,
                }
            };

            var engineLog = developLog.Where(e => e.EntrantId == engine.Id).ToList();

            var change = aspect.GetAspectEngineValue(engine) - engineLog.Select(e => e.Change).Sum();

            for (int i = 0; i <= maxRoundCount; i++)
            {
                var data = engineLog.Where(e => e.AfterRound == i).ToList();

                if (data.Count != 0)
                    change += data.Sum(e => e.Change);

                dataSet.DataPoints.Add(new(i, change));
            }

            dataSets.Add(dataSet);
        }

        return dataSets;
    }
}
