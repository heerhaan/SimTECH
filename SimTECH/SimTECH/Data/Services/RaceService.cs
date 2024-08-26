using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Constants;
using SimTECH.Data.EditModels;
using SimTECH.Data.Models;
using SimTECH.Data.Services.Interfaces;
using SimTECH.Extensions;
using SimTECH.PageModels;

namespace SimTECH.Data.Services;

public class RaceService(IDbContextFactory<SimTechDbContext> factory) : IRaceService
{
    private readonly IDbContextFactory<SimTechDbContext> _dbFactory = factory;

    public async Task<List<Race>> GetRacesBySeason(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Race
            .Where(e => e.SeasonId == seasonId)
            .Include(e => e.Track)
            .ToListAsync();
    }

    public async Task<Race> GetRaceById(long raceId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Race
            .Include(e => e.Track)
                .ThenInclude(e => e.TrackTraits)
            .SingleAsync(e => e.Id == raceId);
    }

    public async Task<long?> GetRaceIdByRound(long seasonId, int round)
    {
        using var context = _dbFactory.CreateDbContext();

        var race = await context.Race
            .FirstOrDefaultAsync(e => e.SeasonId == seasonId && e.Round == round);

        return race?.Id;
    }

    public async Task<long?> GetNextRaceIdOfSeason(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Race
            .Where(e => e.SeasonId == seasonId && (e.State == State.Concept || e.State == State.Active || e.State == State.Advanced))
            .OrderBy(e => e.Round)
            .Select(e => e.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<Race?> GetNextRaceOfSeason(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Race
            .Where(e => e.SeasonId == seasonId && (e.State == State.Concept || e.State == State.Active || e.State == State.Advanced))
            .Include(e => e.Track)
            .OrderBy(e => e.Round)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateRace(Race race)
    {
        using var context = _dbFactory.CreateDbContext();

        if (race.Id == 0)
            context.Add(race);
        else
            context.Update(race);

        await context.SaveChangesAsync();
    }

    public async Task InsertRaces(List<Race> races)
    {
        using var context = _dbFactory.CreateDbContext();

        context.AddRange(races);

        await context.SaveChangesAsync();
    }

    public async Task DeleteRace(long raceId)
    {
        using var context = _dbFactory.CreateDbContext();

        var race = await context.Race.FirstAsync(e => e.Id == raceId);

        // Technically it's possible that this fails when penalties would be assigned, so yeah
        if (race.State == State.Concept)
            context.Remove(race);

        await context.SaveChangesAsync();

        var otherRaces = await context.Race
            .Where(e => e.SeasonId == race.SeasonId && e.Round > race.Round)
            .ExecuteUpdateAsync(e =>
                e.SetProperty(prop => prop.Round, prop => prop.Round - 1));
    }

    public async Task ActivateRace(long raceId)
    {
        using var context = _dbFactory.CreateDbContext();

        var race = await context.Race.Include(e => e.Climate).SingleAsync(e => e.Id == raceId);

        if (race.State != State.Concept)
            throw new InvalidOperationException("Can only activate races in concept state");

        var season = await context.Season.SingleAsync(e => e.Id == race.SeasonId);
        var leagueId = season.LeagueId;

        // Should we return any of this?
        var hasExistingResults = await context.Result.AnyAsync(e => e.RaceId == raceId);
        if (hasExistingResults)
            return;

        var seasonDrivers = await context.SeasonDriver
            .Include(e => e.Driver)
            .Where(e => e.SeasonId == race.SeasonId && e.SeasonTeamId.HasValue)
            .ToListAsync();

        var validLeagueTyres = await context.LeagueTyre.Where(e => e.LeagueId == leagueId).Select(e => e.TyreId).ToListAsync();
        var availableTyres = await context.Tyre
            .Where(e => e.State == State.Active
                && e.ForWet == race.Climate.IsWet
                && validLeagueTyres.Contains(e.Id))
            .ToListAsync();

        if (availableTyres?.Any() != true)
            throw new InvalidOperationException("No valid tyres for this race, smh");

        var driverResults = new List<Result>();

        foreach (var driver in seasonDrivers)
        {
            var tyre = driver.Driver.StrategyPreference switch
            {
                StrategyPreference.None => availableTyres.TakeRandomItem(),
                StrategyPreference.Softer => availableTyres.OrderByDescending(e => e.Pace).First(),
                StrategyPreference.Harder => availableTyres.OrderBy(e => e.Pace).First(),
                _ => throw new ArgumentOutOfRangeException($"Unrecognized type: {driver.Driver.StrategyPreference}"),
            };

            driverResults.Add(new Result
            {
                Status = RaceStatus.Racing,
                TyreLife = tyre.Pace,
                SeasonDriverId = driver.Id,
                SeasonTeamId = driver.SeasonTeamId.GetValueOrDefault(),
                RaceId = race.Id,
                TyreId = tyre.Id,
            });
        }

        context.AddRange(driverResults);

        var editModel = new EditRaceModel(race) { State = State.Active };
        var editedRecord = editModel.Record;

        context.Update(editedRecord);

        // TODO: Warning, is able to save duplicate season drivers to the results, refactor to check for this!
        await context.SaveChangesAsync();
    }

    #region single-purpose calls
    public async Task<List<FinishedRaceModel>> GetRecentlyFinishedCalendarRaces(int amount)
    {
        using var context = _dbFactory.CreateDbContext();

        // It's likely that this is a very unperformant implementation, consider a refactor
        return await context.Race
            .Where(e => e.State == State.Closed && e.DateFinished.HasValue && e.Results.Any())
            .OrderByDescending(e => e.DateFinished)
            .Take(amount)
            .Select(e => new FinishedRaceModel
            {
                RaceId = e.Id,
                Round = e.Round,
                Name = e.Name,
                Country = e.Track.Country,
                LeagueName = e.Season.League.Name,

                WinningDriver = e.Results.Where(e => e.Position == 1)
                    .Select(d => new CompactDriver
                    {
                        Name = d.SeasonDriver.Driver.FullName,
                        Country = d.SeasonDriver.Driver.Country,
                        Number = d.SeasonDriver.Number,
                        Colour = d.SeasonDriver.SeasonTeam == null ? Globals.DefaultColour : d.SeasonDriver.SeasonTeam.Colour,
                        Accent = d.SeasonDriver.SeasonTeam == null ? Globals.DefaultAccent : d.SeasonDriver.SeasonTeam.Accent
                    })
                    .First(),

                WinningTeam = e.Results.Where(e => e.Position == 1)
                    .Select(t => new CompactTeam
                    {
                        Name = t.SeasonTeam.Name,
                        Country = t.SeasonTeam.Team.Country,
                        Colour = t.SeasonTeam.Colour,
                        Accent = t.SeasonTeam.Accent
                    })
                    .First(),
            })
            .ToListAsync();
    }
    #endregion
}

