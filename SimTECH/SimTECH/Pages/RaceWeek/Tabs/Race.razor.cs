﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using SimTECH.Common.Enums;
using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels.RaceWeek;
using SimTECH.Pages.RaceWeek.Dialogs;

namespace SimTECH.Pages.RaceWeek.Tabs;

public partial class Race
{
    [CascadingParameter]
    public RaweCeekModel Model { get; set; }

    [Parameter]
    public List<LapScore> LapScores { get; set; } = new();

    [Parameter]
    public List<RaceOccurrence> Occurrences { get; set; } = new();

    [Parameter]
    public IEnumerable<Tyre> Tyres { get; set; } = Enumerable.Empty<Tyre>();

    [Parameter]
    public SimConfig Config { get; set; } = new();

    [Parameter]
    public EventCallback OnFinish { get; set; } = new();

    private static readonly Entrant[] cycleableReliablities = [Entrant.Driver, Entrant.Team, Entrant.Engine];
    private static readonly RacerEvent[] signalEvents =
        [
            RacerEvent.Pitstop,
            RacerEvent.DriverDnf,
            RacerEvent.CarDnf,
            RacerEvent.EngineDnf,
            RacerEvent.Mistake,
            RacerEvent.Death,
            RacerEvent.Swap,
            //RacerEvent.FastestLap,
        ];

    private Dictionary<int, SituationOccurrence> AdvanceOccurrences { get; set; } = new();
    private List<RaceDriver> RaceDrivers { get; set; } = new();
    private List<Incident> Incidents { get; set; } = new();
    private List<Tyre> AvailableTyres { get; set; } = new();
    private List<Tyre> ValidTyres { get; set; } = new();

    // Controls for the view
    private bool loading = true;
    private bool safetyCarOut = false;

    private SituationOccurrence CurrentSituation { get; set; } = SituationOccurrence.Raced;

    private Entrant ActiveReliabilityCheck { get; set; } = Entrant.Driver;
    private int reliablityCycler = 0;

    // Supportive caluclation fields
    private int fastestLap;
    private int racedLaps;
    private int totalLaps;
    private int racerCount;

    private int calculated;
    private int calculationCount;
    private int calculationsPerAdvance = 5;
    private int calculationDistance = 10;

    private int lastHighestScore = 0;
    private int lastLowestScore = 0;
    private int safetyWearDivider = 3;

    private bool isFirstLap => calculated == 1;

    protected override async Task OnInitializedAsync()
    {
        Incidents = await _incidentService.GetIncidents(StateFilter.Active);

        AvailableTyres.AddRange(
            Tyres.Where(e => e.State == State.Active
                && (e.LeagueTyres?.Any(e => e.LeagueId == Model.Season.LeagueId) ?? false)));

        calculationDistance = Config.CalculationDistance;
        calculationCount = Model.Race.RaceLength / calculationDistance;

        if (LapScores.Count != 0)
            calculated = LapScores.Select(e => e.Order).Max();

        for (int index = 1; index <= calculationCount; index++)
        {
            var situationMatch = Occurrences.FirstOrDefault(e => e.Order == index);

            AdvanceOccurrences.Add(index, situationMatch?.Occurrences ?? SituationOccurrence.Unknown);
        }

        racedLaps = GetCurrentLapCount;
        totalLaps = NumberHelper.LapCount(Model.Race.RaceLength, Model.Race.Track.Length);

        BuildRaceDrivers();

        // Run this one time so that we have valid tyres to show
        var distanceLeft = Model.Race.RaceLength - (calculated * calculationDistance);

        ValidTyres = AvailableTyres.FindValidTyres(distanceLeft, Model.Climate.IsWet).ToList();

        loading = false;
    }

