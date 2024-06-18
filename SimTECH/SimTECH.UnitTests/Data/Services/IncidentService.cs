using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.Data.Services.Interfaces;
using SimTECH.Tests.Data.Factories;

namespace SimTECH.Tests.Data.Services;

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

    public Task<List<Incident>> GetIncidents() => GetIncidents(StateFilter.Active);

    public Task<List<Incident>> GetIncidents(StateFilter filter)
    {
        var incidents = IncidentFactory.GenerateTestIncidentList;

        return Task.FromResult(incidents);
    }

    public Task UpdateIncident(Incident incident)
    {
        throw new NotImplementedException();
    }
}
