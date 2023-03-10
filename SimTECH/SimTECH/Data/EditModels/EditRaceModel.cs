using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditRaceModel
    {
        private readonly Race _race;

        public long Id { get; set; }
        public int Round { get; set; }
        public string Name { get; set; } = string.Empty;
        public Weather Weather { get; set; }
        public State State { get; set; }

        public long SeasonId { get; set; }
        public long TrackId { get; set; }

        public IList<EditStintModel> Stints { get; set; } = new List<EditStintModel>();

        public EditRaceModel() { _race = new Race(); }
        public EditRaceModel(Race race)
        {
            Id = race.Id;
            Round= race.Round;
            Name = race.Name;
            Weather = race.Weather;
            State= race.State;
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
                Weather = Weather,
                State = State,
                SeasonId = SeasonId,
                TrackId = TrackId
            };

        public bool IsDirty => _race != Record || Stints.Any(e => e.IsDirty);

        public void ResetIdentifierFields()
        {
            Id = default;
            Weather = default;
            State = State.Concept;
            SeasonId = default;

            foreach (var stint in Stints)
                stint.ResetIdentifierFields();
        }
    }

    public class EditStintModel
    {
        private readonly Stint _stint;

        public long Id { get; set; }
        public int Order { get; set; }
        public StintEvent StintEvents { get; set; }
        public int RngMin { get; set; }
        public int RngMax { get; set; }
        public long RaceId { get; set; }

        public EditStintModel() { _stint = new Stint(); }
        public EditStintModel(Stint stint)
        {
            Id = stint.Id;
            Order = stint.Order;
            StintEvents = stint.StintEvents;
            RngMin= stint.RngMin;
            RngMax= stint.RngMax;
            RaceId = stint.RaceId;

            _stint = stint;
        }

        public Stint Record =>
            new()
            {
                Id = Id,
                Order= Order,
                StintEvents= StintEvents,
                RngMin= RngMin,
                RngMax= RngMax,
                RaceId= RaceId
            };

        public bool IsDirty => _stint != Record;

        public void ResetIdentifierFields()
        {
            Id = default;
            RaceId = default;
        }
    }
}
