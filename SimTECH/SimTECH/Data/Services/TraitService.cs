using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels;

namespace SimTECH.Data.Services
{
    public class TraitService : StateService<Trait>
    {
        public TraitService(IDbContextFactory<SimTechDbContext> factory) : base(factory) { }

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
                foreach (var trait in driver.AssignedTraitIds)
                {
                    assignedDriverTraits.Add(new DriverTrait { DriverId = driver.Id, TraitId = trait });
                }
            }

            context.AddRange(assignedDriverTraits);

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
}
