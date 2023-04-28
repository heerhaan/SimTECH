﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MudBlazor;
using SimTECH.Data.EditModels;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels;

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

            var season = await context.Season.SingleAsync(e => e.Id == seasonId);

            if (season == null)
                throw new InvalidOperationException("No season related to ID found");
            if (season.State != State.Concept)
                throw new InvalidOperationException("Can only delete seasons which are in concept");

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

        public async Task CheckPenalties(long seasonId, List<Result> raceResults, int nextRound)
        {
            using var context = _dbFactory.CreateDbContext();

            var nextRace = await context.Race.FirstOrDefaultAsync(e => e.Round == nextRound);

            // There is no next race, thus there is no need to determine a penalty
            if (nextRace == null)
                return;

            var usedParts = await GetPartsUsageModel(seasonId);

            // Should be set in either config or league settings(!)
            int commonLimit = 3;
            //int accidentLimit = 3;
            //int collisionLimit = 3;
            //int engineLimit = 3;
            //int electricsLimit = 3;
            //int hydraulicsLimit = 3;
            // Same for the punishments (and differing punishments)
            int positionPunishment = 2;
            int dsqPunishment = 10;

            var newPenalties = new List<Penalty>();

            foreach (var dnfResult in raceResults.Where(e => e.Incident != Incident.None))
            {
                var componentUsage = usedParts.Single(e => e.SeasonDriverId == dnfResult.SeasonDriverId);

                switch (dnfResult.Incident)
                {
                    case Incident.Accident:
                    case Incident.Collision:
                    case Incident.Engine:
                    case Incident.Electrics:
                    case Incident.Hydraulics:
                        if (componentUsage.Accidents > commonLimit)
                        {
                            newPenalties.Add(new Penalty
                            {
                                Reason = $"Too many {dnfResult.Incident}",
                                Punishment = positionPunishment,
                                SeasonDriverId = dnfResult.Id,
                                RaceId = dnfResult.RaceId,
                            });
                        }
                        break;
                    case Incident.Illegal:
                    case Incident.Dangerous:
                    case Incident.Fuel:
                        newPenalties.Add(new Penalty
                        {
                            Reason = $"Disqualified due to {dnfResult.Incident}",
                            Punishment = dsqPunishment,
                            SeasonDriverId = dnfResult.Id,
                            RaceId = dnfResult.RaceId,
                        });
                        break;
                    default:
                        // No need to do anything else if an incidents isn't one of the above
                        break;
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
                    s => s.SeasonDrivers
                        .OrderByDescending(d => d.Points)
                        .Take(1))
                    .ThenInclude(d => d.Driver)
                .Include(
                    s => s.SeasonTeams
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
                DriverNationality = s.SeasonDrivers?.FirstOrDefault()?.Driver.Country ?? EnumHelper.GetDefaultCountry(),

                TeamName = s.SeasonTeams?.FirstOrDefault()?.Name ?? "Unknown",
                TeamColour = s.SeasonTeams?.FirstOrDefault()?.Colour ?? "Unknown",
                TeamNationality = s.SeasonTeams?.FirstOrDefault()?.Team.Country ?? EnumHelper.GetDefaultCountry(),
            });
        }

        // The idea is that this piece of code gives us the info needed to know whether we're going to need to display a warning
        public SeasonOverviewAvailability GetOverviewAvailability(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return new SeasonOverviewAvailability
            {
                HasPointAllotment = context.Season.Single(e => e.Id == seasonId).PointAllotments.Any(),
                HasEngines = false,
                HasTeams = false,
                HasDrivers = false,
            };
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

                var sumTraits = NumberHelper.SumTraitEffects(driverTraits);

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

        public async Task<List<PartsUsageModel>> GetPartsUsageModel(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            var drivers = await context.SeasonDriver
                .Where(e => e.SeasonId == seasonId)
                .Include(e => e.Driver)
                .Include(e => e.SeasonTeam)
                .ToListAsync();

            var results = await context.Result
                .Where(e => e.Race.SeasonId == seasonId)
                .ToListAsync();

            var penalties = await context.Penalty
                .Where(e => e.Race.SeasonId == seasonId)
                .Include(e => e.Race)
                .ToListAsync();

            var partsUsedList = new List<PartsUsageModel>(drivers.Count);

            foreach (var driver in drivers)
            {
                var driverResults = results.Where(e => e.SeasonDriverId == driver.Id).ToList();

                partsUsedList.Add(new PartsUsageModel
                {
                    SeasonDriverId = driver.Id,
                    Name = driver.Driver.FullName,
                    Number = driver.Number,
                    Team = driver.SeasonTeam?.Name ?? "None",
                    Colour = driver.SeasonTeam?.Colour ?? Constants.DefaultColour,
                    Accent = driver.SeasonTeam?.Accent ?? Constants.DefaultAccent,

                    Accidents = driverResults.Count(e => e.Incident == Incident.Accident),
                    Collisions = driverResults.Count(e => e.Incident == Incident.Collision),
                    Engines = driverResults.Count(e => e.Incident == Incident.Engine),
                    Electrics = driverResults.Count(e => e.Incident == Incident.Electrics),
                    Hydraulics = driverResults.Count(e => e.Incident == Incident.Hydraulics),
                    TotalDnf = driverResults.Count(e => e.Status == RaceStatus.Dnf),
                    TotalDsq = driverResults.Count(e => e.Status == RaceStatus.Dsq),

                    GivenPenalties = penalties
                        .Where(e => e.SeasonDriverId == driver.Id)
                        .Select(e => new GivenPenalties
                        {
                            Round = e.Race.Round,
                            Reason = e.Reason,
                            Punishment = e.Punishment
                        })
                        .ToList(),
                });
            }

            return partsUsedList;
        }
        #endregion
    }
}