    private void BuildRaceDrivers()
    {
        RaceDrivers = Model.RaweCeekDrivers
            .OrderBy(e => e.AbsolutePosition)
            .Take(Model.Season.MaximumDriversInRace)
            .Select(e => e.MapToRaceDriver())
            .ToList();

        racerCount = RaceDrivers.Count;

        // Refactor this if persisting happens per advance
        if (Model.Race.State == State.Closed)
        {
            foreach (var driver in RaceDrivers)
            {
                driver.LapScores = LapScores.Where(e => e.ResultId == driver.ResultId).ToList();

                driver.LastScore = driver.LapScores.MaxBy(e => e.Order)?.Score ?? 0;

                if (driver.LastScore > lastHighestScore)
                    lastHighestScore = driver.LastScore;
                else if (driver.LastScore < lastLowestScore)
                    lastLowestScore = driver.LastScore;
            }
        }
        else if (calculated == 0)
        {
            AddFormationLap();
        }

        SetPositions();
    }

    private void AddFormationLap()
    {
        foreach (var driver in RaceDrivers)
        {
            var lapScore = new LapScore
            {
                ResultId = driver.ResultId,
                Order = 0,
                Score = driver.QualifyingBonus(racerCount, Model.Season.GridBonus),
                TyreColour = driver.CurrentTyre.Colour,
            };

            driver.LapScores.Add(lapScore);
            driver.LastScore = lapScore.Score;
        }
    }

    private async Task Advance()
    {
        // Use this variable to persist the lap scores which were generated in this advance
        var lapScoresToPersist = new List<LapScore>();

        // Every i in this instance is relative to 10km (or calculationDistance) of racing aka a calculation
        for (int i = 0; i < calculationsPerAdvance; i++)
        {
            // Prevent continuing the advancing if we have already reached the limit
            if (calculated >= calculationCount)
                break;

            ++calculated;

            AddCalculationSituation();

            // Logic for what happens during a SC goes here
            if (safetyCarOut)
            {
                var safetyCarGoesBackIn = HandleSafetyMoment(lapScoresToPersist);
                if (safetyCarGoesBackIn)
                {
                    // After handling a safety car round, it goes back in
                    safetyCarOut = false;

                    CurrentSituation = SituationOccurrence.Raced;
                }

                continue;
            }

            foreach (var driver in RaceDrivers.Where(e => e.Status == RaceStatus.Racing))
            {
                var lapScore = HandleDriverAdvancement(driver);

                driver.LapScores.Add(lapScore);
                lapScoresToPersist.Add(lapScore);
            }

            DeterminePositions();

            PostProcessAdvance();

            // Stop iterating through all advances since SC has gone out
            if (safetyCarOut)
                break;
        }

        racedLaps = GetCurrentLapCount;

        var lastScores = RaceDrivers.Where(e => e.Status == RaceStatus.Racing).Select(e => e.LastScore).ToArray();

        if (lastScores.Any())
        {
            lastLowestScore = lastScores.Min();
            lastHighestScore = lastScores.Max();
        }

        if (calculated >= calculationCount)
        {
            await Finish();
        }

        // Persist the new stint results, yes after doing all whats needed to show them
        //PersistLapScores(lapScoresToPersist);
    }

