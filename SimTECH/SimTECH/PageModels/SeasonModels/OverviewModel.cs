using SimTECH.Data.Models;

namespace SimTECH.PageModels.SeasonModels;

public class OverviewModel
{
    // Consider using this as a generic cascading model class for the overview and it's components
    // Also yeah
    public List<SeasonEngine> SeasonEngines { get; set; }
    public List<SeasonTeam> SeasonTeams { get; set; }
    public List<SeasonDriver> SeasonDrivers { get; set; }
}
