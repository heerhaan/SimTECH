using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Extensions;
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

        public async Task<Race> GetRaceById(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Race
                .Include(e => e.Track)
                .Include(e => e.Stints)
                .Include(e => e.Penalties)
                .SingleAsync(e => e.Id == raceId);
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

        public async Task ActivateRace(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            var race = await context.Race
                .Include(e => e.Stints)
                .SingleAsync(e => e.Id == raceId);

            if (race.State != State.Concept)
                throw new InvalidOperationException("Can only activate races in concept state");

            var seasonDrivers = await context.SeasonDriver
                .Where(e => e.SeasonId == race.SeasonId && e.SeasonTeamId.HasValue)
                .ToListAsync();

            var strategiesForRace = await context.Strategy
                .Include(e => e.StrategyTyres)
                    .ThenInclude(e => e.Tyre)
                .Where(e => e.StintLength == race.Stints.Count)
                .ToListAsync();

            if (strategiesForRace?.Any() != true)
                throw new InvalidOperationException("No valid strategies for this race, smh");

            var driverResults = new List<Result>();

            foreach (var driver in seasonDrivers)
            {
                var strategy = strategiesForRace[NumberHelper.RandomInt(strategiesForRace.Count - 1)];

                driverResults.Add(new Result
                {
                    Status = RaceStatus.Racing,
                    TyreLife = strategy.StrategyTyres[0].Tyre.Pace,
                    SeasonDriverId = driver.Id,
                    SeasonTeamId = driver.SeasonTeamId.Value,
                    RaceId = race.Id,
                    StrategyId = strategy.Id,
                });
            }

            context.AddRange(driverResults);

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

        public async Task<RaceWeekModel> GetRaceWeekModel(long raceId)
        {
            using var context = _dbFactory.CreateDbContext();

            var race = await context.Race
                .Include(e => e.Penalties)
                .Include(e => e.Track)
                    .ThenInclude(e => e.TrackTraits)
                .SingleAsync(e => e.Id == raceId);

            var strategiesForRace = await context.Strategy
                .Include(e => e.StrategyTyres)
                    .ThenInclude(e => e.Tyre)
                .Where(e => e.StintLength == race.Stints.Count)
                .ToListAsync();

            var weekendDrivers = await context.Result
                .Include(e => e.SeasonDriver)
                    .ThenInclude(e => e.Driver)
                .Include(e => e.SeasonTeam)
                .Where(e => e.RaceId == raceId)
                .Select(result => new RaceWeekDriver
                {
                    SeasonDriverId = result.SeasonDriverId,
                    FullName = result.SeasonDriver.Driver.FullName,
                    Number = result.SeasonDriver.Number,
                    Role = result.SeasonDriver.TeamRole,
                    Country = result.SeasonDriver.Driver.Country,
                    SeasonTeamId = result.SeasonTeamId,
                    TeamName = result.SeasonTeam.Name,
                    Colour = result.SeasonTeam.Colour,
                    Accent = result.SeasonTeam.Accent,
                    ResultId = result.Id,
                    Grid = result.Grid,
                    Position = result.Position,
                    Status = result.Status
                })
                .ToListAsync();

            if (weekendDrivers?.Any() != true)
                throw new InvalidOperationException("We're going to need some actual drivers too");

            return new RaceWeekModel
            {
                Race = race,
                RaceWeekDrivers = weekendDrivers,
                AvailableStrategies = strategiesForRace,
            };
        }
        #endregion
    }
}
