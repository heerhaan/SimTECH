using SimTECH.Data.Models;
using SimTECH.Pages.Season;

namespace SimTECH.Data.EditModels
{
    public class EditSeasonDriverModel
    {
        private readonly SeasonDriver _seasonDriver;

        public long Id { get; set; }
        public int Number { get; set; }
        public int Skill { get; set; }
        public int Reliability { get; set; }
        public TeamRole TeamRole { get; set; }
        public long SeasonId { get; set; }
        public long DriverId { get; set; }
        public long? SeasonTeamId { get; set; }

        private int Points { get; set; }
        private int HiddenPoints { get; set; }

        // Supportive properties
        public long BaseTeamId { get; set; }
        public Driver? Driver { get; set; }

        public EditSeasonDriverModel() { _seasonDriver = new SeasonDriver(); }
        public EditSeasonDriverModel(SeasonDriver seasonDriver)
        {
            Id = seasonDriver.Id;
            Number = seasonDriver.Number;
            Skill = seasonDriver.Skill;
            Reliability = seasonDriver.Reliability;
            TeamRole = seasonDriver.TeamRole;
            SeasonId = seasonDriver.SeasonId;
            DriverId = seasonDriver.DriverId;
            SeasonTeamId = seasonDriver.SeasonTeamId;
            Driver = seasonDriver.Driver;

            Points = seasonDriver.Points;
            HiddenPoints = seasonDriver.HiddenPoints;

            _seasonDriver = seasonDriver;
        }

        public void ResetIdentifierFields()
        {
            Id = default;
            SeasonId = default;
            SeasonTeamId = default;
            Points = default;
            HiddenPoints = default;
        }

        public void SetSeasonId(long seasonId) => SeasonId = seasonId;

        public SeasonDriver Record =>
            new()
            {
                Id = Id,
                Number = Number,
                Skill = Skill,
                Reliability = Reliability,
                TeamRole = TeamRole,
                SeasonId = SeasonId,
                DriverId = DriverId,
                SeasonTeamId = SeasonTeamId,
                Points = Points,
                HiddenPoints = HiddenPoints,
            };

        public bool IsDirty => _seasonDriver != Record;
    }
}
