using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MudBlazor;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels;
using SimTECH.PageModels.SeasonModels;
using System.ComponentModel;

namespace SimTECH.Data.Services
{
    public class SeasonService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;
        private readonly SimConfig _config;

        public SeasonService(IDbContextFactory<SimTechDbContext> factory, IOptions<SimConfig> config)
        {
            _dbFactory = factory;
            _config = config.Value;
        }

        public async Task<List<Season>> GetSeasons() => await GetSeasons(StateFilter.Default);
        public async Task<List<Season>> GetSeasons(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Season
                .Where(e => filter.StatesForFilter().Contains(e.State))
                .Include(e => e.PointAllotments)
                .ToListAsync();
        }

        public async Task<Season> GetSeasonById(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Season
                .Include(e => e.PointAllotments)
                .SingleAsync(e => e.Id == seasonId);
        }

        public async Task<Season?> FindRecentClosedSeason(long leagueId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Season
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

            var season = await context.Season.Include(e => e.PointAllotments).SingleAsync(e => e.Id == seasonId) ?? throw new InvalidOperationException("No season related to ID found");
            if (season.State != State.Concept)
                throw new InvalidOperationException("Can only delete seasons which are in a concept state");

            var seasonDrivers = await context.SeasonDriver.Where(e => e.SeasonId == season.Id).ToListAsync();
            var seasonTeams = await context.SeasonTeam.Where(e => e.SeasonId == season.Id).ToListAsync();
            var seasonEngines = await context.SeasonEngine.Where(e => e.SeasonId == season.Id).ToListAsync();
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

        public async Task<string?> ActivateSeason(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            var season = await context.Season
                .Include(e => e.SeasonEngines)
                .Include(e => e.SeasonTeams)
                .Include(e => e.SeasonDrivers)
                .Include(e => e.Races)
                .SingleAsync(e => e.Id == seasonId);

            if (season.State != State.Concept)
                throw new InvalidOperationException("Can only activate seasons which are currently in 'concept'.");

            if (season.SeasonEngines?.Any() != true)
                return "this blitherin idiot right here forgot to add any entrants to this season, begin with adding the engines.";
            if (season.SeasonTeams?.Any() != true)
                return "absolute moron forgot to add the teams, how are you going to race a motorsport season without any teams. come on mate";
            if (season.SeasonDrivers?.Any() != true)
                return "hey robocop this isn't the future, we still have people driving the cars. so add some fucking drivers will ya?";
            if (season?.Races?.Any() != true)
                return "hint of advice, a season without any races is hardly much of a season. add races, pisshead";

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

            foreach (var dnfResult in raceResults.Where(e => e.Incident?.HasLimit() == true))
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

            if (newPenalties.Any())
            {
                context.AddRange(newPenalties);
                await context.SaveChangesAsync();
            }
        }

        #region page models
        // Retrieves a list of seasons specifically meant to be displayed in the... list of seasons!!!
        public async Task<List<SeasonListModel>> GetSeasonList()
        {
            using var context = _dbFactory.CreateDbContext();

            var seasons = await context.Season
                .Include(e => e.League)
                .Include(
                    s => s.SeasonDrivers!
                        .OrderByDescending(d => d.Points)
                        .Take(1))
                    .ThenInclude(d => d.Driver)
                .Include(
                    s => s.SeasonTeams!
                        .OrderByDescending(t => t.Points)
                        .Take(1))
                    .ThenInclude(t => t.Team)
                .ToListAsync();

            return seasons.ConvertAll(s => new SeasonListModel
            {
                Id = s.Id,
                Year = s.Year,
                State = s.State,

                League = s.League.Name,

                DriverName = s.SeasonDrivers?.FirstOrDefault()?.Driver.FullName ?? "Unknown",
                DriverNumber = s.SeasonDrivers?.FirstOrDefault()?.Number ?? 0,
                DriverNationality = s.SeasonDrivers?.FirstOrDefault()?.Driver.Country ?? Constants.DefaultCountry,

                TeamName = s.SeasonTeams?.FirstOrDefault()?.Name ?? "Unknown",
                TeamColour = s.SeasonTeams?.FirstOrDefault()?.Colour ?? "Unknown",
                TeamNationality = s.SeasonTeams?.FirstOrDefault()?.Team.Country ?? Constants.DefaultCountry,
            });
        }

        // Consider moving this code completely to the page itself
        public async Task<List<QualyBattle>> GetQualifyingBattles(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            var races = await context.Race
                .Where(e => e.SeasonId == seasonId && e.State == State.Closed)
                .Include(e => e.Results)
                .ToListAsync();

            var drivers = await context.SeasonDriver
                .Where(e => e.SeasonId == seasonId)
                .Include(e => e.Driver)
                .ToListAsync();

            var battleVictories = new Dictionary<long, int>();

            foreach (var driver in drivers)
                battleVictories.Add(driver.Id, 0);

            foreach (var race in races)
            {
                foreach (var byTeam in race.Results.GroupBy(e => e.SeasonTeamId))
                {
                    var bestResult = byTeam.OrderBy(e => e.Grid).FirstOrDefault();

                    if (bestResult != null)
                        battleVictories[bestResult.SeasonDriverId]++;
                }
            }

            var teams = await context.SeasonTeam
                .Where(e => e.SeasonId == seasonId)
                .Include(e => e.Team)
                .ToArrayAsync();

            var qualyBattles = new List<QualyBattle>();
            foreach (var victory in battleVictories)
            {
                var driver = drivers.First(e => e.Id == victory.Key);

                SeasonTeam? team = null;
                if (driver.SeasonTeamId.HasValue)
                    team = teams.First(e => e.Id == driver.SeasonTeamId.Value);

                qualyBattles.Add(new QualyBattle
                {
                    Name = driver.Driver.FullName,
                    Score = victory.Value,
                    Colour = team?.Colour ?? "var(--mud-palette-primary)",
                    Team = team?.Team.Name ?? "None",
                });
            }

            return qualyBattles;
        }

        public async Task<List<PowerRankModel>> GetPowerRankings(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            var drivers = await context.SeasonDriver
                .Where(e => e.SeasonId == seasonId)
                .Include(e => e.Driver)
                    .ThenInclude(e => e.DriverTraits)
                .ToListAsync();

            var teams = await context.SeasonTeam
                .Where(e => e.SeasonId == seasonId)
                .Include(e => e.SeasonEngine)
                .Include(e => e.Team)
                    .ThenInclude(e => e.TeamTraits)
                .ToListAsync();

            var allTraits = await context.Trait.ToListAsync();

            var allManufacturers = await context.Manufacturer.ToListAsync();

            var powerDrivers = new List<PowerRankModel>();

            foreach (var driver in drivers)
            {
                var powerModel = new PowerRankModel
                {
                    DriverName = driver.Driver.FullName,
                    DriverNumber = driver.Number,
                    Nationality = driver.Driver.Country,
                };
                var driverTraits = new List<Trait>();

                if (driver.Driver.DriverTraits?.Any() == true)
                    driverTraits.AddRange(allTraits.Where(e => driver.Driver.DriverTraits.Select(dt => dt.TraitId).Contains(e.Id)));

                powerModel.QualyPower += driver.Skill + driver.RetrieveStatusBonus(_config.CarDriverStatusModifier);
                powerModel.RacePower += driver.Skill + driver.RetrieveStatusBonus(_config.CarDriverStatusModifier);
                powerModel.DriverRel = driver.Reliability;

                if (driver.SeasonTeamId.HasValue)
                {
                    var team = teams.Single(e => e.Id == driver.SeasonTeamId);
                    var manufacturer = allManufacturers.Single(e => e.Id == team.ManufacturerId);

                    if (team.Team.TeamTraits?.Any() == true)
                        driverTraits.AddRange(allTraits.Where(e => team.Team.TeamTraits.Select(dt => dt.TraitId).Contains(e.Id)));

                    powerModel.QualyPower += team.BaseValue + team.SeasonEngine.Power + manufacturer.Pace;
                    powerModel.RacePower += team.BaseValue + team.SeasonEngine.Power + manufacturer.Pace;

                    powerModel.Aero = team.Aero;
                    powerModel.Chassis = team.Chassis;
                    powerModel.Powertrain = team.Powertrain;
                    powerModel.CarRel = team.Reliability;
                    powerModel.EngineRel = team.SeasonEngine.Reliability;
                    powerModel.WearMax = manufacturer.WearMax;
                    powerModel.WearMin = manufacturer.WearMin;

                    powerModel.TeamName = team.Name;
                    powerModel.Colour = team.Colour;
                    powerModel.Accent = team.Accent;
                    powerModel.Engine = team.SeasonEngine.Name;
                    powerModel.Manufacturer = manufacturer.Name;
                }
                else
                {
                    powerModel.TeamName = "Dropped";
                    powerModel.Engine = "None";
                }

                var sumTraits = driverTraits.SumTraitEffects();

                powerModel.QualyPower += sumTraits.QualifyingPace;
                powerModel.RacePower += sumTraits.RacePace;
                powerModel.DriverRel += sumTraits.DriverReliability;
                powerModel.CarRel += sumTraits.CarReliability;
                powerModel.EngineRel += sumTraits.EngineReliability;
                powerModel.WearMax += sumTraits.WearMaximum;
                powerModel.WearMin += sumTraits.WearMinimum;

                powerModel.TraitEffect = sumTraits;

                powerDrivers.Add(powerModel);
            }

            return powerDrivers;
        }

        public async Task<List<PartsUsedByDriver>> GetPartsUsageModel(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            var drivers = await context.SeasonDriver
                .Where(e => e.SeasonId == seasonId)
                .Include(e => e.Driver)
                .Include(e => e.SeasonTeam)
                .Include(e => e.GivenPenalties)
                .ToListAsync();

            var results = await context.Result
                .Where(e => e.Race.SeasonId == seasonId)
                .ToListAsync();

            var partsUsedList = new List<PartsUsedByDriver>(drivers.Count);

            foreach (var driver in drivers)
            {
                var driverModel = new PartsUsedByDriver
                {
                    SeasonDriverId = driver.Id,
                    Name = driver.Driver.FullName,
                    Number = driver.Number,
                    Team = driver.SeasonTeam?.Name ?? "None",
                    Colour = driver.SeasonTeam?.Colour ?? Constants.DefaultColour,
                    Accent = driver.SeasonTeam?.Accent ?? Constants.DefaultAccent,
                    GivenPenalties = driver.GivenPenalties?.ToList() ?? new(),
                };

                var driverResults = results.Where(e => e.SeasonDriverId == driver.Id).ToList();
                driverModel.TotalDnf = driverResults.Count(e => e.Status == RaceStatus.Dnf);
                driverModel.TotalDsq = driverResults.Count(e => e.Status == RaceStatus.Dsq);

                foreach (var driverResult in driverResults.Where(e => e.IncidentId.HasValue).GroupBy(e => e.IncidentId))
                {
                    driverModel.DriverIncidents.Add(driverResult.Key.GetValueOrDefault(), driverResult.Count());
                }

                partsUsedList.Add(driverModel);
            }

            return partsUsedList;
        }
        #endregion
    }
}
