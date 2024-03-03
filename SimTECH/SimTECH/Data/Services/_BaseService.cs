using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Common.Enums;

namespace SimTECH.Data.Services;

public abstract class BaseService<T>(IDbContextFactory<SimTechDbContext> factory) where T : ModelBase
{
    protected readonly IDbContextFactory<SimTechDbContext> _dbFactory = factory;

    public async Task<T> GetItemById(long id)
    {
        using var context = _dbFactory.CreateDbContext();

        var entity = context.Set<T>();

        return await entity.FirstAsync(x => x.Id == id);
    }

    public async Task BulkAddItems(List<T> items)
    {
        using var context = _dbFactory.CreateDbContext();

        context.AddRange(items);

        await context.SaveChangesAsync();
    }
}

public abstract class StateService<T>(IDbContextFactory<SimTechDbContext> factory) : BaseService<T>(factory) where T : ModelState
{
    public async Task ChangeState(T item, State targetState)
    {
        using var context = _dbFactory.CreateDbContext();

        item.State = targetState;
        context.Update(item);

        await context.SaveChangesAsync();
    }

    public async Task ArchiveItem(T item)
    {
        using var context = _dbFactory.CreateDbContext();

        if (item.State == State.Archived)
            item.State = State.Active;
        else
            item.State = State.Archived;

        context.Update(item);

        await context.SaveChangesAsync();
    }
}
