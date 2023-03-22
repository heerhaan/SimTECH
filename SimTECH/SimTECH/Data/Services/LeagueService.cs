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

            return await context.League
                .Include(e => e.DevelopmentRanges)
                .ToListAsync();
        }

        public async Task<League> GetLeagueById(long leagueId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.League
                .Include(e => e.DevelopmentRanges)
                .SingleAsync(e => e.Id == leagueId);
        }

        public async Task UpdateLeague(League league)
        {
            using var context = _dbFactory.CreateDbContext();

            if (league.Id == 0)
                context.Add(league);
            else
                context.Update(league);

            await context.SaveChangesAsync();
        }

        public async Task DeleteLeague(League league)
        {
            using var context = _dbFactory.CreateDbContext();

            if (context.Season.Any(e => e.LeagueId == league.Id))
                throw new InvalidOperationException("Can not delete leagues containing seasons");
            else
                context.Remove(league);

            await context.SaveChangesAsync();
        }
    }
}