    private LapScore HandleDriverAdvancement(RaceDriver driver)
    {
        driver.SingleOccurrence = null;
        driver.InstantOvertaken = false;
        driver.RecentMistake = false;

        var lapScore = new LapScore { ResultId = driver.ResultId, Order = calculated };

        // Determine if either driver, car or engine has failed
        var safetyCarOccurrence = CheckReliability(driver, lapScore);

        // Calculate the score for drivers which are still racing
        if (driver.Status == RaceStatus.Racing)
        {
            var minRng = (Model.Season.RngMinimum + driver.RngMinMod);
            var maxRng = (Model.Season.RngMaximum + driver.RngMaxMod);

            int lapValue = NumberHelper.RandomInt(minRng, maxRng);

            // Check if driver made a mistake, if so then it's going to cost him
            for (int j = 0; j < Model.Season.MistakeRolls; j++)
            {
                if (DidReliabilityFail(driver.DriverReliability))
                {
                    lapValue -= NumberHelper.RandomInt(Model.Season.MistakeMinimum, Model.Season.MistakeMaximum);
                    lapScore.RacerEvents |= RacerEvent.Mistake;
                    driver.RecentMistake = true;
                    break;
                }
            }

            // Strategy
            lapValue += HandleStrategy(driver, lapScore);

            // Adds the overall power of the driver
            lapValue += driver.Power;

            if (lapValue > fastestLap)
            {
                fastestLap = lapValue;

                foreach (var raceDriver in RaceDrivers)
                    raceDriver.HasFastestLap = false;

                driver.HasFastestLap = true;
                lapScore.RacerEvents |= RacerEvent.FastestLap;
            }

            // Add time cost of the pitstop
            if (lapScore.RacerEvents.HasFlag(RacerEvent.Pitstop))
                lapValue -= GetPitstopCost();

            // Finally add the score to the lap results
            lapScore.Score = lapValue;
        }
        // If this get's triggered then the current driver caused a safety car, racing goes on as normal until the next advance
        else if (safetyCarOccurrence)
        {
            safetyCarOut = safetyCarOccurrence;
            CurrentSituation = SituationOccurrence.Caution;
        }

        lapScore.TyreColour = driver.CurrentTyre.Colour;

        return lapScore;
    }

    private bool CheckReliability(RaceDriver driver, LapScore lapScore)
    {
        var safetyCar = false;

        if (ActiveReliabilityCheck == Entrant.Driver && DidReliabilityFail(driver.DriverReliability))
        {
            lapScore.RacerEvents |= RacerEvent.DriverDnf;
            driver.Incident = Incidents.Where(e => e.Category == IncidentCategory.Driver).ToList().TakeRandomIncident();
        }
        else if (ActiveReliabilityCheck == Entrant.Team && DidReliabilityFail(driver.CarReliability))
        {
            lapScore.RacerEvents |= RacerEvent.CarDnf;
            driver.Incident = Incidents.Where(e => e.Category == IncidentCategory.Car).ToList().TakeRandomIncident();
        }
        else if (ActiveReliabilityCheck == Entrant.Engine && DidReliabilityFail(driver.EngineReliability))
        {
            lapScore.RacerEvents |= RacerEvent.EngineDnf;
            driver.Incident = Incidents.Where(e => e.Category == IncidentCategory.Engine).ToList().TakeRandomIncident();
        }
        // Additional reliability check happens on the opening lap, as crashes are more frequent then
        else if (isFirstLap && DidReliabilityFail(driver.DriverReliability))
        {
            lapScore.RacerEvents |= RacerEvent.DriverDnf;
            driver.Incident = Incidents.Where(e => e.Category == IncidentCategory.Driver).ToList().TakeRandomIncident();
        }
        else
        {
            return safetyCar;
        }

        // Relability failure = instant overtake by attacking drivers
        driver.InstantOvertaken = true;

        // If enabled, then we're also going to check if anyone experienced a fatal crash
        if (Model.League.Options.HasFlag(LeagueOptions.EnableFatality) && NumberHelper.RandomInt(Model.League.FatalityOdds) == 0)
        {
            safetyCar = true;

            driver.Status = RaceStatus.Fatal;
            driver.Incident = Incidents.Where(e => e.Category == IncidentCategory.Lethal).ToList().TakeRandomIncident();
            lapScore.RacerEvents = RacerEvent.Death;

            CurrentSituation = SituationOccurrence.Halted;

            return safetyCar;
        }

        // Randomly determines the odds a safety car occured due to the DNF'ing driver
        safetyCar = NumberHelper.RandomInt(Model.League.SafetyCarOdds) == 0;
        driver.Status = RaceStatus.Dnf;

        return safetyCar;
    }

