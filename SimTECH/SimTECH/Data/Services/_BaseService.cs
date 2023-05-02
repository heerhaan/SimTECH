using Microsoft.EntityFrameworkCore;

namespace SimTECH.Data.Services
{
    public class BaseService<T>
    {
        protected readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public BaseService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }
    }
}
