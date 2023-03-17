using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels
{
    public class AddEntrantsModel
    {
        // Entrant data properties
        public List<EditSeasonEngineModel> SeasonEngines { get; set; } = new();
        public List<EditSeasonTeamModel> SeasonTeams { get; set; } = new();
        public List<EditSeasonDriverModel> SeasonDrivers { get; set; } = new();

        // Supportive properties
        public List<Engine> BaseEngines { get; set; } = new();
        public List<Team> BaseTeams { get; set; } = new();
        public List<Driver> BaseDrivers { get; set; } = new();
    }
}
