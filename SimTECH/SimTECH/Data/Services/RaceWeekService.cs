using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.EditModels;
using SimTECH.Data.Models;
using SimTECH.Data.Services.Interfaces;
using SimTECH.PageModels.RaceWeek;

namespace SimTECH.Data.Services;

public class RaceWeekService(IDbContextFactory<SimTechDbContext> factory) : IRaceWeekService
{
    private readonly IDbContextFactory<SimTechDbContext> _dbFactory = factory;

    public async Task<List<Result>> GetResultsOfRace(long raceId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Result
            .Where(e => e.RaceId == raceId)
            .Include(e => e.LapScores)
            .ToListAsync();
    }

    public async Task<List<LapScore>> GetLapScores(long raceId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.LapScore
            .Where(e => e.Result.RaceId == raceId)
            .ToListAsync();
    }

    public async Task<List<QualifyingScore>> GetQualifyingScores(long raceId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.QualifyingScore
            .Where(e => e.Result.RaceId == raceId)
            .ToListAsync();
    }

    public async Task<List<PracticeScore>> GetPracticeScores(long raceId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.PracticeScore
            .Where(e => e.Result.RaceId == raceId)
            .ToListAsync();
    }

    public async Task<List<PracticeScore>> GetPracticeScores(long raceId, int index)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.PracticeScore
            .Where(e => e.Result.RaceId == raceId
                && e.Index == index)
            .ToListAsync();
    }

    //public async Task<RaceModel> GetRaceModel(long raceId)
    //{
    //    using var context = _dbFactory.CreateDbContext();

    //    var race = await context.Race.SingleAsync(e => e.Id == raceId);
    //}

    public async Task<List<Tyre>> GetValidTyresForRace(long leagueId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Tyre
            .Where(e => e.State == State.Active
                && e.LeagueTyres.Any(lt => lt.LeagueId == leagueId))
            .ToListAsync();
    }

    public async Task PickTyre(long resultId, Tyre tyre)
    {
        using var context = _dbFactory.CreateDbContext();

        var result = await context.Result.SingleAsync(e => e.Id == resultId);

        result.TyreId = tyre.Id;
        result.TyreLife = tyre.Pace;

        context.Update(result);

        await context.SaveChangesAsync();
    }

    public async Task PersistPracticeScores(List<PracticeScore> practiceScores)
    {
        using var context = _dbFactory.CreateDbContext();

        context.PracticeScore.AddRange(practiceScores);

        var resultIds = practiceScores.Select(e => e.ResultId).ToList();
        var driverResults = await context.Result.Where(e => resultIds.Contains(e.Id)).ToListAsync();

        foreach (var result in driverResults)
        {
            var sessionScore = practiceScores.First(e => e.ResultId == result.Id);

            result.Grid = sessionScore.Position;
            result.Position = sessionScore.Position;
            result.AbsoluteGrid = sessionScore.AbsolutePosition;
            result.AbsolutePosition = sessionScore.AbsolutePosition;
            result.Setup = sessionScore.SetupGained;
        }

        context.UpdateRange(driverResults);

        await context.SaveChangesAsync();
    }

    public async Task PersistQualifyingScores(List<QualifyingScore> qualyScores)
    {
        using var context = _dbFactory.CreateDbContext();

        context.QualifyingScore.AddRange(qualyScores);

        await context.SaveChangesAsync();
    }

    public async Task FinishQualifying(List<QualifyingScore> qualyScores, Dictionary<long, (int, int)> driverPositions, long raceToAdvance, int maximumRace)
    {
        using var context = _dbFactory.CreateDbContext();

        context.QualifyingScore.AddRange(qualyScores);

        var driverResults = await context.Result
            .Where(e => driverPositions.Keys.Contains(e.Id))
            .ToListAsync();

        foreach (var result in driverResults)
        {
            var sessionScore = driverPositions.First(e => e.Key == result.Id);
            var achievedPosition = sessionScore.Value;

            result.Grid = achievedPosition.Item1;
            result.Position = achievedPosition.Item1;

            // TODO: Solution underneath is shit and ugly
            result.AbsoluteGrid = achievedPosition.Item2;
            result.AbsolutePosition = achievedPosition.Item2;

            result.Status = achievedPosition.Item2 > maximumRace
                ? RaceStatus.Dnq
                : RaceStatus.Racing;
        }

        context.UpdateRange(driverResults);

        var race = await context.Race.SingleAsync(e => e.Id == raceToAdvance);

        var editModel = new EditRaceModel(race) { State = State.Advanced };
        var editedRecord = editModel.Record;

        context.Update(editedRecord);

        await context.SaveChangesAsync();

        // NOTE: this assumes that all drivers participating during a weekend will get their
        //       penalties consumed, even if they did not qualify.
        var foundSeasonDriverIds = driverResults.Select(e => e.SeasonDriverId).ToList();

        await LocalConsumePenalties(race.Id, foundSeasonDriverIds);
    }

    private async Task LocalConsumePenalties(long raceId, List<long> involvedDriverIds)
    {
        using var context = _dbFactory.CreateDbContext();

        var penalties = await GetRacePenalties(raceId);

        var consumablePenalties = await context.GivenPenalty
            .Where(e => e.Consumed == false
                && involvedDriverIds.Contains(e.SeasonDriverId))
            .ExecuteUpdateAsync(e =>
                e.SetProperty(p => p.Consumed, true)
                 .SetProperty(p => p.ConsumedAtRaceId, raceId));

        await context.SaveChangesAsync();
    }

    public async Task<List<RaceOccurrence>> GetRaceOccurrences(long raceId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.RaceOccurrence.Where(e => e.RaceId == raceId).ToListAsync();
    }

    public async Task<List<GivenPenalty>> GetRacePenalties(long raceId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.GivenPenalty
            .Where(e => !e.Consumed || e.ConsumedAtRaceId == raceId)
            .Include(e => e.Incident)
            .ToListAsync();
    }

    public async Task ConsumePenalties(List<long> consumables, long raceId)
    {
        using var context = _dbFactory.CreateDbContext();

        await context.GivenPenalty
            .Where(e => consumables.Contains(e.Id))
            .ExecuteUpdateAsync(e =>
                e.SetProperty(p => p.Consumed, true)
                    .SetProperty(p => p.ConsumedAtRaceId, raceId));

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

            if (incidentFrequency > dnfResult.Incident.Limit)
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

    public async Task PersistLapScores(List<LapScore> lapscores)
    {
        using var context = _dbFactory.CreateDbContext();

        context.LapScore.AddRange(lapscores);

        await context.SaveChangesAsync();
    }

    public async Task PersistOccurrences(List<RaceOccurrence> occurrences)
    {
        using var context = _dbFactory.CreateDbContext();

        context.RaceOccurrence.AddRange(occurrences);

        await context.SaveChangesAsync();
    }

    public async Task FinishRace(long raceId, List<Result> finishedResults, List<ScoredPoints> scoredPoints)
    {
        using var context = _dbFactory.CreateDbContext();

        var finishedRace = await context.Race.SingleAsync(e => e.Id == raceId);
        finishedRace.State = State.Closed;
        finishedRace.DateFinished = DateTime.UtcNow;

        context.Update(finishedRace);
        context.UpdateRange(finishedResults);

        var seasonTeams = await context.SeasonTeam
            .Where(e => scoredPoints.Select(s => s.SeasonTeamId).Contains(e.Id))
            .ToListAsync();

        var seasonDrivers = await context.SeasonDriver
            .Where(e => scoredPoints.Select(s => s.SeasonDriverId).Contains(e.Id))
            .Include(e => e.Driver)
            .ToListAsync();

        foreach (var scoredTeam in scoredPoints.GroupBy(e => e.SeasonTeamId))
        {
            var seasonTeam = seasonTeams.Single(e => e.Id == scoredTeam.Key);

            seasonTeam.Points += scoredTeam.Sum(e => e.Points);
            seasonTeam.HiddenPoints += scoredTeam.Sum(e => e.HiddenPoints);

            foreach (var scoredDriver in scoredTeam)
            {
                var seasonDriver = seasonDrivers.Single(e => e.Id == scoredDriver.SeasonDriverId);

                seasonDriver.Points += scoredDriver.Points;
                seasonDriver.HiddenPoints += scoredDriver.HiddenPoints;
            }
        }

        // Drop drivers which had a lethal crash
        foreach (var driverFatality in finishedResults.Where(e => e.Incident?.Category == IncidentCategory.Lethal))
        {
            var droppedDriver = seasonDrivers.Single(e => e.Id == driverFatality.SeasonDriverId);
            droppedDriver.SeasonTeamId = null;
        }

        context.UpdateRange(seasonTeams);
        context.UpdateRange(seasonDrivers);

        await context.SaveChangesAsync();
    }

    public async Task<RaweCeekModel> GetRaceWeekModel(long raceId)
    {
        using var context = _dbFactory.CreateDbContext();

        var raceWeekModel = new RaweCeekModel();

        var race = await context.Race.SingleAsync(e => e.Id == raceId);
        var season = await context.Season.SingleAsync(e => e.Id == race.SeasonId);
        var league = await context.League.SingleAsync(e => e.Id == season.LeagueId);
        var climate = await context.Climate.SingleAsync(e => e.Id == race.ClimateId);
        var track = await context.Track.SingleAsync(e => e.Id == race.TrackId);

        raceWeekModel.Race = race;
        raceWeekModel.Season = season;
        raceWeekModel.League = league;
        raceWeekModel.Climate = climate;

        raceWeekModel.RaweCeekDrivers = await GetRaceWeekDrivers(context);

        return raceWeekModel;
    }

    private async Task<List<RaweCeekDriver>> GetRaceWeekDrivers(SimTechDbContext context)
    {
        var raceweekDrivers = new List<RaweCeekDriver>();



        return raceweekDrivers;
    }
}
