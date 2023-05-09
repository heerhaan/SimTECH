using Microsoft.EntityFrameworkCore;
using SimTECH.Data.EditModels;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class SeasonEntrantService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public SeasonEntrantService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        #region methods exclusively for season engines
        public async Task<List<SeasonEngine>> GetSeasonEngines(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonEngine.Where(e => e.SeasonId == seasonId).ToListAsync();
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

        public async Task SaveEngineDevelopment(Dictionary<long, int> developmentDict, TargetDevelop target)
        {
            using var context = _dbFactory.CreateDbContext();

            var engines = await context.SeasonEngine.Where(e => developmentDict.Keys.Contains(e.Id)).ToListAsync();

            foreach (var engine in engines)
            {
                if (target == TargetDevelop.Main)
                    engine.Power = developmentDict[engine.Id];
                else
                    engine.Reliability = developmentDict[engine.Id];
            }

            context.UpdateRange(engines);

            await context.SaveChangesAsync();
        }
        #endregion

        #region methods exclusively for season teams
        public async Task<List<SeasonTeam>> GetSeasonTeams(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonTeam
                .Where(e => e.SeasonId == seasonId)
                .Include(e => e.Team)
                .ToListAsync();
        }

        public async Task<List<SeasonTeam>> GetSeasonTeamsWithResults(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonTeam
                .Where(e => e.SeasonId == seasonId)
                .Include(e => e.Team)
                .Include(e => e.Results)
                .ToListAsync();
        }

        public async Task<SeasonTeam> GetSeasonTeamById(long seasonTeamId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonTeam
                .Include(e => e.Team)
                .Include(e => e.SeasonEngine)
                .Include(e => e.Manufacturer)
                .SingleAsync(e => e.Id == seasonTeamId);
        }

        public async Task UpdateSeasonTeam(SeasonTeam team)
        {
            using var context = _dbFactory.CreateDbContext();

            if (team.Id == 0)
                context.Add(team);
            else
                context.Update(team);

            await context.SaveChangesAsync();
        }

        public async Task SaveTeamDevelopment(Dictionary<long, int> developmentDict, TargetDevelop target)
        {
            using var context = _dbFactory.CreateDbContext();

            var teams = await context.SeasonTeam.Where(e => developmentDict.Keys.Contains(e.Id)).ToListAsync();

            foreach (var team in teams)
            {
                switch (target)
                {
                    case TargetDevelop.Main:
                        team.BaseValue = developmentDict[team.Id];
                        break;
                    case TargetDevelop.Reliability:
                        team.Reliability = developmentDict[team.Id];
                        break;
                    case TargetDevelop.Aero:
                        team.Aero = developmentDict[team.Id];
                        break;
                    case TargetDevelop.Chassis:
                        team.Chassis = developmentDict[team.Id];
                        break;
                    case TargetDevelop.Powertrain:
                        team.Powertrain = developmentDict[team.Id];
                        break;
                }
            }

            context.UpdateRange(teams);

            await context.SaveChangesAsync();
        }
        #endregion

        #region methods exclusively for season drivers
        public async Task<List<SeasonDriver>> GetSeasonDrivers(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonDriver
                .Include(e => e.Driver)
                .Where(e => e.SeasonId == seasonId)
                .ToListAsync();
        }

        public async Task<List<SeasonDriver>> GetSeasonDriversWithResults(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonDriver
                .Where(e => e.SeasonId == seasonId)
                .Include(e => e.Driver)
                .Include(e => e.Results)
                .ToListAsync();
        }

        public async Task UpdateSeasonDriver(SeasonDriver driver)
        {
            using var context = _dbFactory.CreateDbContext();

            if (driver.Id == 0)
                context.Add(driver);
            else
                context.Update(driver);

            await context.SaveChangesAsync();
        }

        public async Task SaveDriverDevelopment(Dictionary<long, int> developmentDict, TargetDevelop target)
        {
            using var context = _dbFactory.CreateDbContext();

            var drivers = await context.SeasonDriver.Where(e => developmentDict.Keys.Contains(e.Id)).ToListAsync();

            foreach (var driver in drivers)
            {
                if (target == TargetDevelop.Main)
                    driver.Skill = developmentDict[driver.Id];
                else
                    driver.Reliability = developmentDict[driver.Id];
            }

            context.UpdateRange(drivers);

            await context.SaveChangesAsync();
        }
        #endregion

        #region single-use methods
        // List of season engines contains (or is expected to) have all related data
        public async Task PersistSeasonEntrants(List<SeasonEngine> rootEngines)
        {
            using var context = _dbFactory.CreateDbContext();

            context.AddRange(rootEngines);

            await context.SaveChangesAsync();
        }

        public async Task CopyEntrantsFromSeason(long seasonId, long copiedSeasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            var rootEngines = await context.SeasonEngine
                .Where(e => e.SeasonId == copiedSeasonId)
                .Include(e => e.SeasonTeams)
                    .ThenInclude(e => e.SeasonDrivers)
                .ToListAsync();

            var editModels = rootEngines.ConvertAll(e => new EditSeasonEngineModel(e));

            foreach (var editModel in editModels)
            {
                editModel.ResetIdentifierFields();
                editModel.SetSeasonIdForAll(seasonId);
            }

            var rootEntrants = editModels.ConvertAll(e => e.Record);

            context.AddRange(rootEntrants);

            await context.SaveChangesAsync();
        }
        #endregion
    }
}
