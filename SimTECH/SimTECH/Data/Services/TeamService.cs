﻿using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public class TeamService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public TeamService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
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
