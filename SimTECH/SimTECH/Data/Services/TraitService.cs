using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels;
using SimTECH.Pages.Setup;

namespace SimTECH.Data.Services
{
    public class TraitService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public TraitService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Trait>> GetTraits() => await GetTraits(StateFilter.Default);
        public async Task<List<Trait>> GetTraits(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Trait
                .Where(e => filter.StatesForFilter().Contains(e.State))
                .ToListAsync();
        }

        public async Task<List<Trait>> GetTraitsOfType(Entrant type)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Trait.Where(e => e.Type == type).ToListAsync();
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
                context.Update(trait);

            await context.SaveChangesAsync();
        }

        // I suspect the following three methods are a good target for generics, if i implemented those
        public async Task AssignDriverTraits(List<TraitAssigner> assignedDrivers)
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

        public async Task AssignTeamTraits(List<TraitAssigner> assignedTeams)
        {
            using var context = _dbFactory.CreateDbContext();

            // Assume now that we can't have already assigned traits

            // Assume we aren't removing traits

            var assignedDriverTraits = new List<DriverTrait>();

            foreach (var driver in assignedTeams)
            {
                foreach (var trait in driver.AssignedTraitIds)
                {
                    assignedDriverTraits.Add(new DriverTrait { DriverId = driver.Id, TraitId = trait });
                }
            }

            context.AddRange(assignedDriverTraits);

            await context.SaveChangesAsync();
        }

        public async Task AssignTrackTraits(List<TraitAssigner> assignedTracks)
        {
            using var context = _dbFactory.CreateDbContext();

            // Assume now that we can't have already assigned traits

            // Assume we aren't removing traits

            var assignedDriverTraits = new List<DriverTrait>();

            foreach (var driver in assignedTracks)
            {
                foreach (var trait in driver.AssignedTraitIds)
                {
                    assignedDriverTraits.Add(new DriverTrait { DriverId = driver.Id, TraitId = trait });
                }
            }

            context.AddRange(assignedDriverTraits);

            await context.SaveChangesAsync();
        }
    }
}
