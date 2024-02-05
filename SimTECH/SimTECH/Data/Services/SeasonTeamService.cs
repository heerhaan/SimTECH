using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services;

public class SeasonTeamService
{
    private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

    public SeasonTeamService(IDbContextFactory<SimTechDbContext> factory)
    {
        _dbFactory = factory;
    }

    public async Task<List<SeasonTeam>> GetSeasonTeams(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.SeasonTeam
            .Where(e => e.SeasonId == seasonId)
            .Include(e => e.Team)
                .ThenInclude(e => e.TeamTraits)
            .ToListAsync();
    }

    public async Task<List<SeasonTeam>> GetSeasonTeamsWithResults(long seasonId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.SeasonTeam
            .Where(e => e.SeasonId == seasonId)
            .Include(e => e.Team)
            .Include(e => e.Results)
            .ToListAsync();
    }

    public async Task<SeasonTeam> GetSeasonTeamById(long seasonTeamId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.SeasonTeam
            .Include(e => e.Team)
            .Include(e => e.SeasonEngine)
            .Include(e => e.Manufacturer)
            .SingleAsync(e => e.Id == seasonTeamId);
    }

    public async Task<SeasonTeam?> FindRecentSeasonTeam(long teamId, long leagueId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.SeasonTeam
            .Where(e => e.TeamId == teamId && e.Season.LeagueId == leagueId && e.Season.State == State.Closed)
            .OrderByDescending(e => e.SeasonId)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateSeasonTeam(SeasonTeam team)
    {
        using var context = _dbFactory.CreateDbContext();

        if (team.Id == 0)
            context.Add(team);
        else
            context.Update(team);

        await context.SaveChangesAsync();
    }

    public async Task SaveTeamDevelopment(Dictionary<long, int> developmentDict, Aspect target)
    {
        using var context = _dbFactory.CreateDbContext();

        var teams = await context.SeasonTeam.Where(e => developmentDict.Keys.Contains(e.Id)).ToListAsync();

        foreach (var team in teams)
        {
            switch (target)
            {
                case Aspect.BaseCar:
                    team.BaseValue = developmentDict[team.Id];
                    break;
                case Aspect.Reliability:
                    team.Reliability = developmentDict[team.Id];
                    break;
                case Aspect.Aero:
                    team.Aero = developmentDict[team.Id];
                    break;
                case Aspect.Chassis:
                    team.Chassis = developmentDict[team.Id];
                    break;
                case Aspect.Powertrain:
                    team.Powertrain = developmentDict[team.Id];
                    break;
            }
        }

        context.UpdateRange(teams);

        await context.SaveChangesAsync();
    }
}
