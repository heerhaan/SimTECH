using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using System.Reflection;

namespace SimTECH.Data;

public class SimTechDbContext(DbContextOptions<SimTechDbContext> options) : DbContext(options)
{
    public DbSet<Climate> Climate => Set<Climate>();
    public DbSet<Contract> Contract => Set<Contract>();
    public DbSet<DevelopmentLog> DevelopmentLog => Set<DevelopmentLog>();
    public DbSet<DevelopmentRange> DevelopmentRange => Set<DevelopmentRange>();
    public DbSet<Driver> Driver => Set<Driver>();
    public DbSet<DriverTrait> DriverTrait => Set<DriverTrait>();
    public DbSet<Engine> Engine => Set<Engine>();
    public DbSet<GivenPenalty> GivenPenalty => Set<GivenPenalty>();
    public DbSet<Incident> Incident => Set<Incident>();
    public DbSet<LapScore> LapScore => Set<LapScore>();
    public DbSet<League> League => Set<League>();
    public DbSet<LeagueTyre> LeagueTyre => Set<LeagueTyre>();
    public DbSet<Manufacturer> Manufacturer => Set<Manufacturer>();
    public DbSet<PointAllotment> PointAllotment => Set<PointAllotment>();
    public DbSet<PracticeScore> PracticeScore => Set<PracticeScore>();
    public DbSet<QualifyingScore> QualifyingScore => Set<QualifyingScore>();
    public DbSet<Race> Race => Set<Race>();
    public DbSet<RaceOccurrence> RaceOccurrence => Set<RaceOccurrence>();
    public DbSet<Result> Result => Set<Result>();
    public DbSet<Season> Season => Set<Season>();
    public DbSet<SeasonDriver> SeasonDriver => Set<SeasonDriver>();
    public DbSet<SeasonEngine> SeasonEngine => Set<SeasonEngine>();
    public DbSet<SeasonTeam> SeasonTeam => Set<SeasonTeam>();
    public DbSet<Sponsor> Sponsor => Set<Sponsor>();
    public DbSet<Team> Team => Set<Team>();
    public DbSet<TeamTrait> TeamTrait => Set<TeamTrait>();
    public DbSet<Track> Track => Set<Track>();
    public DbSet<TrackTrait> TrackTrait => Set<TrackTrait>();
    public DbSet<Trait> Trait => Set<Trait>();
    public DbSet<Tyre> Tyre => Set<Tyre>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var dbInit = new DataInitializer(builder);
        dbInit.Seed();
    }
}
