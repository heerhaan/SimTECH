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

        public async Task UpdateSeason(Season season)
        {
            using var context = _dbFactory.CreateDbContext();

            if (season.Id == 0)
                context.Add(season);
            else
                context.Update(season);

            await context.SaveChangesAsync();
        }
    }
}
