using SimTECH.Data.Models;

namespace SimTECH.PageModels.SeasonModels;

public class OverviewModel
{
    public Season Season { get; set; }

    public List<Race> Races { get; set; }
    public List<SeasonDriver> SeasonDrivers { get; set; }
    public List<SeasonTeam> SeasonTeams { get; set; }
    public List<SeasonEngine> SeasonEngines { get; set; }

    public long ActiveClassId { get; set; }
}