    // Returns a number which will be added to the users lap score
    private int HandleStrategy(RaceDriver driver, LapScore lapScore)
    {
        int assignedLifeScore = driver.TyreLife;

        var tyreMinWear = driver.CurrentTyre.WearMin + driver.WearMinMod;
        var tyreMaxWear = driver.CurrentTyre.WearMax + driver.WearMaxMod;

        if (tyreMinWear > tyreMaxWear)
        {
            tyreMaxWear = tyreMinWear + 1;
            _snackbar.Add("A situation occurred where the maximum wear was lower than the minimum wear, please reconsider some set tyre wear values!");
        }

        // Triggers a pitstop if condition is met
        if (ValidTyres.Any() && driver.CurrentTyre.PitWhenBelow > driver.TyreLife)
        {
            ChangeTyres(driver, lapScore);

            assignedLifeScore = driver.TyreLife;
        }

        // Adds wear to the tyre
        driver.TyreLife -= NumberHelper.RandomInt(tyreMinWear, tyreMaxWear);

        if (driver.TyreLife < driver.CurrentTyre.MinimumLife)
            driver.TyreLife = driver.CurrentTyre.MinimumLife;

        return assignedLifeScore;
    }

    // returns boolean which indicates whether a safety car is returning after the current advance
    private bool HandleSafetyMoment(List<LapScore> lapScoresToPersist)
    {
        int scoreAboveDriver = 0;
        foreach (var driver in RaceDrivers.Where(e => e.Status == RaceStatus.Racing).OrderBy(e => e.AbsolutePosition))
        {
            driver.SingleOccurrence = null;
            driver.InstantOvertaken = false;

            var lapScore = new LapScore { ResultId = driver.ResultId, Order = calculated };

            // If the tyre life is less than half of the tyres overall pace, then check for a pitstop
            if (ValidTyres.Count() > 0 && driver.TyreLife < (driver.CurrentTyre.Pace / 2))
            {
                ChangeTyres(driver, lapScore);

                var pitCost = GetPitstopCost();

                lapScore.Score -= pitCost;
            }
            else
            {
                // closes the gap to the driver above
                var scoreGap = scoreAboveDriver - driver.LapSum;

                // Current score gap is greater than the expected SC-gap
                if (scoreGap > Model.League.SafetyCarGap)
                {
                    int gapSubtractedBy = Model.League.SafetyCarGapCloser;

                    if (scoreGap < Model.League.SafetyCarGapCloser)
                        gapSubtractedBy = scoreGap - Model.League.SafetyCarGap;

                    lapScore.Score += gapSubtractedBy;
                }
            }

            var tyreMinWear = driver.CurrentTyre.WearMin + driver.WearMinMod;
            var tyreMaxWear = driver.CurrentTyre.WearMax + driver.WearMaxMod;

            if (tyreMinWear > 0)
                tyreMinWear = tyreMinWear / safetyWearDivider;
            if (tyreMaxWear > 0)
                tyreMaxWear = tyreMaxWear / safetyWearDivider;

            driver.TyreLife -= NumberHelper.RandomInt(tyreMinWear, tyreMaxWear);

            if (driver.TyreLife < driver.CurrentTyre.MinimumLife)
                driver.TyreLife = driver.CurrentTyre.MinimumLife;

            lapScore.TyreColour = driver.CurrentTyre.Colour;

            driver.LapScores.Add(lapScore);
            lapScoresToPersist.Add(lapScore);

            // Only update the score of the above driver if the driver did NOT take this opportunity for a pitstop
            if (!lapScore.RacerEvents.HasFlag(RacerEvent.Pitstop))
                scoreAboveDriver = driver.LapSum;
        }

        SetPositions();

        return NumberHelper.RandomInt(Model.League.SafetyCarReturnOdds) == 0;
    }

