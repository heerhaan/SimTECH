using Microsoft.EntityFrameworkCore;
using SimTECH.Data.EditModels;
using SimTECH.Data.Models;
using SimTECH.PageModels;
using SimTECH.Pages.Season;

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

            return await context.Season
                .Include(e => e.PointAllotments)
                .ToListAsync();
        }

        public async Task<Season> GetSeasonById(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Season
                .Include(e => e.PointAllotments)
                .SingleAsync(e => e.Id == seasonId);
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

        public async Task<bool> DeleteSeason(Season? season)
        {
            using var context = _dbFactory.CreateDbContext();

            if (season == null)
                throw new InvalidOperationException("No season related to ID found");
            if (season.State != State.Concept)
                throw new InvalidOperationException("Can only delete seasons which are in concept");

            var seasonDrivers = await context.SeasonDriver.Where(e => e.SeasonId == season.Id).ToListAsync();
            var seasonTeams = await context.SeasonTeam.Where(e => e.SeasonId == season.Id).ToListAsync();
            var seasonEngines = await context.SeasonEngine.Where(e => e.SeasonId == season.Id).ToListAsync();
            var racesInSeason = await context.Race
                .Where(e => e.SeasonId == season.Id)
                .ToListAsync();

            context.RemoveRange(seasonDrivers);
            context.RemoveRange(seasonTeams);
            context.RemoveRange(seasonEngines);
            context.RemoveRange(racesInSeason);
            context.Remove(season);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<string?> ActivateSeason(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            var season = await context.Season
                .Include(e => e.SeasonEngines)
                .Include(e => e.SeasonTeams)
                .Include(e => e.SeasonDrivers)
                .Include(e => e.Races)
                .SingleAsync(e => e.Id == seasonId);

            if (season.State != State.Concept)
                throw new InvalidOperationException("Can only activate seasons which are currently in 'concept'.");

            if (season.SeasonEngines?.Any() != true)
                return "this blitherin idiot right here forgot to add any entrants to this season, begin with adding the engines.";
            if (season.SeasonTeams?.Any() != true)
                return "absolute moron forgot to add the teams, how are you going to race a motorsport season without any teams. come on mate";
            if (season.SeasonDrivers?.Any() != true)
                return "hey robocop this isn't the future, we still have people driving the cars. so add some fucking drivers will ya?";
            if (season?.Races?.Any() != true)
                return "hint of advice, a season without any races is hardly much of a season. add races, pisshead";

            // i know i know, fugly solution here but alas it works
            var editModel = new EditSeasonModel(season) { State = State.Active };
            var editedRecord = editModel.Record;

            context.Update(editedRecord);

            await context.SaveChangesAsync();

            return null;
        }

        public SeasonOverviewAvailability GetOverviewAvailability(long seasonId)
        {
            using var context = _dbFactory.CreateDbContext();

            return new SeasonOverviewAvailability
            {
                HasPointAllotment = context.Season.Single(e => e.Id == seasonId).PointAllotments.Any(),
                HasEngines = false,
                HasTeams = false,
                HasDrivers = false,
            };
        }
    }
}
