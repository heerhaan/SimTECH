using Microsoft.EntityFrameworkCore;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services;

public class TrackService(IDbContextFactory<SimTechDbContext> factory) : StateService<Track>(factory)
{
    public async Task<List<Track>> GetTracks() => await GetTracks(StateFilter.Default);
    public async Task<List<Track>> GetTracks(StateFilter filter)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Track
            .Where(e => filter.StatesForFilter().Contains(e.State))
            .Include(e => e.TrackTraits)
            .ToListAsync();
    }

    public async Task<List<Track>> GetAvailableTracks(long seasonId, StateFilter filter = StateFilter.Active)
    {
        using var context = _dbFactory.CreateDbContext();

        return await context.Track
            .Where(e => filter.StatesForFilter().Contains(e.State)
                && !e.Races.Any(r => r.SeasonId == seasonId))
            .Include(e => e.TrackTraits)
            .ToListAsync();
    }

    public async Task UpdateTrack(Track track)
    {
        using var context = _dbFactory.CreateDbContext();

        if (track.Id == 0)
        {
            track.State = State.Active;
            context.Add(track);
        }
        else
        {
            var removeables = await context.TrackTrait
                    .Where(e => e.TrackId == track.Id)
                    .ToListAsync();

            if (removeables.Any())
                context.RemoveRange(removeables);

            if (track.TrackTraits?.Any() ?? false)
                context.AddRange(track.TrackTraits);

            context.Update(track);
        }

        await context.SaveChangesAsync();
    }

    public async Task ArchiveTrack(Track track)
    {
        using var context = _dbFactory.CreateDbContext();

        if (track.State == State.Archived)
        {
            track.State = State.Active;
        }
        else
        {
            if (context.Race.Any(e => e.TrackId == track.Id))
            {
                track.State = State.Archived;
                context.Update(track);
            }
            else
            {
                context.Remove(track);
            }
        }

        await context.SaveChangesAsync();
    }
}

