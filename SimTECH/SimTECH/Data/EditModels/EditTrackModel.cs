using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.Common.Enums;

namespace SimTECH.Data.EditModels
{
    public class EditTrackModel
    {
        private readonly Track _track;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Country Country { get; set; } = Globals.DefaultCountry;
        public double Length { get; set; } = 4d;
        public State State { get; set; }
        public double AeroMod { get; set; } = 1d;
        public double ChassisMod { get; set; } = 1d;
        public double PowerMod { get; set; } = 1d;
        public double QualifyingMod { get; set; } = 1d;
        public double DefenseMod { get; set; } = 1d;

        public IList<EditTrackTraitModel> TrackTraits { get; set; } = new List<EditTrackTraitModel>();

        public EditTrackModel(Track? track)
        {
            if (track == null)
            {
                _track = new();
            }
            else
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
                DefenseMod = track.DefenseMod;

                if (track.TrackTraits != null)
                    TrackTraits = track.TrackTraits.Select(e => new EditTrackTraitModel(e)).ToList();

                _track = track;
            }
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
                DefenseMod = DefenseMod,

                TrackTraits = TrackTraits.Select(e => e.Record).ToList()
            };

        public bool IsDirty => _track != Record || TrackTraits.Any(e => e.IsDirty);
    }

    public class EditTrackTraitModel
    {
        private readonly TrackTrait _trackTrait;

        public long TrackId { get; set; }
        public long TraitId { get; set; }

        public EditTrackTraitModel() { _trackTrait = new TrackTrait(); }
        public EditTrackTraitModel(TrackTrait trackTrait)
        {
            TrackId = trackTrait.TrackId;
            TraitId = trackTrait.TraitId;

            _trackTrait = trackTrait;
        }

        public TrackTrait Record =>
            new()
            {
                TrackId = TrackId,
                TraitId = TraitId
            };

        public bool IsDirty => _trackTrait != Record;
    }
}
