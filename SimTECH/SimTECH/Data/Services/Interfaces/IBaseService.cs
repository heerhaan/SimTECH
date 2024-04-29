using SimTECH.Data.Models;

namespace SimTECH.Data.Services.Interfaces;

public interface IBaseService<T> where T : ModelBase
{
    public Task<T> GetItemById(long id);

    public Task BulkAddItems(List<T> items);
}
