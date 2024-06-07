using Microsoft.AspNetCore.Identity;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels.RaceWeek;

namespace SimTECH.Pages.RaceWeek;

public class RaceManager(Season season, League league, List<Incident> incidents, SimConfig config)
{
    private const int reliabilityMaxValue = 1000;

    public void AddFormationLap(List<RaceDriver> raceDrivers)
    {
        var racerCount = raceDrivers.Count;

        foreach (var driver in raceDrivers)
        {
            var lapScore = new LapScore
            {
                ResultId = driver.ResultId,
                Order = 0,
                Score = driver.QualifyingBonus(racerCount, season.GridBonus),
                TyreColour = driver.CurrentTyre.Colour,
            };

            driver.LapScores.Add(lapScore);
            driver.LastScore = lapScore.Score;
        }
    }

    public bool CheckReliability(RaceDriver driver, LapScore lapScore,
        Entrant activeCheck, bool isFirstLap)
    {
        var isSafetyCarComingOut = false;

        if (activeCheck == Entrant.Driver && DidReliabilityFail(driver.DriverReliability))
        {
            lapScore.RacerEvents |= RacerEvent.DriverDnf;
            driver.Incident = incidents.TakeRandomIncident(IncidentCategory.Driver);
        }
        else if (activeCheck == Entrant.Team && DidReliabilityFail(driver.CarReliability))
        {
            lapScore.RacerEvents |= RacerEvent.CarDnf;
            driver.Incident = incidents.TakeRandomIncident(IncidentCategory.Car);
        }
        else if (activeCheck == Entrant.Engine && DidReliabilityFail(driver.EngineReliability))
        {
            lapScore.RacerEvents |= RacerEvent.EngineDnf;
            driver.Incident = incidents.TakeRandomIncident(IncidentCategory.Engine);
        }
        // Additional driver reliability check happens on the opening lap
        else if (isFirstLap && DidReliabilityFail(driver.DriverReliability))
        {
            lapScore.RacerEvents |= RacerEvent.DriverDnf;
            driver.Incident = incidents.TakeRandomIncident(IncidentCategory.Driver);
        }
        else
        {
            return isSafetyCarComingOut;
        }

        // Relability failure = instant overtake by attacking drivers
        driver.InstantOvertaken = true;

        // If enabled, then we're also going to check if anyone experienced a fatal crash
        if (league.Options.HasFlag(LeagueOptions.EnableFatality) && NumberHelper.RandomInt(league.FatalityOdds) == 0)
        {
            isSafetyCarComingOut = true;

            driver.Status = RaceStatus.Fatal;
            driver.Incident = incidents.TakeRandomIncident(IncidentCategory.Lethal);
            lapScore.RacerEvents = RacerEvent.Death;

            return isSafetyCarComingOut;
        }

        // Randomly determines the odds a safety car occured due to the DNF'ing driver
        isSafetyCarComingOut = NumberHelper.RandomInt(league.SafetyCarOdds) == 0;
        driver.Status = RaceStatus.Dnf;

        return isSafetyCarComingOut;
    }

    // Check if driver made a mistake, if so then it's going to cost him
    public void CheckIfDriverMadeMistake(RaceDriver driver, LapScore lapScore)
    {
        for (int j = 0; j < season.MistakeRolls; j++)
        {
            if (DidReliabilityFail(driver.DriverReliability))
            {
                lapScore.Score -= NumberHelper.RandomInt(season.MistakeMinimum, season.MistakeMaximum);
                lapScore.RacerEvents |= RacerEvent.Mistake;
                driver.RecentMistake = true;
                break;
            }
        }
    }

    public bool DidReliabilityFail(int reliability) => NumberHelper.RandomInt(reliabilityMaxValue) > reliability;

    public void HandleDriverStrategy(RaceDriver driver, LapScore lapScore, List<Tyre> validTyres)
    {
        // Triggers a pitstop if condition is met
        if (validTyres.Count != 0 && driver.CurrentTyre.PitWhenBelow > driver.TyreLife)
        {
            TriggerPitStop(driver, lapScore, validTyres);
        }

        lapScore.Score += driver.TyreLife;

        // Following code applies tyre wear, so rule: first apply life then apply wear
        var tyreMinWear = driver.CurrentTyre.WearMin + driver.WearMinMod;
        var tyreMaxWear = driver.CurrentTyre.WearMax + driver.WearMaxMod;

        // Ensures the minimum wear is always less than the maximum wear
        if (tyreMinWear > tyreMaxWear)
        {
            tyreMaxWear = tyreMinWear + 1;
        }

        // Adds wear to the tyre
        driver.TyreLife -= NumberHelper.RandomInt(tyreMinWear, tyreMaxWear);

        if (driver.TyreLife < driver.CurrentTyre.MinimumLife)
            driver.TyreLife = driver.CurrentTyre.MinimumLife;
    }

