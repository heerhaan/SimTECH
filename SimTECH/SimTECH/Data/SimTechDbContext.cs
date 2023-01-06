using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data
{
    public class SimTechDbContext : DbContext
    {
        public SimTechDbContext(DbContextOptions<SimTechDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
