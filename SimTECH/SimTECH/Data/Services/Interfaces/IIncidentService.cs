using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services.Interfaces;

public interface IIncidentService : IStateService<Incident>
{
    public Task<List<Incident>> GetIncidents();

    public Task<List<Incident>> GetIncidents(StateFilter filter);

    public Task UpdateIncident(Incident incident);
}
