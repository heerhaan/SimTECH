using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels.RaceWeek;

namespace SimTECH.Managers;

public class RaceManager
{
    private readonly Season _season;
    private readonly League _league;
    private readonly List<Incident> _incidents;

    public RaceManager(Season season, League league, List<Incident> incidents)
    {
        _season = season;
        _league = league;
        _incidents = incidents;
    }

    /// <summary>
    /// (Changing) data used in the view/component/race (necessary to be updated per advance?):
    /// RaceState
    /// Current RaceOccurrence
    /// RacedLaps
    /// (When dynamic is implemented) Climate
    /// RaceDrivers
    /// All RaceOccurrences
    /// ValidTyres
    /// RelCheck
    /// 
    /// Static values used (could simply be determined onInit):
    /// calcDistance
    /// calcCount
    /// RaceTrackLength
    /// TotalLaps
    /// RaceTrackCountry
    /// RaceName
    /// RaceRound
    /// (If not dynamic yet) Climate
    /// SeasonHasRaceClasses
    /// lastLowest - lastHighest
    /// signalEvents
    /// </summary>
    /// <returns></returns>

    // Eventueel kan ik ook enkel de supportieve methods hierin zetten zoals CheckRel en HandlePosGain

    // Wrong way to think? Rather pass static shizzle in constructor and pass individual racedrivers
    //public List<RaceDriver> GetOrderedRaceDrivers()
    //{
    //    return raceDrivers
    //        .OrderBy(e => (int)e.Status).ThenBy(e => e.AbsolutePosition)
    //        .ToList();
    //}

    public bool CheckReliability(RaceDriver driver, LapScore lapScore, Entrant activeCheck, SituationOccurrence currentSituation, bool isFirstLap)
    {
        var safetyCar = false;

        if (activeCheck == Entrant.Driver && DidReliabilityFail(driver.DriverReliability))
        {
            lapScore.RacerEvents |= RacerEvent.DriverDnf;
            driver.Incident = _incidents.Where(e => e.Category == IncidentCategory.Driver).ToList().TakeRandomIncident();
        }
        else if (activeCheck == Entrant.Team && DidReliabilityFail(driver.CarReliability))
        {
            lapScore.RacerEvents |= RacerEvent.CarDnf;
            driver.Incident = _incidents.Where(e => e.Category == IncidentCategory.Car).ToList().TakeRandomIncident();
        }
        else if (activeCheck == Entrant.Engine && DidReliabilityFail(driver.EngineReliability))
        {
            lapScore.RacerEvents |= RacerEvent.EngineDnf;
            driver.Incident = _incidents.Where(e => e.Category == IncidentCategory.Engine).ToList().TakeRandomIncident();
        }
        // Additional reliability check happens on the opening lap, as crashes are more frequent then
        else if (isFirstLap && DidReliabilityFail(driver.DriverReliability))
        {
            lapScore.RacerEvents |= RacerEvent.DriverDnf;
            driver.Incident = _incidents.Where(e => e.Category == IncidentCategory.Driver).ToList().TakeRandomIncident();
        }
        else
        {
            return safetyCar;
        }

        // Relability failure = instant overtake by attacking drivers
        driver.InstantOvertaken = true;

        // If enabled, then we're also going to check if anyone experienced a fatal crash
        if (_league.Options.HasFlag(LeagueOptions.EnableFatality) && NumberHelper.RandomInt(_league.FatalityOdds) == 0)
        {
            safetyCar = true;

            driver.Status = RaceStatus.Fatal;
            driver.Incident = _incidents.Where(e => e.Category == IncidentCategory.Lethal).ToList().TakeRandomIncident();
            lapScore.RacerEvents = RacerEvent.Death;

            currentSituation = SituationOccurrence.Halted;

            return safetyCar;
        }

        // Randomly determines the odds a safety car occured due to the DNF'ing driver
        safetyCar = NumberHelper.RandomInt(_league.SafetyCarOdds) == 0;
        driver.Status = RaceStatus.Dnf;

        return safetyCar;
    }

    private static bool DidReliabilityFail(int reliability) => NumberHelper.RandomInt(1000) > reliability;
}
