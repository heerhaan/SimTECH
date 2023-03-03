using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class LeagueService
    {
        private IDbContextFactory<SimTechDbContext> _dbFactory;

        public LeagueService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        private readonly List<League> _leagues = new()
        {
            new League
            {
                Id = 1,
                Name = "Formula 0",
                State = State.Active
            }
        };

        public List<League> GetTestData()
        {
            return _leagues;
        }

        public async Task<League?> GetLeagueById(long leagueId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.League.FirstOrDefaultAsync(e => e.Id == leagueId);
        }

        public async ValueTask CreateLeague(League league)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Add(league);

            await context.SaveChangesAsync();

            _leagues.Add(league);
        }
    }
}
