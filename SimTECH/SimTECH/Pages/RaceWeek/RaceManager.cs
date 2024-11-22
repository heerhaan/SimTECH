using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels.RaceWeek;

namespace SimTECH.Pages.RaceWeek;

public class RaceManager(Season season, League league, List<Incident> incidents, SimConfig config)
{
    private const int reliabilityMaxValue = 1000;
    private const int minimumPositionsGainedForSupport = 2;
    private const int percentageDefenseWhenMistake = 50;
    //private const int distanceMultiplier = 10;

    public void AddFormationLap(List<RaceDriver> raceDrivers)
    {
        foreach (var driver in raceDrivers)
        {
            var lapScore = new LapScore
            {
                ResultId = driver.ResultId,
                Order = 0,
                Score = GetQualifyingBonus(driver, raceDrivers.Count, season.GridBonus),
                TyreColour = driver.CurrentTyre.Colour,
            };

            driver.LapScores.Add(lapScore);
            driver.LastScore = lapScore.Score;
        }
    }

    private static int GetQualifyingBonus(RaceDriver driver, int racerCount, int bonus)
        => (racerCount * bonus) - ((driver.AbsoluteGrid - 1) * bonus);

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

    public void HandleDriverStrategy(RaceDriver driver, LapScore lapScore, List<Tyre> validTyres, bool isCaution)
    {
        var lifeLimitForPitstop = isCaution
            ? (driver.CurrentTyre.Pace / 2)
            : driver.CurrentTyre.PitWhenBelow;

        // Triggers a pitstop if condition is met
        if (validTyres.Count != 0 && lifeLimitForPitstop > driver.TyreLife)
        {
            ChangeTyres(driver, validTyres);

            lapScore.RacerEvents |= RacerEvent.Pitstop;
            lapScore.Score -= GetPitstopCost(isCaution);
        }

        lapScore.TyreColour = driver.CurrentTyre.Colour;

        // Following code applies tyre wear, so rule: first apply life then apply wear
        var tyreMinWear = driver.CurrentTyre.WearMin + driver.WearMinMod;
        var tyreMaxWear = driver.CurrentTyre.WearMax + driver.WearMaxMod;

        if (isCaution)
        {
            // TODO: Do a few calculations on this to make sure it works
            if (tyreMinWear > 0)
                tyreMinWear /= config.CautionTyreWearDivider;
            if (tyreMaxWear > 0)
                tyreMaxWear /= config.CautionTyreWearDivider;
        }

        // Ensures the minimum wear is always less than the maximum wear
        if (tyreMinWear > tyreMaxWear)
            tyreMaxWear = tyreMinWear + 1;

        // Adds wear to the tyre
        driver.TyreLife -= NumberHelper.RandomInt(tyreMinWear, tyreMaxWear);

        if (driver.CurrentTyre.MinimumLife > driver.TyreLife)// * distanceMultiplier
            driver.TyreLife = driver.CurrentTyre.MinimumLife;// * distanceMultiplier

        // Only add the effects of tyre wear to the score outside of SCs
        if (!isCaution)
            lapScore.Score += driver.TyreLife;
    }

    private static void ChangeTyres(RaceDriver driver, List<Tyre> validTyres)
    {
        // Tyres different from currently fitted
        var currentTyres = validTyres.Where(e => e.Id != driver.CurrentTyre.Id).ToList();

        Tyre nextTyre;
        if (currentTyres.Count > 1)
        {
            nextTyre = driver.StrategyPreference switch
            {
                StrategyPreference.None => currentTyres.TakeRandomItem(),
                StrategyPreference.Softer => currentTyres.OrderByDescending(e => e.Pace).First(),
                StrategyPreference.Harder => currentTyres.OrderBy(e => e.Pace).First(),
                _ => throw new ArgumentOutOfRangeException($"Unrecognized type: {driver.StrategyPreference}"),
            };
        }
        else if (currentTyres.Count == 1)
            nextTyre = currentTyres.First();
        else
            nextTyre = validTyres.First();

        driver.CurrentTyre = nextTyre;
        driver.TyreLife = nextTyre.Pace + driver.LifeBonus;// * distanceMultiplier
        driver.InstantOvertaken = true;
    }

    private int GetPitstopCost(bool isCaution)
    {
        var pitCost = NumberHelper.RandomInt(season.PitMinimum, season.PitMaximum);// * distanceMultiplier

        // Pitstop duration is reduced since a safety car is currently out
        if (isCaution && pitCost > season.PitCostSubtractCaution)
        {
            pitCost -= season.PitCostSubtractCaution;
        }

        return pitCost;
    }

