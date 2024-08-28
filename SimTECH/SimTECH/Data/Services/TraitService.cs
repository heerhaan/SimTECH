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
    // TODO: Following three methods are quite likely a decent target for a more generic implementation
    public async Task AssignDriverTraits(List<EntrantAssignee> assignedDrivers)
    {
        using var context = _dbFactory.CreateDbContext();

        foreach (var driver in assignedDrivers)
        {
            if (driver.AssignedTraits.Count != 0)
            {
                var assignableTraits = driver.AssignedTraits
                    .Select(e => new DriverTrait { DriverId = driver.Id, TraitId = e.Id })
                    .ToList();

                context.AddRange(assignableTraits);
                await context.SaveChangesAsync();
            }

            if (driver.RemovedTraits.Count != 0)
            {
                var removeableTraits = await context.DriverTrait
                    .Where(e => e.DriverId == driver.Id
                        && driver.RemovedTraits.Select(e => e.Id).Contains(e.TraitId))
                    .ToListAsync();

                context.RemoveRange(removeableTraits);
                await context.SaveChangesAsync();
            }
        }
    }

    public async Task AssignTeamTraits(List<EntrantAssignee> assignedTeams)
    {
        using var context = _dbFactory.CreateDbContext();

        foreach (var team in assignedTeams)
        {
            if (team.AssignedTraits.Count != 0)
            {
                var assignableTraits = team.AssignedTraits
                    .Select(e => new TeamTrait { TeamId = team.Id, TraitId = e.Id })
                    .ToList();

                context.AddRange(assignableTraits);
                await context.SaveChangesAsync();
            }

            if (team.RemovedTraits.Count != 0)
            {
                var removeableTraits = await context.DriverTrait
                    .Where(e => e.DriverId == team.Id
                        && team.RemovedTraits.Select(e => e.Id).Contains(e.TraitId))
                    .ToListAsync();

                context.RemoveRange(removeableTraits);
                await context.SaveChangesAsync();
            }
        }
    }

    public async Task AssignTrackTraits(List<EntrantAssignee> assignedTracks)
    {
        using var context = _dbFactory.CreateDbContext();

        foreach (var track in assignedTracks)
        {
            if (track.AssignedTraits.Count != 0)
            {
                var assignableTraits = track.AssignedTraits
                    .Select(e => new TrackTrait { TrackId = track.Id, TraitId = e.Id })
                    .ToList();

                context.AddRange(assignableTraits);
                await context.SaveChangesAsync();
            }

            if (track.RemovedTraits.Count != 0)
            {
                var removeableTraits = await context.DriverTrait
                    .Where(e => e.DriverId == track.Id
                        && track.RemovedTraits.Select(e => e.Id).Contains(e.TraitId))
                    .ToListAsync();

                context.RemoveRange(removeableTraits);
                await context.SaveChangesAsync();
            }
        }
    }
    #endregion
}

