using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class TraitService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public TraitService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Trait>> GetTraits()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Trait.ToListAsync();
        }

        public async Task CreateTrait(Trait trait)
        {
            ValidateTrait(trait);

            using var context = _dbFactory.CreateDbContext();
            context.Add(trait);

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
