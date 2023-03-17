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
        public long TeamId { get; set; }
        public long SeasonId { get; set; }
        public long SeasonEngineId { get; set; }
        public long ManufacturerId { get; set; }

        // Supportive properties
        public string Identifier { get; set; } = new Guid().ToString();
        public string Identified { get; set; } = Guid.Empty.ToString();
        public Team? Team { get; set; }

        public EditSeasonTeamModel() { _seasonTeam = new SeasonTeam(); }
        public EditSeasonTeamModel(SeasonTeam seasonTeam)
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

            _seasonTeam = seasonTeam;
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
                ManufacturerId = ManufacturerId,
            };

        public bool IsDirty => _seasonTeam != Record;

        public void ResetIdentifierFields()
        {
            Id = default;
            SeasonId = default;
            SeasonEngineId = default;
        }
    }
}
