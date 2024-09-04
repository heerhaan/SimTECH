using Microsoft.EntityFrameworkCore;

namespace SimTECH.Data.Services;

// NOTE: Kind of breaks a project structure rule; then again this code doesn't fit elsewhere
public class CommonService(IDbContextFactory<SimTechDbContext> factory)
{
    private readonly IDbContextFactory<SimTechDbContext> _dbFactory = factory;

    public async Task InsertDefaultData()
    {
        using var context = _dbFactory.CreateDbContext();

        await context.Climate.AddRangeAsync(DefaultData.DefaultClimates);
        await context.Incident.AddRangeAsync(DefaultData.DefaultIncidents);
        await context.Manufacturer.AddRangeAsync(DefaultData.DefaultManufacturers);
        await context.Track.AddRangeAsync(DefaultData.DefaultTracks);
        await context.Trait.AddRangeAsync(DefaultData.DefaultTraits);
        await context.Tyre.AddRangeAsync(DefaultData.DefaultTyres);

        await context.SaveChangesAsync();
    }
}
