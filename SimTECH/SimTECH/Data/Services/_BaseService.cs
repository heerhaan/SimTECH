using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public abstract class BaseService<T> where T : ModelBase
    {
        protected readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        protected BaseService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }
    }

    public abstract class StateService<T> : BaseService<T> where T : ModelState
    {
        protected StateService(IDbContextFactory<SimTechDbContext> factory) : base(factory) { }

        public async Task ChangeState(T item, State targetState)
        {
            using var context = _dbFactory.CreateDbContext();

            item.State = targetState;
            context.Update(item);

            await context.SaveChangesAsync();
        }

        public async Task ArchiveItem(T item)
        {
            using var context = _dbFactory.CreateDbContext();

            if (item.State == State.Archived)
                item.State = State.Active;
            else
                item.State = State.Archived;

            context.Update(item);

            await context.SaveChangesAsync();
        }
    }
}
