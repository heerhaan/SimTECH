using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditTrackModel
    {
        private readonly Track _track;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Country Country { get; set; }
        public double Length { get; set; }
        public State State { get; set; }
        public double AeroMod { get; set; }
        public double ChassisMod { get; set; }
        public double PowerMod { get; set; }
        public double QualifyingMod { get; set; }

        public List<long> TraitIds { get; set; } = new();

        public EditTrackModel() { _track= new Track(); }
        public EditTrackModel(Track track)
        {
            Id = track.Id;
            Name = track.Name;
            Country = track.Country;
            Length = track.Length;
            State = track.State;
            AeroMod = track.AeroMod;
            ChassisMod = track.ChassisMod;
            PowerMod = track.PowerMod;
            QualifyingMod = track.QualifyingMod;

            if (track.TrackTraits != null)
                TraitIds = track.TrackTraits.Select(e => e.TraitId).ToList();

            _track = track;
        }

        public Track Record =>
            new()
            {
                Id = Id,
                Name = Name,
                Country = Country,
                Length = Length,
                State = State,
                AeroMod = AeroMod,
                ChassisMod = ChassisMod,
                PowerMod = PowerMod,
                QualifyingMod = QualifyingMod,

                TrackTraits = TraitIds.ConvertAll(e => new TrackTrait { TraitId = e, TrackId = Id })
            };

        public bool IsDirty => _track != Record;
    }
}
