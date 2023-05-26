using SimTECH.Data.Models;
using System.ComponentModel.DataAnnotations;

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
        public long ManufacturerId { get; set; }

        public int Points { get; set; }
        public int HiddenPoints { get; set; }

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
            Points = default;
            HiddenPoints = default;

            if (SeasonDrivers?.Any() == true)
            {
                foreach (var driver in SeasonDrivers)
                    driver.ResetIdentifierFields();
            }
        }

        public void SetSeasonIdForAll(long seasonId)
        {
            SeasonId = seasonId;

            if (SeasonDrivers?.Any() == true)
            {
                foreach (var driver in SeasonDrivers)
                    driver.SetSeasonId(seasonId);
            }
        }

        public void Validate(List<string> errors)
        {
            if (string.IsNullOrEmpty(Name))
                errors.Add("Team lacks a name");

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
                ManufacturerId = ManufacturerId == 0 ? 1 : ManufacturerId,
                SeasonDrivers = SeasonDrivers?.Select(e => e.Record).ToList(),

                Points = Points,
                HiddenPoints = HiddenPoints,
            };

        public bool IsDirty => _seasonTeam != Record;
    }
}
