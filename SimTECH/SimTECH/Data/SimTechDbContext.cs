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

        public DbSet<Contract> Contract => Set<Contract>();
        public DbSet<DevelopmentRange> DevelopmentRange => Set<DevelopmentRange>();
        public DbSet<Driver> Driver => Set<Driver>();
        public DbSet<DriverTrait> DriverTrait => Set<DriverTrait>();
        public DbSet<Engine> Engine => Set<Engine>();
        public DbSet<League> League => Set<League>();
        public DbSet<Manufacturer> Manufacturer => Set<Manufacturer>();
        public DbSet<Penalty> Penalty => Set<Penalty>();
        public DbSet<PointAllotment> PointAllotment => Set<PointAllotment>();
        public DbSet<Race> Race => Set<Race>();
        public DbSet<Result> Result => Set<Result>();
        public DbSet<Season> Season => Set<Season>();
        public DbSet<SeasonDriver> SeasonDriver => Set<SeasonDriver>();
        public DbSet<SeasonEngine> SeasonEngine => Set<SeasonEngine>();
        public DbSet<SeasonTeam> SeasonTeam => Set<SeasonTeam>();
        public DbSet<Sponsor> Sponsor => Set<Sponsor>();
        public DbSet<Stint> Stint => Set<Stint>();
        public DbSet<StintResult> StintResult => Set<StintResult>();
        public DbSet<Strategy> Strategy => Set<Strategy>();
        public DbSet<StrategyTyre> StrategyTyre => Set<StrategyTyre>();
        public DbSet<Team> Team => Set<Team>();
        public DbSet<TeamTrait> TeamTrait => Set<TeamTrait>();
        public DbSet<Track> Track => Set<Track>();
        public DbSet<TrackTrait> TrackTrait => Set<TrackTrait>();
        public DbSet<Trait> Trait => Set<Trait>();
        public DbSet<Tyre> Tyre => Set<Tyre>();
        public DbSet<User> User => Set<User>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
