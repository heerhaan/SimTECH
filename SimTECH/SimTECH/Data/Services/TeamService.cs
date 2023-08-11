using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public class TeamService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;
        private readonly SimConfig _config;

        public TeamService(IDbContextFactory<SimTechDbContext> factory, IOptions<SimConfig> config)
        {
            _dbFactory = factory;
            _config = config.Value;
        }

        public async Task<List<Team>> GetTeams() => await GetTeams(StateFilter.Default);
        public async Task<List<Team>> GetTeams(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Team
                .Where(e => filter.StatesForFilter().Contains(e.State))
                .Include(e => e.TeamTraits)
                .ToListAsync();
        }

        public async Task<List<Team>> GetAvailableTeams()
        {
            using var context = _dbFactory.CreateDbContext();

            var query = context.Team.Where(e => e.State == State.Active);
            if (!_config.AllowMultiLeagueEntry)
                query = query.Where(e => !e.SeasonTeams!.Any(e => e.Season.State == State.Active));

            return await query.ToListAsync();
        }

        public async Task<List<Team>> GetLeagueTeams(long leagueId) => await GetLeagueTeams(leagueId, StateFilter.Default);
        public async Task<List<Team>> GetLeagueTeams(long leagueId, StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Team
                .Where(e => filter.StatesForFilter().Contains(e.State) && e.SeasonTeams!.Any(e => e.Season.LeagueId == leagueId))
                .Include(e => e.TeamTraits)
                .ToListAsync();
        }

        public async Task UpdateTeam(Team team)
        {
            using var context = _dbFactory.CreateDbContext();

            if (team.Id == 0)
            {
                team.State = State.Active;
                context.Add(team);
            }
            else
            {
                var removeables = await context.TeamTrait
                        .Where(e => e.TeamId == team.Id)
                        .ToListAsync();

                if (removeables.Any())
                    context.RemoveRange(removeables);

                if (team.TeamTraits?.Any() ?? false)
                    context.AddRange(team.TeamTraits);

                context.Update(team);
            }

            await context.SaveChangesAsync();
        }

        public async Task ArchiveTeam(Team team)
        {
            using var context = _dbFactory.CreateDbContext();

            if (team.State == State.Archived)
            {
                team.State = State.Active;
            }
            else
            {
                if (context.SeasonTeam.Any(e => e.TeamId == team.Id))
                {
                    team.State = State.Archived;
                    context.Update(team);
                }
                else
                {
                    context.Remove(team);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
