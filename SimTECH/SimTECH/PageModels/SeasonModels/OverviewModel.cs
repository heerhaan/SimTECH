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

    public List<SeasonDriver> ClassDrivers()
    {
        if (ActiveClassId == default)
            return SeasonDrivers;

        var teamsWithClass = SeasonTeams
            .Where(e => e.ClassId == ActiveClassId)
            .Select(e => e.Id)
            .ToList();

        return SeasonDrivers
            .Where(e => e.SeasonTeamId.HasValue && teamsWithClass.Contains(e.SeasonTeamId.Value))
            .ToList();
    }

    public List<SeasonTeam> ClassTeams()
    {
        if (ActiveClassId == default)
            return SeasonTeams;

        return SeasonTeams
            .Where(e => e.ClassId == ActiveClassId)
            .ToList();
    }
}
