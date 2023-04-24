using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public class TrackService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public TrackService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Track>> GetTracks() => await GetTracks(StateFilter.Default);
        public async Task<List<Track>> GetTracks(StateFilter filter)
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Track
                .Where(e => filter.StatesForFilter().Contains(e.State))
                .Include(e => e.TrackTraits)
                .ToListAsync();
        }

        public async Task UpdateTrack(Track track)
        {
            ValidateTrack(track);

            using var context = _dbFactory.CreateDbContext();

            if (track.Id == 0)
            {
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

        #region validation

        private static void ValidateTrack(Track track)
        {
            if (track == null)
                throw new NullReferenceException("Track is very null here, yes");
        }

        #endregion
    }
}
