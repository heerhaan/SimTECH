using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SimTECH.Data
{
    public class SimTechDbContext : IdentityDbContext
    {
        public SimTechDbContext(DbContextOptions<SimTechDbContext> options) : base(options)
        {

        }
    }
}