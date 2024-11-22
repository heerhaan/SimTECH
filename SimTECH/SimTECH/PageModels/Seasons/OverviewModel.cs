using SimTECH.Data.Models;

namespace SimTECH.PageModels.Seasons;

public class OverviewModel
{
    public Season Season { get; set; }

    public List<Race> Races { get; set; }
    public List<SeasonDriver> SeasonDrivers { get; set; }
    public List<SeasonTeam> SeasonTeams { get; set; }
    //public List<SeasonEngine> SeasonEngines { get; set; }
    public List<Result> Results { get; set; }
    public List<Manufacturer> Manufacturers { get; set; }

    public long ActiveClassId { get; set; }

    private List<long> TeamIdsOfClass => SeasonTeams.Where(e => e.ClassId == ActiveClassId).Select(e => e.Id).ToList();

    public List<SeasonDriver> ClassDrivers
    {
        get
        {
            if (ActiveClassId == default)
                return SeasonDrivers;

            return SeasonDrivers
                .Where(e => e.SeasonTeamId.HasValue && TeamIdsOfClass.Contains(e.SeasonTeamId.Value))
                .ToList();
        }
    }

    public List<SeasonTeam> ClassTeams
    {
        get
        {
            if (ActiveClassId == default)
                return SeasonTeams;

            return SeasonTeams.Where(e => e.ClassId == ActiveClassId).ToList();
        }
    }

    public List<Result> ClassResults
    {
        get
        {
            if (ActiveClassId == default)
                return Results;

            return Results.Where(e => TeamIdsOfClass.Contains(e.SeasonTeamId)).ToList();
        }
    }
}
