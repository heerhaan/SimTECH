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
        public int Attack { get; set; }
        public int Defense { get; set; }
        public TeamRole TeamRole { get; set; }
        public long? SeasonTeamId { get; set; }
        public long SeasonId { get; set; }
        public long DriverId { get; set; }

        private int Points { get; set; }
        private double HiddenPoints { get; set; }

        // Supportive properties
        public long BaseTeamId { get; set; }
        public Driver? Driver { get; set; }
        public bool Contracted { get; set; }

        public EditSeasonDriverModel(SeasonDriver? seasonDriver)
        {
            if (seasonDriver == null)
            {
                _seasonDriver = new();
            }
            else
            {
                Id = seasonDriver.Id;
                Number = seasonDriver.Number;
                Skill = seasonDriver.Skill;
                Reliability = seasonDriver.Reliability;
                Attack = seasonDriver.Attack;
                Defense = seasonDriver.Defense;
                TeamRole = seasonDriver.TeamRole;
                SeasonId = seasonDriver.SeasonId;
                DriverId = seasonDriver.DriverId;
                SeasonTeamId = seasonDriver.SeasonTeamId;
                Driver = seasonDriver.Driver;

                Points = seasonDriver.Points;
                HiddenPoints = seasonDriver.HiddenPoints;

                _seasonDriver = seasonDriver;
            }
        }

        public void ResetIdentifierFields()
        {
            Id = default;
            SeasonId = default;
            SeasonTeamId = default;
            Points = default;
            HiddenPoints = default;
        }

        public void Validate(List<string> errors)
        {
            var driverName = Driver?.FullName ?? "Unknown Driver";

            if (Number < 0)
                errors.Add($"Number of {driverName} is negative, which is cringe and weird. Don't do that.");
        }

        public SeasonDriver Record =>
            new()
            {
                Id = Id,
                Number = Number,
                Skill = Skill,
                Reliability = Reliability,
                Attack = Attack,
                Defense = Defense,
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
