﻿using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services;

public class TyreService(IDbContextFactory<SimTechDbContext> factory) : StateService<Tyre>(factory)
{
    public async Task<List<Tyre>> GetTyres() => await GetTyres(StateFilter.Default);
    public async Task<List<Tyre>> GetTyres(StateFilter filter)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Tyre
            .Where(e => filter.StatesForFilter().Contains(e.State))
            .Include(e => e.LeagueTyres)
            .ToListAsync();
    }

    public async Task UpdateTyre(Tyre tyre)
    {
        using var context = _dbFactory.CreateDbContext();

        if (tyre.Id == 0)
        {
            tyre.State = State.Active;
            context.Add(tyre);
        }
        else
            context.Update(tyre);

        await context.SaveChangesAsync();
    }
}
