using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class EditSeasonTeamModel
    {
        private readonly SeasonTeam _seasonTeam;

        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Principal { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public string Accent { get; set; } = string.Empty;
        public int BaseValue { get; set; }
        public int Aero { get; set; }
        public int Chassis { get; set; }
        public int Powertrain { get; set; }
        public int Reliability { get; set; }
        public long SeasonEngineId { get; set; }
        public long TeamId { get; set; }
        public long SeasonId { get; set; }
        public long? ClassId { get; set; }
        public long ManufacturerId { get; set; }

        public int Points { get; set; }
        public double HiddenPoints { get; set; }

        public IList<EditSeasonDriverModel>? SeasonDrivers { get; set; }

        // Supportive properties
        public long BaseEngineId { get; set; }
        public Team? Team { get; set; }

        public EditSeasonTeamModel(SeasonTeam? seasonTeam)
        {
            if (seasonTeam == null)
            {
                Principal = "Mr. Principal";
                Colour = "#ffffff";
                Accent = "#000000";
                BaseValue = 100;
                Aero = 20;
                Chassis = 20;
                Powertrain = 20;
                Reliability = 1000;

                _seasonTeam = new();
            }
            else
            {
                Id = seasonTeam.Id;
                Name = seasonTeam.Name;
                Principal = seasonTeam.Principal;
                Colour = seasonTeam.Colour;
                Accent = seasonTeam.Accent;
                BaseValue = seasonTeam.BaseValue;
                Aero = seasonTeam.Aero;
                Chassis = seasonTeam.Chassis;
                Powertrain = seasonTeam.Powertrain;
                Reliability = seasonTeam.Reliability;
                TeamId = seasonTeam.TeamId;
                SeasonId = seasonTeam.SeasonId;
                SeasonEngineId = seasonTeam.SeasonEngineId;
                ClassId = seasonTeam.ClassId;
                ManufacturerId = seasonTeam.ManufacturerId;
                Team = seasonTeam.Team;

                Points = seasonTeam.Points;
                HiddenPoints = seasonTeam.HiddenPoints;

                if (seasonTeam.SeasonDrivers?.Any() == true)
                    SeasonDrivers = seasonTeam.SeasonDrivers.Select(e => new EditSeasonDriverModel(e)).ToList();

                _seasonTeam = seasonTeam;
            }
        }

        public void ResetIdentifierFields()
        {
            Id = default;
            SeasonId = default;
            SeasonEngineId = default;
            ClassId = default;
            Points = default;
            HiddenPoints = default;

            if (SeasonDrivers?.Any() == true)
            {
                foreach (var driver in SeasonDrivers)
                    driver.ResetIdentifierFields();
            }
        }

        public void Validate(List<string> errors)
        {
            string teamName = Team?.Name ?? "Unknown Team";

            if (string.IsNullOrEmpty(Name))
                errors.Add($"{teamName} lacks a name.");

            if (ManufacturerId == 0)
                errors.Add($"{teamName} is not a boat, thus it needs a tyre manufacturer. Add it or else...");

            if (string.IsNullOrEmpty(Colour) || string.IsNullOrEmpty(Accent))
                errors.Add($"Put some colours on {teamName} please.");

            // TODO: More validation messages
        }

        public SeasonTeam Record =>
            new()
            {
                Id = Id,
                Name = Name,
                Principal = Principal,
                Colour = Colour,
                Accent = Accent,
                BaseValue = BaseValue,
                Aero = Aero,
                Chassis = Chassis,
                Powertrain = Powertrain,
                Reliability = Reliability,
                TeamId = TeamId,
                SeasonId = SeasonId,
                SeasonEngineId = SeasonEngineId,
                ClassId = ClassId,
                ManufacturerId = ManufacturerId == 0 ? 1 : ManufacturerId,
                SeasonDrivers = SeasonDrivers?.Select(e => e.Record).ToList() ?? new(),

                Points = Points,
                HiddenPoints = HiddenPoints,
            };

        public bool IsDirty => _seasonTeam != Record;
    }
}
