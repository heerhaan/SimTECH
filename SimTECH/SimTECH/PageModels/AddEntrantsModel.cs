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

        public long LeagueId { get; set; }
        public bool HasContracting { get; set; }
    }

    public static class EntrantModelExtensions
    {
        public static List<SeasonEngine> CombineEntrantsToRoot(this AddEntrantsModel model)
        {
            foreach (var engine in model.SeasonEngines)
            {
                engine.SeasonTeams = model.SeasonTeams.Where(e => e.BaseEngineId == engine.EngineId).ToList();

                foreach (var team in engine.SeasonTeams)
                {
                    team.SeasonDrivers = model.SeasonDrivers.Where(e => e.BaseTeamId == team.TeamId).ToList();
                }
            }

            return model.SeasonEngines.Select(e => e.Record).ToList();
        }

        public static void RemoveUnsetEngines(this AddEntrantsModel model)
        {
            foreach (var unset in model.SeasonEngines.Where(se => !model.BaseEngines.Select(e => e.Id).Contains(se.EngineId)))
            {
                model.SeasonEngines.Remove(unset);
            }
        }

        public static void RemoveUnsetTeams(this AddEntrantsModel model)
        {
            foreach (var unset in model.SeasonTeams.Where(st => !model.BaseTeams.Select(e => e.Id).Contains(st.TeamId)))
            {
                model.SeasonTeams.Remove(unset);
            }
        }

        public static void RemoveUnsetDrivers(this AddEntrantsModel model)
        {
            foreach (var unset in model.SeasonDrivers.Where(sd => !model.BaseDrivers.Select(e => e.Id).Contains(sd.DriverId)))
            {
                model.SeasonDrivers.Remove(unset);
            }
        }
    }
}
