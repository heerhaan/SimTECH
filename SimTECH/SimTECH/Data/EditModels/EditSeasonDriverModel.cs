using SimTECH.Data.Models;

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

            _seasonDriver = seasonDriver;
        }

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
            };

        public bool IsDirty => _seasonDriver != Record;
    }
}
