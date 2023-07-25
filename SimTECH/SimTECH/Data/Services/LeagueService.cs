using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public class LeagueService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public LeagueService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }


        public async Task<List<League>> GetLeagues() => await GetLeagues(StateFilter.Default);
        public async Task<List<League>> GetLeagues(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.League
                .Where(e => filter.StatesForFilter().Contains(e.State))
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
            {
                league.State = State.Active;
                context.Add(league);
            }
            else
            {
                if (league.DevelopmentRanges?.Any() == true)
                {
                    var removeableRanges = await context.DevelopmentRange
                        .Where(e => e.LeagueId == league.Id && !league.DevelopmentRanges.Select(e => e.Id).Contains(e.Id))
                        .ToListAsync();

                    context.RemoveRange(removeableRanges);
                }

                context.Update(league);
            }

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

        public async Task<List<DevelopmentRange>> GetLeagueDevelopmentRanges(long leagueId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.DevelopmentRange
                .Where(e => e.LeagueId == leagueId)
                .ToListAsync();
        }
    }
}
