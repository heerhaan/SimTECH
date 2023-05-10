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
        private readonly SimConfig _config;

        public RaceService(IDbContextFactory<SimTechDbContext> factory, IOptions<SimConfig> config)
        {
            _dbFactory = factory;
            _config = config.Value;
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
                .SingleAsync(e => e.Id == raceId);
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
        {//TODO: Validations?
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
                .Where(e => e.State == State.Active)
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

        public async Task ConsumePenalties(List<long> consumables, long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            await context.GivenPenalty
                .Where(e => consumables.Contains(e.Id))
                .ExecuteUpdateAsync(e =>
                    e.SetProperty(p => p.Consumed, p => true)
                     .SetProperty(p => p.ConsumedAtRaceId, p => raceId));

            await context.SaveChangesAsync();
        }

        public async Task PersistLapScores(List<LapScore> lapscores)
        {
            using var context = _dbFactory.CreateDbContext();

            // Validation?

            context.LapScore.AddRange(lapscores);

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

            // De-activate the drivers which had a lethal crash
            foreach (var death in finishedResults.Where(e => e.Incident?.Category == CategoryIncident.Lethal))
            {
                var ripSeasonDriver = seasonDrivers.Single(e => e.Id == death.SeasonDriverId);
                ripSeasonDriver.SeasonTeamId = null;
                ripSeasonDriver.Driver.Alive = false;
            }

            context.UpdateRange(seasonTeams);
            context.UpdateRange(seasonDrivers);

            await context.SaveChangesAsync();
        }

        #region single-purpose calls
        public async Task<RaceWeekModel> GetRaceWeekModel(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            var race = await context.Race
                .Include(e => e.Track)
                .Include(e => e.Climate)
                .Include(e => e.Season)
                .SingleAsync(e => e.Id == raceId);

            if (race.State == State.Concept)
                throw new InvalidOperationException("wrong state, reee. Activate first!!!");

            var trackTraits = await context.Trait
                .Where(trait => trait.TrackTraits.Any(tt => tt.TrackId == race.TrackId))
                .ToListAsync();

            // Prepare the drivers for this weekend
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

            if (weekendDrivers?.Any() != true)
                throw new InvalidOperationException("We're going to need some actual drivers too");

            // Set strategies
            var strategiesForRace = await context.Strategy
                .Include(e => e.StrategyTyres)
                    .ThenInclude(e => e.Tyre)
                .ToListAsync();

            foreach (var driver in weekendDrivers)
                driver.Strategy = strategiesForRace.Find(e => e.Id == driver.StrategyId);

            // Set eventual penalties
            var unconsumedPenalties = await context.GivenPenalty.Where(e => !e.Consumed || e.ConsumedAtRaceId == raceId).ToListAsync();
            if (unconsumedPenalties?.Any() == true)
            {
                foreach (var penalty in unconsumedPenalties.GroupBy(e => e.SeasonDriverId))
                {
                    var matchingDriver = weekendDrivers.SingleOrDefault(e => e.SeasonDriverId == penalty.Key);
                    if (matchingDriver != null)
                    {
                        matchingDriver.Penalty = penalty.Sum(e => e.Incident.Punishment);
                        matchingDriver.Reasons = string.Join(", ", penalty.Select(e => e.Incident.Name));
                    }
                }
            }

            return new RaceWeekModel
            {
                Race = race,
                MaximumInRace = race.Season.MaximumDriversInRace,
                RaceWeekDrivers = weekendDrivers,
                AvailableStrategies = strategiesForRace,
                TrackTraits = trackTraits,
            };
        }

        public async Task<List<CalendarRaceModel>> GetRaceCalendar(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            var races = await context.Race
                .Where(e => e.SeasonId == seasonId)
                .Include(e => e.Climate)
                .Include(e => e.Track)
                .Include(e => e.Results)
                .ToListAsync();

            var drivers = await context.SeasonDriver
                .Where(e => e.SeasonId == seasonId)
                .Include(e => e.Driver)
                .ToListAsync();

            var teams = await context.SeasonTeam
                .Where(e => e.SeasonId == seasonId)
                .Include(e => e.Team)
                .ToListAsync();

            var calendar = new List<CalendarRaceModel>(races.Count);

            foreach (var race in races.OrderBy(e => e.Round))
            {
                var calendarModel = new CalendarRaceModel
                {
                    Id = race.Id,
                    Round = race.Round,
                    Name = race.Name,
                    Country = race.Track.Country,
                    Weather = race.Climate.Terminology,
                    WeatherIcon = race.Climate.Icon,
                    State = race.State,
                    TrackId = race.TrackId,
                };

                if (race.Results?.Any() == true)
                {
                    var poleResult = race.Results.FirstOrDefault(e => e.Grid == 1);

                    if (poleResult != null)
                    {
                        var poleDriver = drivers.Single(e => e.Id == poleResult.SeasonDriverId);
                        var poleTeam = teams.Single(e => e.Id == poleResult.SeasonTeamId);

                        calendarModel.PoleSitter = new DriverWinner
                        {
                            Name = poleDriver.Driver.FullName,
                            Country = poleDriver.Driver.Country,
                            Number = poleDriver.Number,
                            Colour = poleTeam.Colour,
                            Accent = poleTeam.Accent
                        };
                    }

                    var winningResult = race.Results.FirstOrDefault(e => e.Position == 1);

                    if (winningResult != null)
                    {
                        var winningDriver = drivers.Single(d => winningResult.SeasonDriverId == d.Id);
                        var driverTeam = teams.Find(e => e.Id == winningDriver.SeasonTeamId.GetValueOrDefault());

                        calendarModel.DriverWinner = new DriverWinner
                        {
                            Name = winningDriver.Driver.FullName,
                            Country = winningDriver.Driver.Country,
                            Number = winningDriver.Number,
                        };

                        if (driverTeam != null)
                        {
                            calendarModel.DriverWinner.Colour = driverTeam.Colour;
                            calendarModel.DriverWinner.Accent = driverTeam.Accent;
                        }

                        var winningTeam = teams.Single(t => winningResult.SeasonTeamId == t.Id);

                        calendarModel.TeamWinner = new TeamWinner
                        {
                            Name = winningTeam.Name,
                            Colour = winningTeam.Colour,
                            Accent = winningTeam.Accent,
                        };
                    }
                }

                calendar.Add(calendarModel);
            }

            return calendar;
        }

        public async Task<List<FinishedRaceModel>> GetRecentlyFinishedCalendarRaces(int amount)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Race
                .Where(e => e.State == State.Closed && e.Results.Any())
                .TakeLastSpecial(amount)
                .Select(e => new FinishedRaceModel
                {
                    Round = e.Round,
                    Name = e.Name,
                    Country = e.Track.Country,
                    LeagueName = e.Season.League.Name,

                    WinningDriver = e.Results.Select(d => new DriverWinner
                    {
                        Name = d.SeasonDriver.Driver.FullName,
                        Country = d.SeasonDriver.Driver.Country,
                        Number = d.SeasonDriver.Number,
                        Colour = d.SeasonDriver.SeasonTeam == null ? Constants.DefaultColour : d.SeasonDriver.SeasonTeam.Colour,
                        Accent = d.SeasonDriver.SeasonTeam == null ? Constants.DefaultAccent : d.SeasonDriver.SeasonTeam.Accent
                    })
                    .First(),

                    WinningTeam = e.Results.Select(t => new TeamWinner
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

        // TODO: Initially I wanted to make the racemodel more generic, can we still do that after all the changes?
        //private async Task<RaceModel> FillRaceModelBase<TDriver>(long raceId, List<DriverBase> participatingDrivers) { }

        // Race, qualy and practice models are nearly the same but a generic solution did not came to me
        public async Task<RaceModel> RetrieveRaceModel(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            var race = await context.Race
                .Include(e => e.Climate)
                .Include(e => e.Track)
                    .ThenInclude(e => e.TrackTraits)
                .SingleAsync(e => e.Id == raceId);

            var season = await context.Season
                .Include(e => e.League)
                .Include(e => e.PointAllotments)
                .SingleAsync(e => e.Id == race.SeasonId);

            var driverResults = await context.Result
                .Where(e => e.RaceId == raceId && e.Status != RaceStatus.Dnq)
                .Include(e => e.LapScores)
                .ToListAsync();

            var drivers = await context.SeasonDriver
                .Where(e => e.SeasonId == race.SeasonId)
                .Include(e => e.Driver)
                    .ThenInclude(e => e.DriverTraits)
                .ToListAsync();

            var teams = await context.SeasonTeam
                .Where(e => e.SeasonId == race.SeasonId)
                .Include(e => e.SeasonEngine)
                .Include(e => e.Manufacturer)
                .Include(e => e.Team)
                    .ThenInclude(e => e.TeamTraits)
                .ToListAsync();

            // Excludes wet traits if the race isn't wet either
            var allTraits = await context.Trait
                .Where(e => (!e.ForWetConditions) || e.ForWetConditions == race.Climate.IsWet)
                .ToListAsync();

            var allStrategies = await context.Strategy
                .Include(e => e.StrategyTyres)
                    .ThenInclude(e => e.Tyre)
                .ToListAsync();

            List<Trait> trackTraits;
            if (race.Track?.TrackTraits?.Any() == true)
                trackTraits = allTraits.Where(e => race.Track.TrackTraits.Select(tt => tt.TraitId).Contains(e.Id)).ToList();
            else
                trackTraits = new();

            var raceDrivers = new List<RaceDriver>();

            // Set weather multipliers defined in the climate here
            var engineMultiplier = race.Climate.EngineMultiplier;
            var weatherRng = race.Climate.RngModifier;
            var weatherDnf = race.Climate.ReliablityModifier;

            // Iterate through all driver results for this raceweek, excluding the drivers which failed to qualify
            foreach (var driverResult in driverResults.Where(e => e.Status != RaceStatus.Dnq))
            {
                var driverTraits = new List<Trait>(trackTraits);

                var driver = drivers.Find(e => e.Id == driverResult.SeasonDriverId) ?? throw new InvalidOperationException("Could not find matching seasondriver for result");
                var team = teams.Find(e => e.Id == driverResult.SeasonTeamId) ?? throw new InvalidOperationException("Could not find matching seasonteam for result");
                var strategy = allStrategies.Find(e => e.Id == driverResult.StrategyId) ?? throw new InvalidOperationException("Could not find the strategy for driver");

                if (team.Manufacturer == null)
                    throw new InvalidOperationException("where the tyre manufacturer at boi?");

                if (driver.Driver.DriverTraits?.Any() == true)
                    driverTraits.AddRange(allTraits.Where(e => driver.Driver.DriverTraits.Select(dt => dt.TraitId).Contains(e.Id)));
                if (team.Team.TeamTraits?.Any() == true)
                    driverTraits.AddRange(allTraits.Where(e => team.Team.TeamTraits.Select(dt => dt.TraitId).Contains(e.Id)));

                double teamModifiers = (team.Aero * race.Track?.AeroMod ?? 1)
                    + (team.Chassis * race.Track?.ChassisMod ?? 1)
                    + (team.Powertrain * race.Track?.PowerMod ?? 1);

                var sumTraits = NumberHelper.SumTraitEffects(driverTraits);
                var driverPower = driver.Skill + driver.RetrieveStatusBonus(_config.CarDriverStatusModifier);
                var carPower = team.BaseValue + teamModifiers.RoundDouble();
                var enginePower = (team.SeasonEngine.Power * engineMultiplier).RoundDouble();
                var totalPower = driverPower + carPower + enginePower + sumTraits.RacePace;

                var actualDefense = ((driver.Defense + sumTraits.Defense) * race.Track?.DefenseMod ?? 1.0).RoundDouble();

                raceDrivers.Add(new RaceDriver
                {
                    ResultId = driverResult.Id,
                    SeasonDriverId = driverResult.SeasonDriverId,
                    SeasonTeamId = driverResult.SeasonTeamId,
                    FirstName = driver.Driver.FirstName,
                    LastName = driver.Driver.LastName,
                    FullName = driver.Driver.FullName,
                    Number = driver.Number,
                    Role = driver.TeamRole,
                    Nationality = driver.Driver.Country,
                    TeamName = team.Name,
                    Colour = team.Colour,
                    Accent = team.Accent,

                    Status = driverResult.Status,
                    Incident = driverResult.Incident,

                    Grid = driverResult.Grid,
                    Setup = driverResult.Setup,
                    TyreLife = driverResult.TyreLife,

                    Strategy = strategy,
                    // We might want to store the current tyre ID too otherwise this makes little sense when opening an older rees
                    // Also this way one has to finish a started race
                    // Alternatively determine the current tyre based on the state of the race
                    CurrentTyre = strategy.StrategyTyres[0].Tyre,

                    Power = totalPower,
                    Attack = driver.Attack + sumTraits.Attack,
                    Defense = actualDefense,
                    DriverReliability = driver.Reliability + sumTraits.DriverReliability + weatherDnf,
                    CarReliability = team.Reliability + sumTraits.CarReliability,
                    EngineReliability = team.SeasonEngine.Reliability + sumTraits.EngineReliability,
                    WearMaxMod = team.Manufacturer.WearMax + sumTraits.WearMaximum,
                    WearMinMod = team.Manufacturer.WearMin + sumTraits.WearMinimum,
                    RngMinMod = team.Manufacturer.Pace + sumTraits.MinRNG,
                    RngMaxMod = team.Manufacturer.Pace + sumTraits.MaxRNG + weatherRng,

                    Position = driverResult.Position,

                    LapScores = driverResult.LapScores?.ToList() ?? new(),
                });
            }

            return new RaceModel
            {
                RaceId = race.Id,
                TrackId = race.TrackId,
                TrackLength = race.Track?.Length ?? 0,
                DefenseMod = race.Track?.DefenseMod ?? 1.0,
                RaceLength = race.RaceLength,
                Name = race.Name,
                Country = race.Track?.Country ?? EnumHelper.GetDefaultCountry(),
                ClimateId = race.ClimateId,
                Climate = race.Climate.Terminology,
                ClimateIcon = race.Climate.Icon,
                Round = race.Round,
                IsFinished = race.State == State.Closed,

                RaceDrivers = raceDrivers,

                Season = season,
                LeagueOptions = season.League.Options,

                FatalityOdds = _config.FatalityChance,
                DisqualifyOdds = _config.DisqualifyChance,
                MistakeRolls = _config.MistakeAmountRolls,
                MistakeMinCost = _config.MistakeLowerValue,
                MistakeMaxCost = _config.MistakeUpperValue,
                GapMarge = _config.GapMarge,
            };
        }

        public async Task<QualifyingModel> RetrieveQualifyingModel(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            var race = await context.Race
                .Include(e => e.Climate)
                .Include(e => e.Track)
                    .ThenInclude(e => e.TrackTraits)
                .SingleAsync(e => e.Id == raceId);

            var unconsumedPenalties = await context.GivenPenalty.Where(e => !e.Consumed).Include(e => e.Incident).ToListAsync();

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
                .Where(e => (!e.ForWetConditions) && e.ForWetConditions == race.Climate.IsWet)
                .ToListAsync();

            // Do we feel secure about these null refs?
            var trackTraits = allTraits
                .Where(e => race.Track.TrackTraits.Select(tt => tt.TraitId).Contains(e.Id))
                .ToList();

            var model = new QualifyingModel
            {
                RaceId = race.Id,
                Name = race.Name,
                Country = race.Track.Country,
                Climate = race.Climate.Terminology,
                ClimateIcon = race.Climate.Icon,

                AmountRuns = season.RunAmountSession,
                MaximumRaceDrivers = season.MaximumDriversInRace,
                QualyRng = season.QualifyingRNG,
                QualyAmountQ2 = season.QualifyingAmountQ2,
                QualyAmountQ3 = season.QualifyingAmountQ3,

                GapMarge = _config.GapMarge,
            };

            var raceDrivers = new List<QualifyingDriver>();

            foreach (var driverResult in driverResults)
            {
                var driverTraits = new List<Trait>(trackTraits);

                var driver = drivers.Find(e => e.Id == driverResult.SeasonDriverId) ?? throw new InvalidOperationException("Could not find matching seasondriver for result");
                var team = teams.Find(e => e.Id == driverResult.SeasonTeamId) ?? throw new InvalidOperationException("Could not find matching seasonteam for result");

                var baseSpeed = driver.Skill + team.BaseValue + team.SeasonEngine.Power;
                var teamModifiers = (team.Aero * race.Track.AeroMod)
                    + (team.Chassis * race.Track.ChassisMod)
                    + (team.Powertrain * race.Track.PowerMod);

                var driverPower = baseSpeed + teamModifiers.RoundDouble();

                if (driver.Driver.DriverTraits?.Any() == true)
                    driverTraits.AddRange(allTraits.Where(e => driver.Driver.DriverTraits.Select(dt => dt.TraitId).Contains(e.Id)));

                if (team.Team.TeamTraits?.Any() == true)
                    driverTraits.AddRange(allTraits.Where(e => team.Team.TeamTraits.Select(dt => dt.TraitId).Contains(e.Id)));

                var traitEffect = NumberHelper.SumTraitEffects(driverTraits);
                var driverPenalties = unconsumedPenalties.Where(e => e.SeasonDriverId == driver.Id).ToArray();

                if (driverPenalties.Any())
                    model.PenaltiesToConsume.AddRange(driverPenalties.Select(e => e.Id));

                model.QualifyingDrivers.Add(new QualifyingDriver
                {
                    ResultId = driverResult.Id,
                    FirstName = driver.Driver.FirstName,
                    LastName = driver.Driver.LastName,
                    FullName = driver.Driver.FullName,
                    Number = driver.Number,
                    Role = driver.TeamRole,
                    Nationality = driver.Driver.Country,
                    TeamName = team.Name,
                    Colour = team.Colour,
                    Accent = team.Accent,

                    Power = driverPower + traitEffect.QualifyingPace,
                    PenaltyPunishment = driverPenalties.Any() ? driverPenalties.Sum(e => e.Incident.DoubledPunishment()) : 0,

                    RunValuesQ1 = new int[season.RunAmountSession],
                    RunValuesQ2 = new int[season.RunAmountSession],
                    RunValuesQ3 = new int[season.RunAmountSession],
                });
            }

            return model;
        }

        public async Task<PracticeModel> RetrievePracticeModel(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            var race = await context.Race
                .Include(e => e.Climate)
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
                .Where(e => !e.ForWetConditions)
                .ToListAsync();

            // Do we feel secure about these null refs?
            var trackTraits = allTraits
                .Where(e => race.Track.TrackTraits.Select(tt => tt.TraitId).Contains(e.Id))
                .ToList();

            var practiceDrivers = new List<PracticeDriver>();

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
                    FirstName = driver.Driver.FirstName,
                    LastName = driver.Driver.LastName,
                    FullName = driver.Driver.FullName,
                    Number = driver.Number,
                    Role = driver.TeamRole,
                    Nationality = driver.Driver.Country,
                    TeamName = team.Name,
                    Colour = team.Colour,
                    Accent = team.Accent,

                    Power = driverPower,

                    RunValues = new int[season.RunAmountSession],
                });
            }

            return new PracticeModel
            {
                RaceId = race.Id,
                Name = race.Name,
                Country = race.Track.Country,
                Climate = race.Climate.Terminology,
                ClimateIcon = race.Climate.Icon,

                AmountRuns = season.RunAmountSession,
                PracticeRng = season.QualifyingRNG / 2,

                PracticeDrivers = practiceDrivers,

                GapMarge = _config.GapMarge,
            };
        }
        #endregion
    }
}
