using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.PageModels;

namespace SimTECH.Data.Services;

public class TraitService(IDbContextFactory<SimTechDbContext> factory) : StateService<Trait>(factory)
{
    public async Task<List<Trait>> GetTraits() => await GetTraits(StateFilter.Default);
    public async Task<List<Trait>> GetTraits(StateFilter filter)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Trait
            .Where(e => filter.StatesForFilter().Contains(e.State))
            .ToListAsync();
    }

    public async Task<List<Trait>> GetTraitsOfType(Entrant type) => await GetTraitsOfType(type, StateFilter.Default);
    public async Task<List<Trait>> GetTraitsOfType(Entrant type, StateFilter filter)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Trait
            .Where(e => e.Type == type && filter.StatesForFilter().Contains(e.State))
            .ToListAsync();
    }

    public async Task UpdateTrait(Trait trait)
    {
        using var context = _dbFactory.CreateDbContext();

        if (trait.Id == 0)
        {
            trait.State = State.Active;
            context.Add(trait);
        }
        else
        {
            context.Update(trait);
        }

        await context.SaveChangesAsync();
    }

    #region assigner methods
    // I suspect the following three methods are a good target for generics, if i implemented those
    public async Task AssignDriverTraits(List<EntrantAssignee> assignedDrivers)
    {
        using var context = _dbFactory.CreateDbContext();

        // Assume now that we can't have already assigned traits

        // Assume we aren't removing traits

        var assignedDriverTraits = new List<DriverTrait>();

        foreach (var driver in assignedDrivers)
        {
            if (driver.AssignedTraits.Count != 0)
            {
                var assignableTraits = driver.AssignedTraitIds
                    .Select(e => new DriverTrait { DriverId = driver.Id, TraitId = e })
                    .ToList();

                context.AddRange(assignableTraits);
            }

            if (driver.RemovedTraitIds.Count != 0)
            {
                var removeableTraits = await context.DriverTrait
                    .Where(e => e.DriverId == driver.Id
                        && driver.RemovedTraitIds.Contains(e.TraitId))
                    .ToListAsync();

                context.RemoveRange(removeableTraits);
            }
        }

        await context.SaveChangesAsync();
    }

    public async Task AssignTeamTraits(List<EntrantAssignee> assignedTeams)
    {
        using var context = _dbFactory.CreateDbContext();

        // Assume now that we can't have already assigned traits

        // Assume we aren't removing traits

        var teamTraits = new List<TeamTrait>();

        foreach (var team in assignedTeams)
        {
            foreach (var trait in team.AssignedTraitIds)
            {
                teamTraits.Add(new TeamTrait { TeamId = team.Id, TraitId = trait });
            }
        }

        context.AddRange(teamTraits);

        await context.SaveChangesAsync();
    }

    public async Task AssignTrackTraits(List<EntrantAssignee> assignedTracks)
    {
        using var context = _dbFactory.CreateDbContext();

        // Assume now that we can't have already assigned traits

        // Assume we aren't removing traits

        var trackTraits = new List<TrackTrait>();

        foreach (var track in assignedTracks)
        {
            foreach (var trait in track.AssignedTraitIds)
            {
                trackTraits.Add(new TrackTrait { TrackId = track.Id, TraitId = trait });
            }
        }

        context.AddRange(trackTraits);

        await context.SaveChangesAsync();
    }
    #endregion
}

