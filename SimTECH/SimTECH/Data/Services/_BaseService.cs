using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public abstract class BaseService<T> where T : ModelBase
    {
        protected readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public BaseService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }
    }

    public abstract class StateService<T> : BaseService<T> where T : ModelState
    {
        public StateService(IDbContextFactory<SimTechDbContext> factory) : base(factory) { }

        public async Task ChangeState(T item, State targetState)
        {
            using var context = _dbFactory.CreateDbContext();

            item.State = targetState;
            context.Update(item);

            await context.SaveChangesAsync();
        }
    }
}
