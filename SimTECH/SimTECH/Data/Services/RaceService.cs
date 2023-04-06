using Microsoft.EntityFrameworkCore;
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
                .Include(e => e.Track)
                .Where(e => e.SeasonId == seasonId)
                .ToListAsync();
        }

        public async Task<Race> GetRaceById(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Race
                .Include(e => e.Track)
                .Include(e => e.Penalties)
                .SingleAsync(e => e.Id == raceId);
        }

        public async Task<Race?> GetNextRaceOfSeason(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Race
                .Include(e => e.Track)
                .Where(e => e.SeasonId == seasonId && (e.State == State.Concept || e.State == State.Active || e.State == State.Advanced))
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

        public async Task ActivateRace(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            var race = await context.Race
                .SingleAsync(e => e.Id == raceId);

            if (race.State != State.Concept)
                throw new InvalidOperationException("Can only activate races in concept state");

            var seasonDrivers = await context.SeasonDriver
                .Where(e => e.SeasonId == race.SeasonId && e.SeasonTeamId.HasValue)
                .ToListAsync();

            var strategiesForRace = await context.Strategy
                .Include(e => e.StrategyTyres)
                    .ThenInclude(e => e.Tyre)
                .ToListAsync();

            if (strategiesForRace?.Any() != true)
                throw new InvalidOperationException("No valid strategies for this race, smh");

            var driverResults = new List<Result>();

            foreach (var driver in seasonDrivers)
            {
                var strategy = strategiesForRace[NumberHelper.RandomInt(strategiesForRace.Count - 1)];

                driverResults.Add(new Result
                {
                    Status = RaceStatus.Racing,
                    TyreLife = strategy.StrategyTyres[0].Tyre.Pace,
                    SeasonDriverId = driver.Id,
                    SeasonTeamId = driver.SeasonTeamId.GetValueOrDefault(),
                    RaceId = race.Id,
                    StrategyId = strategy.Id,
                });
            }

            context.AddRange(driverResults);

            var editModel = new EditRaceModel(race) { State = State.Active };
            var editedRecord = editModel.Record;

            context.Update(editedRecord);

            // TODO: Warning, is able to save duplicate season drivers to the results, refactor to check for this!
            await context.SaveChangesAsync();
        }

        public async Task PickStrategy(long resultId, long strategyId, int pace)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Result
                .Where(e => e.Id == resultId)
                .ExecuteUpdate(ex => ex
                    .SetProperty(prop => prop.StrategyId, strategyId)
                    .SetProperty(prop => prop.TyreLife, pace));

            await context.SaveChangesAsync();
        }

        public async Task PersistGridPositions(Dictionary<long, int> driverPositions, long? raceToAdvance = null, int? maximumRace = null)
        {
            using var context = _dbFactory.CreateDbContext();

            if (raceToAdvance != null && maximumRace == null)
                throw new InvalidOperationException("oi fucker, include the maximum allowed too!");

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

        public async Task PersistLapScores(Dictionary<long, List<int>> resultLapScores)
        {
            using var context = _dbFactory.CreateDbContext();

            var lapEntities = new List<LapScore>();

            foreach (var result in resultLapScores)
            {
                var resultScores = result.Value.ConvertAll(e => new LapScore { Score = e, ResultId = result.Key });
                lapEntities.AddRange(resultScores);
            }

            context.LapScore.AddRange(lapEntities);

            await context.SaveChangesAsync();
        }

        public async Task FinishRace(Race finishedRace, List<Result> finishedResults, List<ScoredPoints> scoredPoints)
        {
            using var context = _dbFactory.CreateDbContext();

            context.Update(finishedRace);
            context.UpdateRange(finishedResults);

            var seasonTeams = await context.SeasonTeam
                .Where(e => scoredPoints.Select(s => s.SeasonTeamId).Contains(e.Id))
                .ToListAsync();

            var seasonDrivers = await context.SeasonDriver
                .Where(e => scoredPoints.Select(s => s.SeasonDriverId).Contains(e.Id))
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

            context.UpdateRange(seasonTeams);
            context.UpdateRange(seasonDrivers);

            await context.SaveChangesAsync();
        }

        // Race, qualy and practice models are nearly the same but a generic solution did not came to me
        public async Task<RaceModel> RetrieveRaceModel(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            var race = await context.Race
                .Include(e => e.Track)
                    .ThenInclude(e => e.TrackTraits)
                .SingleAsync(e => e.Id == raceId);

            var season = await context.Season
                .Include(e => e.PointAllotments)
                .SingleAsync(e => e.Id == race.SeasonId);

            var driverResults = await context.Result
                .Where(e => e.RaceId == raceId && e.Status != RaceStatus.Dnq)
                .ToListAsync();

            var drivers = await context.SeasonDriver
                .Include(e => e.Driver)
                    .ThenInclude(e => e.DriverTraits)
                .Where(e => e.SeasonId == race.SeasonId)
                .ToListAsync();

            var teams = await context.SeasonTeam
                .Include(e => e.SeasonEngine)
                .Include(e => e.Team)
                    .ThenInclude(e => e.TeamTraits)
                .Where(e => e.SeasonId == race.SeasonId)
                .ToListAsync();

            // Excludes wet traits if the race isn't wet either
            var allTraits = await context.Trait
                .Where(e => (!e.ForWetConditions) && e.ForWetConditions == race.IsWet)
                .ToListAsync();

            var allStrategies = await context.Strategy
                .Include(e => e.StrategyTyres)
                    .ThenInclude(e => e.Tyre)
                .ToListAsync();

            var allManufacturers = await context.Manufacturer.ToListAsync();

            List<Trait> trackTraits;
            if (race.Track?.TrackTraits?.Any() == true)
                trackTraits = allTraits.Where(e => race.Track.TrackTraits.Select(tt => tt.TraitId).Contains(e.Id)).ToList();
            else
                trackTraits = new();

            var raceDrivers = new List<RaceDriver>();

            var lapCount = 50;

            // TODO: read the following from the config
            const double engineMultiplier = 0.9;
            const int weatherRng = 0;
            const int weatherDnf = 0;

            foreach (var driverResult in driverResults.Where(e => e.Status != RaceStatus.Dnq))
            {
                var driverTraits = new List<Trait>(trackTraits);

                var driver = drivers.Find(e => e.Id == driverResult.SeasonDriverId)
                    ?? throw new InvalidOperationException("Could not find matching seasondriver for result");
                var team = teams.Find(e => e.Id == driverResult.SeasonTeamId)
                    ?? throw new InvalidOperationException("Could not find matching seasonteam for result");
                var manufacturer = allManufacturers.Find(e => e.Id == team.ManufacturerId)
                    ?? throw new InvalidOperationException("Could not find matching manufacturer for driver");
                var strategy = allStrategies.Find(e => e.Id == driverResult.StrategyId)
                    ?? throw new InvalidOperationException("Could not find the strategy for driver");

                var enginePower = (team.SeasonEngine.Power * engineMultiplier).RoundDouble();

                int baseSpeed = driver.Skill + team.BaseValue + enginePower;
                double teamModifiers = (team.Aero * race.Track.AeroMod)
                    + (team.Chassis * race.Track.ChassisMod)
                    + (team.Powertrain * race.Track.PowerMod);

                var driverPower = baseSpeed + teamModifiers.RoundDouble();

                if (driver.Driver.DriverTraits?.Any() == true)
                    driverTraits.AddRange(allTraits.Where(e => driver.Driver.DriverTraits.Select(dt => dt.TraitId).Contains(e.Id)));
                if (team.Team.TeamTraits?.Any() == true)
                    driverTraits.AddRange(allTraits.Where(e => team.Team.TeamTraits.Select(dt => dt.TraitId).Contains(e.Id)));

                var sumTraits = NumberHelper.SumTraitEffects(driverTraits);

                var totalPower = sumTraits.DriverPace + driver.Skill + sumTraits.CarPace + team.BaseValue + teamModifiers.RoundDouble() + RetrieveStatusBonus(driver) + sumTraits.EnginePace + enginePower;
                var normalizedPowerCalc = (totalPower * lapCount);

                raceDrivers.Add(new RaceDriver
                {
                    ResultId = driverResult.Id,
                    SeasonDriverId = driverResult.SeasonDriverId,
                    SeasonTeamId = driverResult.SeasonTeamId,
                    FullName = driver.Driver.FullName,
                    Number = driver.Number,
                    Role = driver.TeamRole,
                    Nationality = driver.Driver.Country,
                    TeamName = team.Name,
                    Colour = team.Colour,
                    Accent = team.Accent,

                    Power = driverPower,

                    Status = driverResult.Status,
                    Incident = driverResult.Incident,

                    Grid = driverResult.Grid,
                    Setup = driverResult.Setup,
                    TyreLife = driverResult.TyreLife,

                    Strategy = strategy,
                    CurrentTyre = strategy.StrategyTyres[0].Tyre,

                    NormalizedPower = normalizedPowerCalc,
                    DriverReliability = sumTraits.DriverReliability + driver.Reliability + weatherDnf,
                    CarReliability = sumTraits.CarReliability + team.Reliability,
                    EngineReliability = sumTraits.EngineReliability + team.SeasonEngine.Reliability,
                    WearMaxMod = sumTraits.WearMaximum + manufacturer.WearMax,
                    WearMinMod = sumTraits.WearMinimum + manufacturer.WearMin,
                    RngMinMod = sumTraits.MinRNG + manufacturer.Pace,
                    RngMaxMod = sumTraits.MaxRNG + manufacturer.Pace + weatherRng,

                    Position = driverResult.Position,
                });
            }

            return new RaceModel
            {
                RaceId = race.Id,
                TrackId = race.TrackId,
                Name = race.Name,
                Country = race.Track?.Country ?? EnumHelper.GetDefaultCountry(),
                Weather = race.Weather,
                Round = race.Round,

                AmountRuns = lapCount,

                RaceDrivers = raceDrivers,

                Season = season
            };
        }

        // TODO: function can probably be placed elsewhere where it fits better
        private static int RetrieveStatusBonus(SeasonDriver driver)
        {
            if (driver.TeamRole == TeamRole.Main)
                return 2;
            else if (driver.TeamRole == TeamRole.Support)
                return -2;

            return 0;
        }

        public async Task<QualifyingModel> RetrieveQualifyingModel(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            var race = await context.Race
                .Include(e => e.Track)
                    .ThenInclude(e => e.TrackTraits)
                .SingleAsync(e => e.Id == raceId);

            var season = await context.Season.SingleAsync(e => e.Id == race.SeasonId);

            var driverResults = await context.Result
                .Where(e => e.RaceId == raceId)
                .ToListAsync();

            var drivers = await context.SeasonDriver
                .Include(e => e.Driver)
                    .ThenInclude(e => e.DriverTraits)
                .Where(e => e.SeasonId == race.SeasonId)
                .ToListAsync();

            var teams = await context.SeasonTeam
                .Include(e => e.SeasonEngine)
                .Include(e => e.Team)
                    .ThenInclude(e => e.TeamTraits)
                .Where(e => e.SeasonId == race.SeasonId)
                .ToListAsync();

            // Excludes wet traits if the race isn't wet either
            var allTraits = await context.Trait
                .Where(e => (!e.ForWetConditions) && e.ForWetConditions == race.IsWet)
                .ToListAsync();

            // Do we feel secure about these null refs?
            var trackTraits = allTraits
                .Where(e => race.Track.TrackTraits.Select(tt => tt.TraitId).Contains(e.Id))
                .ToList();

            var raceDrivers = new List<QualifyingDriver>();

            var amountRuns = 2;

            foreach (var driverResult in driverResults)
            {
                var driverTraits = new List<Trait>(trackTraits);

                var driver = drivers.Find(e => e.Id == driverResult.SeasonDriverId);
                var team = teams.Find(e => e.Id == driverResult.SeasonTeamId);

                if (driver == null)
                    throw new InvalidOperationException("Could not find matching seasondriver for result");
                if (team == null)
                    throw new InvalidOperationException("Could not find matching seasonteam for result");

                int baseSpeed = driver.Skill + team.BaseValue + team.SeasonEngine.Power;
                double teamModifiers = (team.Aero * race.Track.AeroMod)
                    + (team.Chassis * race.Track.ChassisMod)
                    + (team.Powertrain * race.Track.PowerMod);

                var driverPower = baseSpeed + teamModifiers.RoundDouble();

                if (driver.Driver.DriverTraits?.Any() == true)
                    driverTraits.AddRange(allTraits.Where(e => driver.Driver.DriverTraits.Select(dt => dt.TraitId).Contains(e.Id)));

                if (team.Team.TeamTraits?.Any() == true)
                    driverTraits.AddRange(allTraits.Where(e => team.Team.TeamTraits.Select(dt => dt.TraitId).Contains(e.Id)));

                raceDrivers.Add(new QualifyingDriver
                {
                    ResultId = driverResult.Id,
                    FullName = driver.Driver.FullName,
                    Number = driver.Number,
                    Role = driver.TeamRole,
                    Nationality = driver.Driver.Country,
                    TeamName = team.Name,
                    Colour = team.Colour,
                    Accent = team.Accent,

                    Power = driverPower,

                    TraitEffect = NumberHelper.SumTraitEffects(driverTraits),
                    RunValuesQ1 = new int[amountRuns],
                    RunValuesQ2 = new int[amountRuns],
                    RunValuesQ3 = new int[amountRuns],
                });
            }

            return new QualifyingModel
            {
                RaceId = race.Id,
                Name = race.Name,
                Country = race.Track.Country,
                Weather = race.Weather,

                AmountRuns = amountRuns,

                QualifyingDrivers = raceDrivers,

                Season = season
            };
        }

        public async Task<PracticeModel> RetrievePracticeModel(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            var race = await context.Race
                .Include(e => e.Track)
                    .ThenInclude(e => e.TrackTraits)
                .SingleAsync(e => e.Id == raceId);

            var driverResults = await context.Result
                .Where(e => e.RaceId == raceId)
                .ToListAsync();

            var drivers = await context.SeasonDriver
                .Include(e => e.Driver)
                    .ThenInclude(e => e.DriverTraits)
                .Where(e => e.SeasonId == race.SeasonId)
                .ToListAsync();

            var teams = await context.SeasonTeam
                .Include(e => e.SeasonEngine)
                .Include(e => e.Team)
                    .ThenInclude(e => e.TeamTraits)
                .Where(e => e.SeasonId == race.SeasonId)
                .ToListAsync();

            // Excludes wet traits if the race isn't wet either
            var allTraits = await context.Trait
                .Where(e => (!e.ForWetConditions) && e.ForWetConditions == race.IsWet)
                .ToListAsync();

            // Do we feel secure about these null refs?
            var trackTraits = allTraits
                .Where(e => race.Track.TrackTraits.Select(tt => tt.TraitId).Contains(e.Id))
                .ToList();

            var practiceDrivers = new List<PracticeDriver>();

            var amountRuns = 2;

            foreach (var driverResult in driverResults)
            {
                var driverTraits = new List<Trait>(trackTraits);

                var driver = drivers.Find(e => e.Id == driverResult.SeasonDriverId);
                var team = teams.Find(e => e.Id == driverResult.SeasonTeamId);

                if (driver == null)
                    throw new InvalidOperationException("Could not find matching seasondriver for result");
                if (team == null)
                    throw new InvalidOperationException("Could not find matching seasonteam for result");

                int baseSpeed = driver.Skill + team.BaseValue + team.SeasonEngine.Power;
                double teamModifiers = (team.Aero * race.Track.AeroMod)
                    + (team.Chassis * race.Track.ChassisMod)
                    + (team.Powertrain * race.Track.PowerMod);

                var driverPower = baseSpeed + teamModifiers.RoundDouble();

                if (driver.Driver.DriverTraits?.Any() == true)
                    driverTraits.AddRange(allTraits.Where(e => driver.Driver.DriverTraits.Select(dt => dt.TraitId).Contains(e.Id)));
                if (team.Team.TeamTraits?.Any() == true)
                    driverTraits.AddRange(allTraits.Where(e => team.Team.TeamTraits.Select(dt => dt.TraitId).Contains(e.Id)));

                practiceDrivers.Add(new PracticeDriver
                {
                    ResultId = driverResult.Id,
                    FullName = driver.Driver.FullName,
                    Number = driver.Number,
                    Role = driver.TeamRole,
                    Nationality = driver.Driver.Country,
                    TeamName = team.Name,
                    Colour = team.Colour,
                    Accent = team.Accent,

                    Power = driverPower,

                    RunValues = new int[amountRuns],
                });
            }

            return new PracticeModel
            {
                RaceId = race.Id,
                Name = race.Name,
                Country = race.Track.Country,
                Weather = race.Weather,

                AmountRuns = amountRuns,

                PracticeDrivers = practiceDrivers,
            };
        }

        // TODO: I want to make the reacemodel more generic, but how?!
        //private async Task<RaceModel> FillRaceModelBase<TDriver>(long raceId, List<DriverBase> participatingDrivers) { }

        #region single-purpose calls
        // TODO: LeagueID parameter
        public async Task<List<CopyRaceModel>> GetRacesToCopy()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Race
                .Where(e => e.Season.LeagueId == 1)
                .Select(e => new CopyRaceModel
                {
                    RaceId = e.Id,
                    Name = e.Name,
                    Country = e.Track.Country,
                    SeasonYear = e.Season.Year
                })
                .ToListAsync();
        }

        public async Task<RaceWeekModel> GetRaceWeekModel(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            var race = await context.Race
                .Include(e => e.Penalties)
                .Include(e => e.Track)
                .SingleAsync(e => e.Id == raceId);

            if (race.State == State.Concept)
                throw new InvalidOperationException("wrong state, reee. Activate first!!!");

            var trackTraits = await context.Trait
                .Where(trait => trait.TrackTraits.Any(tt => tt.TrackId == race.TrackId))
                .ToListAsync();

            var strategiesForRace = await context.Strategy
                .Include(e => e.StrategyTyres)
                    .ThenInclude(e => e.Tyre)
                .ToListAsync();

            var weekendDrivers = await context.Result
                .Include(e => e.SeasonDriver)
                    .ThenInclude(e => e.Driver)
                .Include(e => e.SeasonTeam)
                .Where(e => e.RaceId == raceId)
                .Select(result => new RaceWeekDriver
                {
                    SeasonDriverId = result.SeasonDriverId,
                    FullName = result.SeasonDriver.Driver.FullName,
                    Number = result.SeasonDriver.Number,
                    Role = result.SeasonDriver.TeamRole,
                    Country = result.SeasonDriver.Driver.Country,

                    SeasonTeamId = result.SeasonTeamId,
                    TeamName = result.SeasonTeam.Name,
                    Colour = result.SeasonTeam.Colour,
                    Accent = result.SeasonTeam.Accent,

                    ResultId = result.Id,
                    Grid = result.Grid,
                    Position = result.Position,
                    Status = result.Status,
                    StrategyId = result.StrategyId,
                })
                .ToListAsync();

            foreach (var driver in weekendDrivers)
                driver.Strategy = strategiesForRace.Find(e => e.Id == driver.StrategyId);

            if (weekendDrivers?.Any() != true)
                throw new InvalidOperationException("We're going to need some actual drivers too");

            return new RaceWeekModel
            {
                Race = race,
                RaceWeekDrivers = weekendDrivers,
                AvailableStrategies = strategiesForRace,
                TrackTraits = trackTraits,
            };
        }
        #endregion
    }
}
