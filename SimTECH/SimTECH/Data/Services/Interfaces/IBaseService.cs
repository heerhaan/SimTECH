using SimTECH.Data.Models;

namespace SimTECH.Data.Services.Interfaces;

// NOTE: HUGE problem with this feature; the assumption that one service is for one entity
public interface IBaseService<T> where T : ModelBase
{
    public Task<T> GetItemById(long id);

    public Task BulkAddItems(List<T> items);
}
