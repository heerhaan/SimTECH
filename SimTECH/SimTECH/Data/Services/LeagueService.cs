using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services;

public class LeagueService(IDbContextFactory<SimTechDbContext> factory)
{
    private readonly IDbContextFactory<SimTechDbContext> _dbFactory = factory;

    public async Task<List<League>> GetLeagues() => await GetLeagues(StateFilter.Default);

    public async Task<List<League>> GetLeagues(StateFilter filter)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.League
            .Where(e => filter.StatesForFilter().Contains(e.State))
            .Include(e => e.DevelopmentRanges)
            .Include(e => e.LeagueTyres)
            .ToListAsync();
    }

    public async Task<League> GetLeagueById(long leagueId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.League
            .Include(e => e.DevelopmentRanges)
            .Include(e => e.LeagueTyres)
            .SingleAsync(e => e.Id == leagueId);
    }

    public async Task UpdateLeague(League league)
    {
        using var context = _dbFactory.CreateDbContext();

        if (league.Id == 0)
        {
            league.State = State.Active;
            context.Add(league);
        }
        else
        {
            if (league.DevelopmentRanges?.Any() == true)
            {
                var removeableRanges = await context.DevelopmentRange
                    .Where(e => e.LeagueId == league.Id && !league.DevelopmentRanges.Select(e => e.Id).Contains(e.Id))
                    .ToListAsync();

                context.RemoveRange(removeableRanges);
            }

            var removeableTyres = await context.LeagueTyre
                .Where(e => e.LeagueId == league.Id)
                .ToListAsync();

            if (removeableTyres.Any())
                context.RemoveRange(removeableTyres);

            if (league.LeagueTyres?.Any() == true)
                context.AddRange(league.LeagueTyres);

            context.Update(league);
        }

        await context.SaveChangesAsync();
    }

    public async Task DeleteLeague(League league)
    {
        using var context = _dbFactory.CreateDbContext();

        if (context.Season.Any(e => e.LeagueId == league.Id))
            throw new InvalidOperationException("Can not delete leagues containing seasons");
        else
            context.Remove(league);

        await context.SaveChangesAsync();
    }

    public async Task<List<DevelopmentRange>> GetLeagueDevelopmentRanges(long leagueId)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.DevelopmentRange
            .Where(e => e.LeagueId == leagueId)
            .ToListAsync();
    }
}

