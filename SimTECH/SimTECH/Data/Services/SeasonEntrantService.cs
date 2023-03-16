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

        // Teams
        public async Task<List<SeasonTeam>> GetSeasonTeams(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonTeam.Where(e => e.SeasonId == seasonId).ToListAsync();
        }

        // Engines
        public async Task<List<SeasonEngine>> GetSeasonEngines(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonEngine.Where(e => e.SeasonId == seasonId).ToListAsync();
        }

        // Drivers
        public async Task<List<SeasonDriver>> GetSeasonDrivers(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.SeasonDriver.Where(e => e.SeasonId == seasonId).ToListAsync();
        }
    }
}
