﻿using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services;

public class TeamService(IDbContextFactory<SimTechDbContext> factory) : StateService<Team>(factory)
{
    public async Task<List<Team>> GetTeams() => await GetTeams(StateFilter.Default);
    public async Task<List<Team>> GetTeams(StateFilter filter)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Team
            .Where(e => filter.StatesForFilter().Contains(e.State))
            .Include(e => e.TeamTraits)
            .ToListAsync();
    }

    public async Task<List<Team>> GetTeamsInActiveLeague(long leagueId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Team
            .Where(e => e.State == State.Active
                && e.SeasonTeams.Any(e => e.Season.LeagueId == leagueId && e.Season.State == State.Active))
            .Include(e => e.TeamTraits)
            .ToListAsync();
    }

    public async Task<List<Team>> GetAvailableTeams(long seasonId, StateFilter filter = StateFilter.Active)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Team
            .Where(e => filter.StatesForFilter().Contains(e.State)
                && !e.SeasonTeams.Any(st => st.SeasonId == seasonId))
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

            if (removeables.Count != 0)
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
