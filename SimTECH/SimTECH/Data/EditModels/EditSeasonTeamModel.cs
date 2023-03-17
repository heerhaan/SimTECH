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
            SeasonEngineId = seasonTeam.SeasonEngineId;
            ManufacturerId = seasonTeam.ManufacturerId;

            _seasonTeam = seasonTeam;
        }

        public void ResetIdentifierFields()
        {
            Id = default;
            SeasonId = default;
            SeasonEngineId = default;
        }
    }
}
