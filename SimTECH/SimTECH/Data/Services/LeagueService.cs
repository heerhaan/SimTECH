using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class LeagueService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public LeagueService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<League>> GetLeagues()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.League.ToListAsync();
        }

        public async Task<League?> GetLeagueById(long leagueId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.League.FirstOrDefaultAsync(e => e.Id == leagueId);
        }

        public async Task CreateLeague(League league)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Add(league);

            await context.SaveChangesAsync();
        }

        public async Task UpdateLeague(League league)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Update(league);

            await context.SaveChangesAsync();
        }
    }
}
