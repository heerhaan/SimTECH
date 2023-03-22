using Microsoft.EntityFrameworkCore;
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
        #endregion

        #region methods exclusively for season teams
        public async Task<List<SeasonTeam>> GetSeasonTeams(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonTeam
                .Where(e => e.SeasonId == seasonId)
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

        public async Task UpdateSeasonDriver(SeasonDriver driver)
        {
            using var context = _dbFactory.CreateDbContext();

            if (driver.Id == 0)
                context.Add(driver);
            else
                context.Update(driver);

            await context.SaveChangesAsync();
        }
        #endregion

        #region single-use methods
        // List of season engines contains (or is expected to) have all related data
        public async Task PersistSeasonEntrants(List<SeasonEngine> rootEngines)
        {
            using var context = _dbFactory.CreateDbContext();

            await context.AddRangeAsync(rootEngines);

            await context.SaveChangesAsync();
        }
        #endregion
    }
}
