using SimTECH.Data.EditModels;
using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public sealed class AddEntrantsModel
    {
        // Entrant data properties
        public List<EditSeasonEngineModel> SeasonEngines { get; set; } = new();
        public List<EditSeasonTeamModel> SeasonTeams { get; set; } = new();
        public List<EditSeasonDriverModel> SeasonDrivers { get; set; } = new();

        // Supportive properties
        public List<Engine> BaseEngines { get; set; } = new();
        public List<Team> BaseTeams { get; set; } = new();
        public List<Driver> BaseDrivers { get; set; } = new();

        public void CombineEntrantModels()
        {
            foreach (var engine in SeasonEngines)
            {
                engine.SeasonTeams = SeasonTeams.Where(e => e.BaseEngineId == engine.EngineId).ToList();

                foreach (var team in engine.SeasonTeams)
                {
                    team.SeasonDrivers = SeasonDrivers.Where(e => e.BaseTeamId == team.TeamId).ToList();
                }
            }
        }
    }
}
