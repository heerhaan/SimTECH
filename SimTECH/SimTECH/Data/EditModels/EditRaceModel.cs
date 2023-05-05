using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditRaceModel
    {
        private readonly Race _race;

        public long Id { get; set; }
        public int Round { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RaceLength { get; set; }
        public State State { get; set; }

        public long SeasonId { get; set; }
        public long TrackId { get; set; }
        public long ClimateId { get; set; }

        public EditRaceModel() { _race = new Race(); }
        public EditRaceModel(Race race)
        {
            Id = race.Id;
            Round = race.Round;
            Name = race.Name;
            RaceLength = race.RaceLength;
            State = race.State;
            SeasonId = race.SeasonId;
            TrackId = race.TrackId;
            ClimateId = race.ClimateId;

            _race = race;
        }

        public Race Record =>
            new()
            {
                Id = Id,
                Round = Round,
                Name = Name,
                RaceLength = RaceLength,
                State = State,
                SeasonId = SeasonId,
                TrackId = TrackId,
                ClimateId = ClimateId,
            };

        public bool IsDirty => _race != Record;

        public void ResetIdentifierFields()
        {
            Id = default;
            SeasonId = default;
        }
    }
}
