using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.PageModels;

namespace SimTECH.Data.Services
{
    public class RaceService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public RaceService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Race>> GetRacesBySeason(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Race
                .Include(e => e.Track)
                .Where(e => e.SeasonId == seasonId)
                .ToListAsync();
        }

        public async Task UpdateRace(Race race)
        {
            using var context = _dbFactory.CreateDbContext();

            if (race.Id == 0)
                context.Add(race);
            else
                context.Update(race);

            await context.SaveChangesAsync();
        }

        #region single-purpose calls
        // TODO: LeagueID parameter
        public async Task<List<CopyRaceModel>> GetRacesToCopy()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Race
                .Where(e => e.Season.LeagueId == 1)
                .Select(e => new CopyRaceModel
                {
                    RaceId = e.Id,
                    Name = e.Name,
                    Country = e.Track.Country,
                    SeasonYear = e.Season.Year
                })
                .ToListAsync();
        }
        #endregion
    }
}
