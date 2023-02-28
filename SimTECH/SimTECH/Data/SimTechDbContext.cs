using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using System.Reflection;

namespace SimTECH.Data
{
    public class SimTechDbContext : DbContext
    {
        public SimTechDbContext(DbContextOptions<SimTechDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var key in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                key.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
