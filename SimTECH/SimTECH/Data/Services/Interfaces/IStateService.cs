using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services.Interfaces;

public interface IStateService<T> : IBaseService<T> where T : ModelState
{
    public Task ChangeState(T item, State targetState);

    public Task ArchiveItem(T item);
}