    public void DeterminePositions(List<RaceDriver> raceDrivers)
    {
        var currentScoreOrderedPositions = GetCurrentPositions(raceDrivers);

        // NOTE: Wasn't fully convinced yet on this implementation, but it does work soooo...?
        var allPositionsAligned = false;
        while (!allPositionsAligned)
        {
            foreach (var driver in raceDrivers.Where(e => e.Status == RaceStatus.Racing).OrderBy(e => e.AbsolutePosition))
            {
                var lastScore = driver.LapScores.MaxBy(e => e.Order)
                    ?? throw new Exception("There's no singular highest order score found");

                int positionChange = driver.AbsolutePosition - currentScoreOrderedPositions[driver.SeasonDriverId];

                // Assign the new positions based on whether their overtakes have been succesful
                if (positionChange > 0)
                    HandlePositionGain(raceDrivers, driver, lastScore, positionChange);

                driver.LastScore = lastScore.Score;
            }

            // Need to re-retrieve this for every driver since their positions may change due to other overtakes (maybe)
            // ...altough i dont think this matters
            currentScoreOrderedPositions = GetCurrentPositions(raceDrivers);

            foreach (var driver in raceDrivers.Where(e => e.Status == RaceStatus.Racing).OrderBy(e => e.AbsolutePosition))
            {
                if (driver.AbsolutePosition != currentScoreOrderedPositions[driver.SeasonDriverId])
                    break;

                allPositionsAligned = true;
            }
        }

        SetPositions(raceDrivers);
    }

    // Returns the current position order based on the summed up laps of the drivers
    private static Dictionary<long, int> GetCurrentPositions(List<RaceDriver> raceDrivers)
    {
        var actualPositions = new Dictionary<long, int>();
        int absoluteIndex = 0;

        foreach (var driver in raceDrivers.OrderBy(e => (int)e.Status).ThenByDescending(e => e.LapSum))
            actualPositions.Add(driver.SeasonDriverId, ++absoluteIndex);

        return actualPositions;
    }

    // Sets the new positions of the drivers according to their current lap sums
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

    private void HandlePositionGain(List<RaceDriver> raceDrivers, RaceDriver driver, LapScore lastScore,
        int gainedPositions)
    {
        while (gainedPositions > 0)
        {
            var abovePosition = driver.AbsolutePosition - 1;
            if (abovePosition == 0)
                break;

            var defendingDriver = raceDrivers.First(e => e.AbsolutePosition == abovePosition);

            // Only perform a check if the defender is not instantly overtake, as this points that the defender has DNFed or pitted
            if (defendingDriver.InstantOvertaken == false)
            {
                // Checks whether team orders are applied to the attacking and defending driver
                if (UseTeamOrders(driver, defendingDriver, gainedPositions))
                {
                    var defenderLastScore = defendingDriver.LapScores.OrderByDescending(e => e.Order).First();

                    if (defendingDriver.Role == TeamRole.Main)
                    {
                        var scoreGapAttacker = driver.LapSum - defendingDriver.LapSum;
                        // BattleRng is used here to represent a gap between the two drivers
                        lastScore.Score -= scoreGapAttacker + league.BattleRng;

                        defenderLastScore.RacerEvents |= RacerEvent.Swap;
                        lastScore.RacerEvents |= RacerEvent.MaintainPosition;

                        break;
                    }
                    else
                    {
                        lastScore.RacerEvents |= RacerEvent.Swap;
                        defenderLastScore.RacerEvents |= RacerEvent.MaintainPosition;
                    }
                }
                // Only perform battles within the same race class
                else if (driver.ClassId == defendingDriver.ClassId)
                {
                    // Subtract attack value from defense, what's left is how much the attacker is hindered
                    var attackingResult = driver.Attack + NumberHelper.RandomInt(league.BattleRng * -1, league.BattleRng);
                    var defendingResult = defendingDriver.Defense + NumberHelper.RandomInt(league.BattleRng * -1, league.BattleRng);

                    // Defender recently made a mistake, so we're punishing him for it :)
                    if (defendingDriver.RecentMistake)
                        defendingResult = defendingResult.PercentageOfNumber(percentageDefenseWhenMistake);

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
            }

            // Overtake succeeded, driver gains a position!
            (driver.AbsolutePosition, defendingDriver.AbsolutePosition) = (defendingDriver.AbsolutePosition, driver.AbsolutePosition);

            gainedPositions--;
        }
    }

    // Team orders may be applied for the current position change, rules are:
    // Attacker is support driver and only has 1 position left to gain, thus it maintains position
    // Defender is support driver and won't make an attempt to defend against overtaker
    private static bool UseTeamOrders(RaceDriver attackingDriver, RaceDriver defendingDriver, int positionsGained)
    {
        if (attackingDriver.SeasonTeamId != defendingDriver.SeasonTeamId)
            return false;

        // Atacker is the main driver, defender should not hold up his teammate at all
        if (attackingDriver.Role == TeamRole.Main
            && defendingDriver.Role == TeamRole.Support)
        {
            return true;
        }

        // Call team orders on attacking driver only if the attacking support driver wouldn't be able to attack
        // any other driver besides his own (main/lead) teammate
        if (attackingDriver.Role == TeamRole.Support
            && defendingDriver.Role == TeamRole.Main
            && minimumPositionsGainedForSupport > positionsGained)
        {
            return true;
        }

        return false;
    }
}