    private static void TriggerPitStop(RaceDriver driver, LapScore lapScore, List<Tyre> validTyres)
    {
        // Tyres different from currently fitted
        var currentTyres = validTyres.Where(e => e.Id != driver.CurrentTyre.Id).ToList();

        Tyre nextTyre;
        if (currentTyres.Count > 1)
            nextTyre = currentTyres.TakeRandomItem();
        else if (currentTyres.Count == 1)
            nextTyre = currentTyres.First();
        else
            nextTyre = validTyres.First();

        driver.CurrentTyre = nextTyre;
        driver.TyreLife = nextTyre.Pace + driver.LifeBonus;
        driver.InstantOvertaken = true;

        lapScore.RacerEvents |= RacerEvent.Pitstop;
    }

    public void DeterminePositions(List<RaceDriver> raceDrivers)
    {
        var allPositionsAligned = false;
        // Need to re-retrieve this for every driver since their positions may change due to over overtakes (maybe) / altough i dont think this matters
        var actualPositions = GetCurrentPositions(raceDrivers);

        // NOTE: Wasn't fully convinced yet on this implementation, but it does work soooo...?
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

    public void SetPositions(List<RaceDriver> raceDrivers)
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
                : "+" + Math.Round((scoreAboveDriver - driver.LapSum) * config.GapMarge, 2).ToString("F2");

            scoreAboveDriver = driver.LapSum;
        }
    }

    private void HandlePositionGain(List<RaceDriver> raceDrivers, RaceDriver driver, LapScore lastScore, int gainedPositions)
    {
        var battleRng = league.BattleRng;

        while (gainedPositions > 0)
        {
            var abovePosition = driver.AbsolutePosition - 1;
            if (abovePosition == 0)
                break;

            var defendingDriver = raceDrivers.First(e => e.AbsolutePosition == abovePosition);

            // Team orders may be applied for the current position change, rules are:
            // Attacker is support driver and only has 1 position left to gain, thus it maintains position
            // Defender is support driver and won't make an attempt to defend against overtaker
            if (UseTeamOrders(driver, defendingDriver, gainedPositions))
            {
                if (defendingDriver.Role == TeamRole.Main)
                {
                    var scoreGapAttacker = driver.LapSum - defendingDriver.LapSum;
                    // BattleRng is used here to represent a gap between the two drivers
                    lastScore.Score -= (scoreGapAttacker + battleRng);
                    lastScore.RacerEvents |= RacerEvent.MaintainPosition;

                    break;
                }
                else
                {
                    defendingDriver.LapScores.Last().RacerEvents |= RacerEvent.Swap;
                }
            }
            else if (defendingDriver.InstantOvertaken == false && driver.ClassId == defendingDriver.ClassId)
            {
                // Subtract attack value from defense, what's left is how much the attacker is hindered
                var attackingResult = driver.Attack + NumberHelper.RandomInt(battleRng * -1, battleRng);
                var defendingResult = defendingDriver.Defense + NumberHelper.RandomInt(battleRng * -1, battleRng);

                // Defender recently made a mistake, so we're punishing him for it :)
                if (defendingDriver.RecentMistake)
                    defendingResult /= 2;

                var battleCost = defendingResult - attackingResult;

                if (battleCost > 0)
                    lastScore.Score -= battleCost;

                // Attacker has failed to overtake because the defender still has a higher lap sum
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

    private static bool UseTeamOrders(RaceDriver attackingDriver, RaceDriver defendingDriver, int positionsGained)
    {
        if (attackingDriver.SeasonTeamId != defendingDriver.SeasonTeamId)
            return false;

        if (attackingDriver.Role == TeamRole.Main
            && defendingDriver.Role == TeamRole.Support)
        {
            return true;
        }

        // Only stop overtaking if the attacker wouldn't be overtaking any other drivers anyway
        if (attackingDriver.Role == TeamRole.Support
            && defendingDriver.Role == TeamRole.Main
            && positionsGained == 1)
        {
            return true;
        }

        return false;
    }
}
