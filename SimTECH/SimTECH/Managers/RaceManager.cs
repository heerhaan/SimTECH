using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels.RaceWeek;

namespace SimTECH.Managers;

public class RaceManager
{
    private const int reliabilityMaxValue = 1000;

    private readonly Season _season;
    private readonly League _league;
    private readonly List<Incident> _incidents;
    private readonly SimConfig _config;

    public RaceManager(Season season, League league, List<Incident> incidents, SimConfig config)
    {
        _season = season;
        _league = league;
        _incidents = incidents;
        _config = config;
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

    public void AddFormationLap(List<RaceDriver> raceDrivers)
    {
        var racerCount = raceDrivers.Count;

        foreach (var driver in raceDrivers)
        {
            var lapScore = new LapScore
            {
                ResultId = driver.ResultId,
                Order = 0,
                Score = driver.QualifyingBonus(racerCount, _season.GridBonus),
                TyreColour = driver.CurrentTyre.Colour,
            };

            driver.LapScores.Add(lapScore);
            driver.LastScore = lapScore.Score;
        }
    }

    public bool CheckReliability(RaceDriver driver, LapScore lapScore,
        Entrant activeCheck, SituationOccurrence currentSituation, bool isFirstLap)
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

    private static bool DidReliabilityFail(int reliability) => NumberHelper.RandomInt(reliabilityMaxValue) > reliability;

    public void DeterminePositions(List<RaceDriver> raceDrivers)
    {
        var allPositionsAligned = false;
        // Need to re-retrieve this for every driver since their positions may change due to over overtakes (maybe) / altough i dont think this matters
        var actualPositions = GetCurrentPositions(raceDrivers);

        // TODO: This likely can be optimized further
        while (!allPositionsAligned)
        {
            foreach (var driver in raceDrivers.Where(e => e.Status == RaceStatus.Racing).OrderBy(e => e.AbsolutePosition))
            {
                var lastScore = driver.LapScores.Last();

                int positionChange = driver.AbsolutePosition - actualPositions[driver.SeasonDriverId];

                // Assign the new positions based on whether their overtakes have been succesful
                if (positionChange > 0)
                    HandlePositionGain(raceDrivers, driver, lastScore, positionChange);

                driver.LastScore = lastScore.Score;
            }

            allPositionsAligned = true;
            actualPositions = GetCurrentPositions(raceDrivers);

            foreach (var driver in raceDrivers.Where(e => e.Status == RaceStatus.Racing).OrderBy(e => e.AbsolutePosition))
            {
                if (driver.AbsolutePosition != actualPositions[driver.SeasonDriverId])
                    allPositionsAligned = false;
            }
        }

        SetPositions(raceDrivers);
    }

    private static Dictionary<long, int> GetCurrentPositions(List<RaceDriver> raceDrivers)
    {
        var actualPositions = new Dictionary<long, int>();
        int absoluteIndex = 0;

        foreach (var driver in raceDrivers.OrderBy(e => (int)e.Status).ThenByDescending(e => e.LapSum))
            actualPositions.Add(driver.SeasonDriverId, ++absoluteIndex);

        return actualPositions;
    }

    private void SetPositions(List<RaceDriver> raceDrivers)
    {
        int absoluteIndex = 0;
        int scoreAboveDriver = 0;

        var positionIndexDict = raceDrivers.Select(e => e.ClassId).Distinct().ToDictionary(e => e, _ => 0);

        foreach (var driver in raceDrivers.OrderBy(e => (int)e.Status).ThenByDescending(e => e.LapSum))
        {
            driver.Position = ++positionIndexDict[driver.ClassId];
            driver.AbsolutePosition = ++absoluteIndex;

            driver.GapAbove = driver.AbsolutePosition == 1
                ? "LEADER"
                : "+" + (Math.Round((scoreAboveDriver - driver.LapSum) * _config.GapMarge, 2)).ToString("F2");

            scoreAboveDriver = driver.LapSum;
        }
    }

    private void HandlePositionGain(List<RaceDriver> raceDrivers, RaceDriver driver, LapScore lastScore, int gainedPositions)
    {
        var battleRng = _league.BattleRng;

        while (gainedPositions > 0)
        {
            var abovePosition = driver.AbsolutePosition - 1;
            if (abovePosition == 0)
                break;

            var defendingDriver = raceDrivers.First(e => e.AbsolutePosition == abovePosition);

            // Driver above is teammate AND support driver AND attacker is main driver, swap time!
            //if (defendingDriver.SeasonTeamId == driver.SeasonTeamId
            //    && driver.Role == TeamRole.Main
            //    && defendingDriver.Role == TeamRole.Support)
            //{
            //    lastScore.RacerEvents |= RacerEvent.Swap;
            //    defendingDriver.LapScores.Last().RacerEvents |= RacerEvent.Swap;
            //}
            //else

            if (defendingDriver.InstantOvertaken == false && driver.ClassId == defendingDriver.ClassId)
            {
                // Subtract attack value from defense, what's left is how much the attacker is hindered
                var attackingResult = driver.Attack + NumberHelper.RandomInt((battleRng * -1), battleRng);
                var defendingResult = defendingDriver.Defense + NumberHelper.RandomInt((battleRng * -1), battleRng);

                // Defender frequently made a mistake, so we're punishing him for it :)
                if (defendingDriver.RecentMistake)
                    defendingResult /= 2;

                var battleCost = defendingResult - attackingResult;

                if (battleCost > 0)
                    lastScore.Score -= battleCost;

                // Attacking driver has failed to overtake, the defender still has a higher lap sum
                if (defendingDriver.LapSum > driver.LapSum)
                {
                    defendingDriver.DefensiveCount++;
                    break;
                }

                // It only counts as an overtake if it wasn't in an instant
                driver.OvertakeCount++;
            }

            // Overtake succeeded, driver gains a position!
            (driver.AbsolutePosition, defendingDriver.AbsolutePosition) = (defendingDriver.AbsolutePosition, driver.AbsolutePosition);

            gainedPositions--;
        }
    }
}
