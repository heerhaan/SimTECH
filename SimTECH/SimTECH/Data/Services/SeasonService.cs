﻿using Microsoft.EntityFrameworkCore;
using MudBlazor;
using SimTECH.Common.Constants;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.PageModels.Seasons;

namespace SimTECH.Data.Services;

public class SeasonService(IDbContextFactory<SimTechDbContext> factory)
{
    private readonly IDbContextFactory<SimTechDbContext> _dbFactory = factory;

    public async Task<List<Season>> GetSeasons() => await GetSeasons(StateFilter.Default);
    public async Task<List<Season>> GetSeasons(StateFilter filter)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Season
            .Where(e => filter.StatesForFilter().Contains(e.State))
            .ToListAsync();
    }

    public async Task<Season> GetSeasonById(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Season
            .Include(e => e.PointAllotments)
            .Include(e => e.RaceClasses)
            .SingleAsync(e => e.Id == seasonId);
    }

    public async Task<Season?> FindRecentClosedSeason(long leagueId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Season
            .Include(e => e.RaceClasses)
            .Where(e => e.LeagueId == leagueId && e.State == State.Closed)
            .OrderByDescending(e => e.Id)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateSeason(Season season)
    {
        using var context = _dbFactory.CreateDbContext();

        if (season.Id == 0)
            context.Add(season);
        else
            context.Update(season);

        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteSeason(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        var season = await context.Season
            .Include(e => e.PointAllotments)
            .SingleAsync(e => e.Id == seasonId) 
            ?? throw new InvalidOperationException("No season related to ID found");
        if (season.State != State.Concept)
            throw new InvalidOperationException("Can only delete seasons which are in a concept state");

        var seasonDrivers = await context.SeasonDriver
            .Where(e => e.SeasonId == season.Id)
            .ToListAsync();
        var seasonTeams = await context.SeasonTeam
            .Where(e => e.SeasonId == season.Id)
            .ToListAsync();
        var seasonEngines = await context.SeasonEngine
            .Where(e => e.SeasonId == season.Id)
            .ToListAsync();
        var racesInSeason = await context.Race
            .Where(e => e.SeasonId == season.Id)
            .ToListAsync();

        context.RemoveRange(seasonDrivers);
        context.RemoveRange(seasonTeams);
        context.RemoveRange(seasonEngines);
        context.RemoveRange(racesInSeason);
        context.Remove(season);

        await context.SaveChangesAsync();

        return true;
    }

    // Don't forget to subtract contract durations if you call this method again elsewhere
    public async Task<string?> ActivateSeason(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        var season = await context.Season
            .Include(e => e.SeasonEngines)
            .Include(e => e.SeasonTeams)
            .Include(e => e.SeasonDrivers)
            .Include(e => e.Races)
            .Include(e => e.League)
            .SingleAsync(e => e.Id == seasonId);

        if (season.State != State.Concept)
            throw new InvalidOperationException("Can only activate seasons which are currently in 'concept'.");

        if (season.SeasonEngines?.Any() != true)
            return "this blitherin idiot right here forgot to add any entrants to this season.";
        if (season.SeasonTeams?.Any() != true)
            return "absolute moron forgot to add the teams, how are you going to race a motorsport season without any teams. come on mate";
        if (season.SeasonDrivers?.Any() != true)
            return "hey robocop this isn't the future, we still have people driving the cars. So add some fucking drivers will ya?";
        if (season?.Races?.Any() != true)
            return "hint of advice, a season without any races is hardly much of a season. Add races, pisshead";

        season.State = State.Active;

        context.Update(season);

        await context.SaveChangesAsync();

        return null;
    }

    public async Task FinishSeason(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        var season = await context.Season.SingleAsync(e => e.Id == seasonId);

        season.State = State.Closed;
        context.Update(season);

        await context.SaveChangesAsync();
    }

    public async Task CheckPenalties(List<Result> raceResults)
    {
        using var context = _dbFactory.CreateDbContext();

        var newPenalties = new List<GivenPenalty>();

        foreach (var dnfResult in raceResults.Where(e => e.Incident?.Penalized == true))
        {
            var incidentFrequency = await context.Result
                .Where(e => e.SeasonDriverId == dnfResult.SeasonDriverId && e.IncidentId == dnfResult.IncidentId)
                .CountAsync();
            if (incidentFrequency > dnfResult.Incident!.Limit)
            {
                newPenalties.Add(new GivenPenalty
                {
                    SeasonDriverId = dnfResult.SeasonDriverId,
                    IncidentId = dnfResult.IncidentId!.Value,
                });
            }
        }

        if (newPenalties.Count != 0)
        {
            context.AddRange(newPenalties);
            await context.SaveChangesAsync();
        }
    }

    #region entrant controls
    public async Task<List<DevelopmentLog>> GetDevelopmentLog(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.DevelopmentLog.Where(e => e.SeasonId == seasonId).ToListAsync();
    }

    public async Task SaveDevelopmentLog(List<DevelopmentLog> logs)
    {
        using var context = _dbFactory.CreateDbContext();

        context.DevelopmentLog.AddRange(logs);

        await context.SaveChangesAsync();
    }

    // List of season engines contains (or is expected to) have all related data
    public async Task PersistSeasonEntrants(List<SeasonEngine> rootEngines)
    {
        using var context = _dbFactory.CreateDbContext();

        context.AddRange(rootEngines);

        await context.SaveChangesAsync();
    }
    #endregion

    #region page models
    public async Task<List<SeasonListModel>> GetSeasonList()
    {
        using var context = _dbFactory.CreateDbContext();

        var seasons = await context.Season
            .Include(e => e.League)
            .ToListAsync();

        var winningDrivers = await context.SeasonDriver
            .Include(e => e.Driver)
            .Include(e => e.SeasonTeam)
            .GroupBy(e => e.SeasonId)
            .Select(e => e.OrderByDescending(o => o.Points).First())
            .ToListAsync();

        var winningTeams = await context.SeasonTeam
            .Include(e => e.Team)
            .GroupBy(e => e.SeasonId)
            .Select(e => e.OrderByDescending(o => o.Points).FirstOrDefault())
            .ToListAsync();

        var seasonListItems = new List<SeasonListModel>(seasons.Count);

        foreach (var season in seasons)
        {
            var seasonDriver = winningDrivers.Find(e => e.SeasonId == season.Id);
            var seasonTeam = winningTeams.Find(e => e!.SeasonId == season.Id);

            seasonListItems.Add(new SeasonListModel
            {
                Id = season.Id,
                Year = season.Year,
                State = season.State,
                League = season.League.Name,

                DriverName = seasonDriver?.Driver?.FullName ?? "[UNKNOWN]",
                DriverNumber = seasonDriver?.Number ?? 0,
                DriverNationality = seasonDriver?.Driver?.Country ?? Globals.DefaultCountry,
                DriverColour = seasonDriver?.SeasonTeam?.Colour ?? Globals.DefaultColour,
                DriverAccent = seasonDriver?.SeasonTeam?.Accent ?? Globals.DefaultAccent,

                TeamName = seasonTeam?.Name ?? "[UNKNOWN]",
                TeamNationality = seasonTeam?.Team?.Country ?? Globals.DefaultCountry,
                TeamColour = seasonTeam?.Colour ?? Globals.DefaultColour,
            });
        }

        return seasonListItems;
    }

    //public async Task<List<PowerRankingItem>> GetPowerRankingList(long seasonId, long raceClassId, long leagueId)
    //{
    //    using var context = _dbFactory.CreateDbContext();

    //    var league = await context.League.SingleAsync(e => e.Id == leagueId);

    //    var seasonDrivers = await context.SeasonDriver
    //        .Include(e => e.Driver)
    //            .ThenInclude(e => e.DriverTraits)
    //        .Where(e => e.SeasonId == seasonId
    //            && e.SeasonTeamId != null
    //            && (raceClassId == default || e.SeasonTeam!.ClassId == raceClassId))
    //        .ToListAsync();

    //    var seasonTeams = await context.SeasonTeam
    //        .Include(e => e.Team)
    //            .ThenInclude(e => e.TeamTraits)
    //        .Include(e => e.SeasonEngine)
    //        .Include(e => e.Manufacturer)
    //        .Where(e => seasonDrivers.Select(sd => sd.SeasonTeamId).Contains(e.Id))
    //        .ToListAsync();

    //    var allTraits = await context.Trait.ToListAsync();

    //    var resultList = new List<PowerRankingItem>(seasonDrivers.Count);

    //    foreach (var seasonDriver in seasonDrivers)
    //    {
    //        var team = seasonTeams.FirstOrDefault(e => e.Id == seasonDriver.SeasonTeamId);
    //        if (team == null) continue;

    //        var item = new PowerRankingItem
    //        {
    //            DriverName = seasonDriver.Driver.FullName,
    //            DriverNumber = seasonDriver.Number,
    //            Nationality = seasonDriver.Driver.Country,
    //            TeamName = team.Team.Name,
    //            Colour = team.Colour,
    //            Accent = team.Accent,
    //            Manufacturer = team.Manufacturer.Name,
    //            QualyPower = 0,
    //            RacePower = 0,
    //            Aero = team.g
    //        };

    //        resultList.Add(item);
    //    }

    //    return resultList;
    //}
    #endregion
}

