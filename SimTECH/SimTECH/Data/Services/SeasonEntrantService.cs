using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Pages.Leagues;

namespace SimTECH.Data.Services
{
    public class SeasonEntrantService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public SeasonEntrantService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

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

        #region methods exclusively for season engines
        public async Task<List<SeasonEngine>> GetSeasonEngines(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonEngine.Where(e => e.SeasonId == seasonId).ToListAsync();
        }

        public async Task<SeasonEngine?> FindRecentSeasonEngine(long engineId, long leagueId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonEngine
                .Where(e => e.EngineId == engineId && e.Season.LeagueId == leagueId && e.Season.State == State.Closed)
                .OrderByDescending(e => e.SeasonId)
                .FirstOrDefaultAsync();
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

        public async Task SaveEngineDevelopment(Dictionary<long, int> developmentDict, Aspect target)
        {
            using var context = _dbFactory.CreateDbContext();

            var engines = await context.SeasonEngine.Where(e => developmentDict.Keys.Contains(e.Id)).ToListAsync();

            foreach (var engine in engines)
            {
                if (target == Aspect.Skill)
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

        public async Task<SeasonTeam?> FindRecentSeasonTeam(long teamId, long leagueId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonTeam
                .Where(e => e.TeamId == teamId && e.Season.LeagueId == leagueId && e.Season.State == State.Closed)
                .OrderByDescending(e => e.SeasonId)
                .FirstOrDefaultAsync();
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

        public async Task SaveTeamDevelopment(Dictionary<long, int> developmentDict, Aspect target)
        {
            using var context = _dbFactory.CreateDbContext();

            var teams = await context.SeasonTeam.Where(e => developmentDict.Keys.Contains(e.Id)).ToListAsync();

            foreach (var team in teams)
            {
                switch (target)
                {
                    case Aspect.Skill:
                        team.BaseValue = developmentDict[team.Id];
                        break;
                    case Aspect.Reliability:
                        team.Reliability = developmentDict[team.Id];
                        break;
                    case Aspect.Aero:
                        team.Aero = developmentDict[team.Id];
                        break;
                    case Aspect.Chassis:
                        team.Chassis = developmentDict[team.Id];
                        break;
                    case Aspect.Powertrain:
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

        public async Task<SeasonDriver?> FindRecentSeasonDriver(long driverId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonDriver
                .Where(e => e.DriverId == driverId && e.Season.State == State.Closed)
                .OrderByDescending(e => e.SeasonId)
                .FirstOrDefaultAsync();
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

        public async Task SaveDriverDevelopment(Dictionary<long, int> developmentDict, Aspect target)
        {
            using var context = _dbFactory.CreateDbContext();

            var drivers = await context.SeasonDriver.Where(e => developmentDict.Keys.Contains(e.Id)).ToListAsync();

            foreach (var driver in drivers)
            {
                switch (target)
                {
                    case Aspect.Skill: driver.Skill = developmentDict[driver.Id]; break;
                    case Aspect.Reliability: driver.Reliability = developmentDict[driver.Id]; break;
                    case Aspect.Attack: driver.Attack = developmentDict[driver.Id]; break;
                    case Aspect.Defense: driver.Defense = developmentDict[driver.Id]; break;
                    default: throw new InvalidOperationException("thats the wrong enum value buddy");
                }
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
        #endregion
    }
}
