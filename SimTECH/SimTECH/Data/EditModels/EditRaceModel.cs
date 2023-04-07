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
        public Weather Weather { get; set; }
        public State State { get; set; }

        public long SeasonId { get; set; }
        public long TrackId { get; set; }

        public EditRaceModel() { _race = new Race(); }
        public EditRaceModel(Race race)
        {
            Id = race.Id;
            Round = race.Round;
            Name = race.Name;
            RaceLength = race.RaceLength;
            Weather = race.Weather;
            State = race.State;
            SeasonId = race.SeasonId;
            TrackId = race.TrackId;

            _race = race;
        }

        public Race Record =>
            new()
            {
                Id = Id,
                Round = Round,
                Name = Name,
                RaceLength = RaceLength,
                Weather = Weather,
                State = State,
                SeasonId = SeasonId,
                TrackId = TrackId,
            };

        public bool IsDirty => _race != Record;

        public void ResetIdentifierFields()
        {
            Id = default;
            Weather = default;
            State = State.Concept;
            SeasonId = default;
        }
    }
}