    // Returns the score cost which the pitstop took
    private void ChangeTyres(RaceDriver driver, LapScore lapScore)
    {
        var currentTyres = ValidTyres.Where(e => e.Id != driver.CurrentTyre.Id).ToList();

        Tyre nextTyre;
        if (currentTyres.Count() > 1)
            nextTyre = currentTyres.TakeRandomItem();
        else if (currentTyres.Count() == 1)
            nextTyre = currentTyres.First();
        else
            nextTyre = ValidTyres.First();

        driver.CurrentTyre = nextTyre;
        driver.TyreLife = nextTyre.Pace + driver.LifeBonus;
        driver.InstantOvertaken = true;

        lapScore.RacerEvents |= RacerEvent.Pitstop;
    }

    private int GetPitstopCost()
    {
        var pitCost = NumberHelper.RandomInt(Model.Season.PitMinimum, Model.Season.PitMaximum);

        // Pitstop duration is reduced since this is a safety car moment
        if (safetyCarOut && pitCost > Model.Season.PitCostSubtractCaution)
            pitCost -= Model.Season.PitCostSubtractCaution;

        return pitCost;
    }

    // Actions to undertake after having triggered an "Advance"
    private void PostProcessAdvance()
    {
        // Cycle through the reliability, to check something else (otherwise we have way too many DNFs)
        reliablityCycler++;
        reliablityCycler %= cycleableReliablities.Length;
        ActiveReliabilityCheck = cycleableReliablities[reliablityCycler];

        // Set the next amount of tyres valid for drivers to stop on to.
        var distanceLeft = Model.Race.RaceLength - (calculated * calculationDistance);
        ValidTyres = AvailableTyres.FindValidTyres(distanceLeft, Model.Climate.IsWet).ToList();
    }

    // Recalculates the positions of the participating race drivers, includes atacking and defending
    private void DeterminePositions()
    {
        var allPositionsAligned = false;
        // Need to re-retrieve this for every driver since their positions may change due to over overtakes (maybe) / altough i dont think this matters
        var actualPositions = GetCurrentActualPositions();

        // This likely can be optimized further
        while (!allPositionsAligned)
        {
            foreach (var driver in RaceDrivers.Where(e => e.Status == RaceStatus.Racing).OrderBy(e => e.AbsolutePosition))
            {
                var lastScore = driver.LapScores.Last();

                int positionChange = driver.AbsolutePosition - actualPositions[driver.SeasonDriverId];

                // Assign the new positions based on whether their overtakes have been succesful
                if (positionChange > 0)
                    HandlePositionGain(driver, lastScore, positionChange);

                driver.LastScore = lastScore.Score;
            }

            allPositionsAligned = true;
            actualPositions = GetCurrentActualPositions();

            foreach (var driver in RaceDrivers.Where(e => e.Status == RaceStatus.Racing).OrderBy(e => e.AbsolutePosition))
            {
                if (driver.AbsolutePosition != actualPositions[driver.SeasonDriverId])
                    allPositionsAligned = false;
            }
        }

        SetPositions();
    }

