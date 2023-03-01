using SimTECH.Data.Models;
using SimTECH.Extensions;

namespace SimTECH.Data.Services
{
    public class TrackService
    {
        private readonly List<Track> _tracks = new()
        {
            new Track()
            {
                Id = 1,
                Name = "Test",
                Country = EnumHelper.GetDefaultCountry(),
                Length = NumberHelper.RandomDouble(2.00, 5.00),
                State = State.Active,
                AeroMod = NumberHelper.RandomDouble(0.5, 1.5),
                ChassisMod = NumberHelper.RandomDouble(0.5, 1.5),
                PowerMod = NumberHelper.RandomDouble(0.5, 1.5),
                QualifyingMod = NumberHelper.RandomDouble(0.5, 1.5),
            },
        };

        public List<Track> GetTestNames()
        {
            return _tracks;
        }

        public void CreateTrack(Track track)
        {
            ValidateTrack(track);

            _tracks.Add(track);
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
