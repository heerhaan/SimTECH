using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimTECH.Data.EditModels;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels;

namespace SimTECH.Data.Services
{
    public class RaceService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public RaceService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

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

        public async Task<Race> GetRaceByRound(long seasonId, int round)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Race
                .FirstAsync(e => e.SeasonId == seasonId && e.Round == round);
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

            // Should we return any of this?
            var hasExistingResults = await context.Result.AnyAsync(e => e.RaceId == raceId);
            if (hasExistingResults)
                return;

            var seasonDrivers = await context.SeasonDriver
                .Where(e => e.SeasonId == race.SeasonId && e.SeasonTeamId.HasValue)
                .ToListAsync();

            var availableTyres = await context.Tyre
                .Where(e => e.State == State.Active && e.ForWet == race.Climate.IsWet)
                .ToListAsync();

            if (availableTyres?.Any() != true)
                throw new InvalidOperationException("No valid tyres for this race, smh");

            var driverResults = new List<Result>();

            foreach (var driver in seasonDrivers)
            {
                var tyre = availableTyres.TakeRandomItem();

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

        public async Task PickTyre(long resultId, Tyre tyre)
        {
            using var context = _dbFactory.CreateDbContext();

            var result = await context.Result.SingleAsync(e => e.Id == resultId);

            result.TyreId = tyre.Id;
            result.TyreLife = tyre.Pace;

            context.Update(result);

            await context.SaveChangesAsync();
        }

        public async Task PersistPracticeResults(Dictionary<long, int> driverPositions)
        {
            await PersistGridPositions(driverPositions, null, null);
        }
        public async Task PersistQualifyingResults(Dictionary<long, int> driverPositions, long raceToAdvance, int maximumRace)
        {
            await PersistGridPositions(driverPositions, raceToAdvance, maximumRace);
        }
        private async Task PersistGridPositions(Dictionary<long, int> driverPositions, long? raceToAdvance, int? maximumRace)
        {
            using var context = _dbFactory.CreateDbContext();

            if (raceToAdvance != null && maximumRace == null)
                throw new InvalidOperationException("oi fucker, include the maximum allowed too!");

            var positionCount = driverPositions.Count;
            var driverResults = await context.Result.Where(e => driverPositions.Keys.Contains(e.Id)).ToListAsync();

            foreach (var result in driverResults)
            {
                var achievedPosition = driverPositions[result.Id];
                result.Grid = achievedPosition;
                result.Position = achievedPosition;

                if (maximumRace != null)
                    result.Status = achievedPosition > maximumRace.Value ? RaceStatus.Dnq : RaceStatus.Racing;
            }

            context.UpdateRange(driverResults);

            if (raceToAdvance != null)
            {
                var race = await context.Race.SingleAsync(e => e.Id == raceToAdvance);

                var editModel = new EditRaceModel(race) { State = State.Advanced };
                var editedRecord = editModel.Record;

                context.Update(editedRecord);
            }

            await context.SaveChangesAsync();
        }

        public async Task PersistPracticeScores(List<PracticeScore> practiceScores)
        {
            using var context = _dbFactory.CreateDbContext();

            context.PracticeScore.AddRange(practiceScores);

            await context.SaveChangesAsync();
        }

        public async Task PersistQualifyingScores(List<QualifyingScore> qualyScores)
        {
            using var context = _dbFactory.CreateDbContext();

            context.QualifyingScore.AddRange(qualyScores);

            await context.SaveChangesAsync();
        }

        public async Task<List<GivenPenalty>> GetPenalties()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.GivenPenalty.Include(e => e.Incident).ToListAsync();
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

        public async Task<List<GivenPenalty>> GetUnconsumedPenalties()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.GivenPenalty
                .Where(e => !e.Consumed)
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
            foreach (var death in finishedResults.Where(e => e.Incident?.Category == CategoryIncident.Lethal))
            {
                var ripSeasonDriver = seasonDrivers.Single(e => e.Id == death.SeasonDriverId);
                ripSeasonDriver.SeasonTeamId = null;
            }

            context.UpdateRange(seasonTeams);
            context.UpdateRange(seasonDrivers);

            await context.SaveChangesAsync();
        }

        #region single-purpose calls
        public async Task<int> PracticeSessionsCompleted(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            var allPracticeScores = await context.PracticeScore.Where(e => e.RaceId == raceId).ToListAsync();
            return allPracticeScores.MaxBy(e => e.Index)?.Index ?? 0;
        }

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
                            Colour = d.SeasonDriver.SeasonTeam == null ? Constants.DefaultColour : d.SeasonDriver.SeasonTeam.Colour,
                            Accent = d.SeasonDriver.SeasonTeam == null ? Constants.DefaultAccent : d.SeasonDriver.SeasonTeam.Accent
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
}
