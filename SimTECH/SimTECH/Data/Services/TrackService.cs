﻿using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class TrackService
    {
        private readonly IDbContextFactory<SimTechDbContext> _dbFactory;

        public TrackService(IDbContextFactory<SimTechDbContext> factory)
        {
            _dbFactory = factory;
        }

        public async Task<List<Track>> GetTracks()
        {
            using var context = _dbFactory.CreateDbContext();

            return await context.Track.ToListAsync();
        }

        public async Task UpdateTrack(Track track)
        {
            ValidateTrack(track);

            using var context = _dbFactory.CreateDbContext();

            if (track.Id == 0)
                context.Add(track);
            else
                context.Update(track);

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