    private void HandlePositionGain(RaceDriver driver, LapScore lastScore, int gainedPositions)
    {
        var battleRng = Model.League.BattleRng;

        while (gainedPositions > 0)
        {
            var abovePosition = driver.AbsolutePosition - 1;
            if (abovePosition == 0)
                break;

            var defendingDriver = RaceDrivers.First(e => e.AbsolutePosition == abovePosition);

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
                    defendingResult = defendingResult / 2;

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

    private void PreProcessFinish()
    {
        bool anyoneDisqualified = false;

        foreach (var driver in RaceDrivers.Where(e => e.Status == RaceStatus.Racing))
        {
            if (NumberHelper.RandomInt(Model.League.DisqualificationOdds) == 0)
            {
                driver.Incident = Incidents
                    .Where(e => e.Category == IncidentCategory.Disqualified)
                    .ToList()
                    .TakeRandomIncident();
                driver.Status = RaceStatus.Dsq;

                anyoneDisqualified = true;
            }
        }

        if (anyoneDisqualified)
            SetPositions();
    }

    private void SetPositions()
    {
        int absoluteIndex = 0;
        int scoreAboveDriver = 0;

        var positionIndexDict = RaceDrivers.Select(e => e.ClassId).Distinct().ToDictionary(e => e, _ => 0);

        foreach (var driver in RaceDrivers.OrderBy(e => (int)e.Status).ThenByDescending(e => e.LapSum))
        {
            driver.Position = ++positionIndexDict[driver.ClassId];
            driver.AbsolutePosition = ++absoluteIndex;

            driver.GapAbove = driver.AbsolutePosition == 1
                ? "LEADER"
                : "+" + (Math.Round((scoreAboveDriver - driver.LapSum) * Config.GapMarge, 2)).ToString("F2");

            scoreAboveDriver = driver.LapSum;
        }
    }

    private async Task Finish()
    {
        PreProcessFinish();

        // Update object references
        Model.SetRacerData(RaceDrivers);

        // Should be removed when it is done per advance
        var allLapScores = RaceDrivers.SelectMany(e => e.LapScores).Where(e => e.Id == 0).ToList();
        LapScores.AddRange(allLapScores);

        int initialHighestOccurr = 0;
        if (Occurrences.Any())
            initialHighestOccurr = Occurrences.Select(e => e.Order).Max();

        foreach (var occurr in AdvanceOccurrences.Where(e => e.Key > initialHighestOccurr))
        {
            Occurrences.Add(new RaceOccurrence
            {
                Order = occurr.Key,
                Occurrences = occurr.Value,
                RaceId = Model.Race.Id,
            });
        }

        await OnFinish.InvokeAsync();
    }

    private void ChangeCalculationPerAdvance(MudChip? calcChip)
    {
        if (calcChip != null)
            calculationsPerAdvance = (int)calcChip.Value;
    }

    private void AddCalculationSituation() => AdvanceOccurrences[calculated] = CurrentSituation;

    private bool DidReliabilityFail(int reliability) => NumberHelper.RandomInt(1000) > reliability;

    private int GetCurrentLapCount => NumberHelper.LapCount(calculated * calculationDistance, Model.Race.Track.Length);

    private Dictionary<long, int> GetCurrentActualPositions()
    {
        var actualPositions = new Dictionary<long, int>();
        int absoluteIndex = 0;

        foreach (var driver in RaceDrivers.OrderBy(e => (int)e.Status).ThenByDescending(e => e.LapSum))
            actualPositions.Add(driver.SeasonDriverId, ++absoluteIndex);

        return actualPositions;
    }

    // Honestly, below is more of an example to the first one
    // private Dictionary<long, int> AggregateActualPositions()
    // {
    //     //source: racedrivers
    //     //seed: dict
    //     //func: (dict, driver) =>

    //     var absoluteIndex = 0;
    //     return RaceDrivers
    //         .OrderBy(e => (int)e.Status)
    //             .ThenByDescending(e => e.LapSum)
    //         .Aggregate
    //         (
    //             new Dictionary<long, int>(),
    //             (dict, driver) =>
    //             {
    //                 dict.Add(driver.SeasonDriverId, ++absoluteIndex);
    //                 return dict;
    //             }
    //         );
    // }

    private async Task ShowGapperChart() => _ = await _dialogService.ShowAsync<GapChartDialog>(
        "Gapper chart",
        new DialogParameters { ["Drivers"] = RaceDrivers },
        Globals.StatisticDialogDefaultOptionsXl);

    private async Task ShowPositionChart() => _ = await _dialogService.ShowAsync<PositionChangeChart>(
        "Position chart",
        new DialogParameters { ["Drivers"] = RaceDrivers },
        Globals.StatisticDialogDefaultOptionsXl);
}