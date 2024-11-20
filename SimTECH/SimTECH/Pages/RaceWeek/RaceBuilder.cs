using SimTECH.Data.Models;
using SimTECH.PageModels.RaceWeek;

namespace SimTECH.Pages.RaceWeek;

// TODO: Exploratory and temp for now, need to think critically if this is what we need
// NOTE: Builders and such generally should not be built as a goal but as a tool, this underneath reads like a goal...
public static class RaceBuilder
{
}

public class RaceDriverBuilder
{
    private RaweCeekDriver _driver = new();

    public RaceDriverBuilder()
    {
        Reset();
    }

    public void Reset()
    {
        _driver = new();
    }

    public void BuildDriverInfo(SeasonDriver seasonDriver)
    {
        if (seasonDriver.Driver == null)
            throw new Exception("Driver in seasonDriver is null!!!");

        _driver.SeasonDriverId = seasonDriver.Id;
        _driver.FirstName = seasonDriver.Driver.FirstName;
        // etc...
    }

    // BuildTeamData

    // BuildManufacturerData

    // BuildResultData

    // BuildPenaltyData

    // BuildTraits

    // BuildEngine

    // BuildTyre

    // BuildIncidents

    // BuildRacePerformanceData

    public RaweCeekDriver GetDriver()
    {
        var driver = _driver;

        Reset();

        return driver;
    }
}
