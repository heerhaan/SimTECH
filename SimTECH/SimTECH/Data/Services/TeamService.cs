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

        public async Task CreateTeam(Team team)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Add(team);

            await context.SaveChangesAsync();
        }

        public async Task UpdateTeam(Team team)
        {
            using var context = _dbFactory.CreateDbContext();
            context.Update(team);

            await context.SaveChangesAsync();
        }
    }
}
