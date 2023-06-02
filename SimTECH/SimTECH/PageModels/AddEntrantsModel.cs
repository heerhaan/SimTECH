using SimTECH.Data.EditModels;
using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    // Clean this one up please, its so ugly
    public sealed class AddEntrantsModel
    {
        // Entrant data properties
        public List<EditSeasonEngineModel> SeasonEngines { get; set; } = new();
        public List<EditSeasonTeamModel> SeasonTeams { get; set; } = new();
        public List<EditSeasonDriverModel> SeasonDrivers { get; set; } = new();

        // Supportive properties
        public HashSet<Engine> BaseEngines { get; set; } = new();
        public HashSet<Team> BaseTeams { get; set; } = new();
        public HashSet<Driver> BaseDrivers { get; set; } = new();

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

    public static class EntrantModelExtensions
    {
        public static void RemoveUnsetEngines(this AddEntrantsModel model)
        {
            var unsetEngines = model.SeasonEngines.Where(se => !model.BaseEngines.Select(e => e.Id).Contains(se.EngineId));
            foreach (var unset in unsetEngines)
            {
                model.SeasonEngines.Remove(unset);
            }
        }

        public static void RemoveUnsetTeams(this AddEntrantsModel model)
        {
            var unsetTeams = model.SeasonTeams.Where(st => !model.BaseTeams.Select(e => e.Id).Contains(st.TeamId));
            foreach (var unset in unsetTeams)
            {
                model.SeasonTeams.Remove(unset);
            }
        }

        public static void RemoveUnsetDrivers(this AddEntrantsModel model)
        {
            var unsetDrivers = model.SeasonDrivers.Where(sd => !model.BaseDrivers.Select(e => e.Id).Contains(sd.DriverId));
            foreach (var unset in unsetDrivers)
            {
                model.SeasonDrivers.Remove(unset);
            }
        }
    }
}
