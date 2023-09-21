using Microsoft.EntityFrameworkCore;

namespace SimTECH.Data.Services;

// Yes this service is unique in that it doesnt belong to a single "Model", it breaks a design rule but lmao who cares
public class CommonService
{
    private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

    public CommonService(IDbContextFactory<SimTechDbContext> factory)
    {
        _dbFactory = factory;
    }

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
