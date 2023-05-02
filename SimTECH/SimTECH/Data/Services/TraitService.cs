using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Extensions;

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
            ValidateTrait(trait);

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

        #region validation

        private static void ValidateTrait(Trait trait)
        {
            if (trait == null)
                throw new NullReferenceException("Trait is very null here, yes");
        }

        #endregion
    }
}
