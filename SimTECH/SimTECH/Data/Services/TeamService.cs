using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class TeamService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public TeamService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Team>> GetTeams()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Team.ToListAsync();
        }

        public async Task UpdateTeam(Team team)
        {
            using var context = _dbFactory.CreateDbContext();

            if (team.Id == 0)
            {
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
    }
}
