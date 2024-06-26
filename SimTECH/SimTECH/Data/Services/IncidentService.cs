﻿using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.Data.Services.Interfaces;

namespace SimTECH.Data.Services;

public class IncidentService(IDbContextFactory<SimTechDbContext> factory)
    : StateService<Incident>(factory), IIncidentService
{
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
}
