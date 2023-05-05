using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public class IncidentService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public IncidentService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Incident>> GetIncidents() => await GetIncidents(StateFilter.Default);
        public async Task<List<Incident>> GetIncidents(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Incident.Where(e => filter.StatesForFilter().Contains(e.State)).ToListAsync();
        }

        public async Task UpdateIncident(Incident incident)
        {
            using var context = _dbFactory.CreateDbContext();

            if (incident.Id == 0)
            {
                incident.State = State.Active;
                context.Add(incident);
            }
            else
            {
                context.Update(incident);
            }

            await context.SaveChangesAsync();
        }

        public async Task ChangeState(Incident incident, State targetState)
        {
            using var context = _dbFactory.CreateDbContext();

            incident.State = targetState;
            context.Update(incident);

            await context.SaveChangesAsync();
        }
    }
}
