using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.Data.Services.Interfaces;

namespace SimTECH.UnitTests.Data.Services;

public class IncidentService : IIncidentService
{
    public Task<Incident> GetItemById(long id)
    {
        throw new NotImplementedException();
    }

    public Task BulkAddItems(List<Incident> items)
    {
        throw new NotImplementedException();
    }

    public Task ArchiveItem(Incident item)
    {
        throw new NotImplementedException();
    }

    public Task ChangeState(Incident item, State targetState)
    {
        throw new NotImplementedException();
    }

    public Task<List<Incident>> GetIncidents() => Task.FromResult(new List<Incident>());

    public Task<List<Incident>> GetIncidents(StateFilter filter)
    {
        return Task.FromResult(new List<Incident>());
    }

    public Task UpdateIncident(Incident incident)
    {
        throw new NotImplementedException();
    }
}
