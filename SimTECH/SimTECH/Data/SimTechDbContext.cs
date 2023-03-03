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

        public DbSet<Contract> Contracts => Set<Contract>();
        public DbSet<DevelopmentRange> DevelopmentRanges => Set<DevelopmentRange>();
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<DriverTrait> DriverTraits => Set<DriverTrait>();
        public DbSet<Engine> Engines => Set<Engine>();
        public DbSet<League> Leagues => Set<League>();
        public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
        public DbSet<Penalty> Penaltys => Set<Penalty>();
        public DbSet<PointAllotment> PointAllotments => Set<PointAllotment>();
        public DbSet<Race> Races => Set<Race>();
        public DbSet<Result> Results => Set<Result>();
        public DbSet<Season> Seasons => Set<Season>();
        public DbSet<SeasonDriver> SeasonDrivers => Set<SeasonDriver>();
        public DbSet<SeasonEngine> SeasonEngines => Set<SeasonEngine>();
        public DbSet<SeasonTeam> SeasonTeams => Set<SeasonTeam>();
        public DbSet<Sponsor> Sponsors => Set<Sponsor>();
        public DbSet<Stint> Stints => Set<Stint>();
        public DbSet<StintResult> StintResults => Set<StintResult>();
        public DbSet<Strategy> Strategys => Set<Strategy>();
        public DbSet<StrategyTyre> StrategyTyres => Set<StrategyTyre>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<TeamTrait> TeamTraits => Set<TeamTrait>();
        public DbSet<Track> Tracks => Set<Track>();
        public DbSet<TrackTrait> TrackTraits => Set<TrackTrait>();
        public DbSet<Trait> Traits => Set<Trait>();
        public DbSet<Tyre> Tyres => Set<Tyre>();
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
