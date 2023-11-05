using SimTECH.Data.Models;
using SimTECH.Common.Enums;

namespace SimTECH.Data.EditModels
{
    public class EditRaceModel
    {
        private readonly Race _race;

        public long Id { get; set; }
        public int Round { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RaceLength { get; set; } = 300;
        public State State { get; set; }

        public long SeasonId { get; set; }
        public long TrackId { get; set; }
        public long ClimateId { get; set; }

        public EditRaceModel() { _race = new Race(); }
        public EditRaceModel(Race? race)
        {
            if (race == null)
            {
                _race = new Race();
            }
            else
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

        public List<string> RunValidations()
        {
            var errorValidations = new List<string>();

            if ((RaceLength % 10) != 0)
            {
                errorValidations.Add("Race length should be a factor of 10");
            }

            return errorValidations;
        }
    }
}
