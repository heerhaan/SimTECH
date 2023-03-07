using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class SeasonService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public SeasonService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Season>> GetSeasons()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Season.Include(e => e.PointAllotments).ToListAsync();
        }

        public async Task CreateSeason(Season season)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Add(season);

            await context.SaveChangesAsync();
        }

        public async Task UpdateSeason(Season season)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Update(season);

            await context.SaveChangesAsync();
        }
    }
}
