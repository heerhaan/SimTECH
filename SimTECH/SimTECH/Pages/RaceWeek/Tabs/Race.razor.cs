using Microsoft.AspNetCore.Components;
using MudBlazor;
using SimTECH.Common.Enums;
using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.Data.Services;
using SimTECH.Data.Services.Interfaces;
using SimTECH.Extensions;
using SimTECH.PageModels.RaceWeek;
using SimTECH.Pages.RaceWeek.Dialogs;

namespace SimTECH.Pages.RaceWeek.Tabs;

public partial class Race
{
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
            RacerEvent.MaintainPosition,
            RacerEvent.FastestLap,
        ];

    #region injected services
    [Inject]
    private IIncidentService _incidentService { get; set; }

    [Inject]
    private IRaceWeekService _raceWeekService { get; set; }

    [Inject]
    private IDialogService _dialogService { get; set; }

    [Inject]
    private ISnackbar _snackbar { get; set; }
    #endregion

    [CascadingParameter]
    public RaweCeekModel Model { get; set; }

    [Parameter]
    public long RaceId { get; set; }

    [Parameter]
    public SimConfig Config { get; set; } = new();

    [Parameter]
    public EventCallback OnFinish { get; set; } = new();

    private List<LapScore> LapScores { get; set; } = [];

    private List<RaceOccurrence> Occurrences { get; set; } = [];

    private List<Incident> Incidents { get; set; } = [];

    private List<Tyre> Tyres { get; set; } = [];

    private Dictionary<int, SituationOccurrence> AdvanceOccurrences { get; set; } = [];

    private List<RaceDriver> RaceDrivers { get; set; } = [];

    private List<Tyre> ValidTyres { get; set; } = [];

    private bool Loading { get; set; } = true;
    private int RacedLaps { get; set; }
    private int TotalLaps { get; set; }

    private SituationOccurrence CurrentSituation { get; set; } = SituationOccurrence.Raced;

    private Entrant ActiveReliabilityCheck { get; set; } = Entrant.Driver;
    private int reliablityCycler = 0;

    private int fastestLap;
    private int calculated;
    private int calculationCount;
    private int calculationsPerAdvance = 5;
    private int calculationDistance = 10;

    private int lastHighestScore = 0;
    private int lastLowestScore = 0;

    private RaceManager raceManager;

    private bool IsFirstLap => calculated == 1;

    protected override async Task OnInitializedAsync()
    {
        Loading = true;

        LapScores = await _raceWeekService.GetLapScores(RaceId);
        Occurrences = await _raceWeekService.GetRaceOccurrences(RaceId);
        Incidents = await _incidentService.GetIncidents(StateFilter.Active);
        Tyres = await _raceWeekService.GetValidTyresForRace(Model.League.Id);

        raceManager = new(Model.Season, Model.League, Incidents, Config);

        calculationDistance = Config.CalculationDistance;
        calculationCount = Model.Race.RaceLength / calculationDistance;

        if (LapScores.Count != 0)
            calculated = LapScores.Max(e => e.Order);

        for (int index = 1; index <= calculationCount; index++)
        {
            var situationMatch = Occurrences.FirstOrDefault(e => e.Order == index);

            AdvanceOccurrences.Add(index, situationMatch?.Occurrences ?? SituationOccurrence.Unknown);
        }

        RacedLaps = GetCurrentLapCount;
        TotalLaps = NumberHelper.LapCount(Model.Race.RaceLength, Model.Race.Track.Length);

        BuildRaceDrivers();

        ValidTyres = Tyres.FindValidTyres(GetDistanceLeft, Model.Climate.IsWet).ToList();

        Loading = false;
    }

    private int GetCurrentLapCount => NumberHelper.LapCount(calculated * calculationDistance, Model.Race.Track.Length);

    private int GetDistanceLeft => Model.Race.RaceLength - (calculationDistance * calculated);

    private void BuildRaceDrivers()
    {
        RaceDrivers = Model.RaweCeekDrivers
            .OrderBy(e => e.AbsolutePosition)
            .Take(Model.Season.MaximumDriversInRace)
            .Select(e => e.MapToRaceDriver())
            .ToList();

        // Refactor this if persisting happens per advance
        if (Model.Race.State is State.Closed)
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
            raceManager.AddFormationLap(RaceDrivers);
        }

        raceManager.SetPositions(RaceDrivers);
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

            AdvanceOccurrences[calculated] = CurrentSituation;

            var situationBeforeAdvance = CurrentSituation;

            switch (situationBeforeAdvance)
            {
                case SituationOccurrence.Raced:
                    RegularAdvance(lapScoresToPersist);
                    break;
                case SituationOccurrence.Caution:
                case SituationOccurrence.Halted:
                    CautionAdvance(lapScoresToPersist);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unrecognized situation: {CurrentSituation}");
            }

            raceManager.DeterminePositions(RaceDrivers);

            PostProcessAdvance();

            // Status of the safety car has changed, stop progression so the user KNOWS
            if (CurrentSituation != situationBeforeAdvance)
                break;
        }

        RacedLaps = GetCurrentLapCount;

        var lastScores = RaceDrivers
            .Where(e => e.Status == RaceStatus.Racing)
            .Select(e => e.LastScore)
            .ToArray();

        if (lastScores.Length != 0)
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

    private void RegularAdvance(List<LapScore> lapScoresToPersist)
    {
        foreach (var driver in RaceDrivers.Where(e => e.Status == RaceStatus.Racing).OrderBy(e => e.AbsolutePosition))
        {
            driver.SingleOccurrence = null;
            driver.InstantOvertaken = false;
            driver.RecentMistake = false;

            var lapScore = new LapScore { ResultId = driver.ResultId, Order = calculated };

            // Determine if either driver, car or engine has failed
            var isCautionSituation = raceManager.CheckReliability(driver, lapScore, ActiveReliabilityCheck, IsFirstLap);

            // Calculate the score for drivers which are still racing
            if (driver.Status == RaceStatus.Racing)
            {
                var minRng = Model.Season.RngMinimum + driver.RngMinMod;
                var maxRng = Model.Season.RngMaximum + driver.RngMaxMod;

                lapScore.Score = NumberHelper.RandomInt(minRng, maxRng);

                raceManager.CheckIfDriverMadeMistake(driver, lapScore);

                raceManager.HandleDriverStrategy(driver, lapScore, ValidTyres, false);

                // Adds the overall power of the driver
                lapScore.Score += driver.Power;
            }
            // If this get's triggered then the current driver caused a safety car, racing goes on as normal until the next advance
            else if (isCautionSituation)
            {
                if (driver.Status is RaceStatus.Fatal)
                    CurrentSituation = SituationOccurrence.Halted;
                else
                    CurrentSituation = SituationOccurrence.Caution;
            }

            driver.LapScores.Add(lapScore);
            lapScoresToPersist.Add(lapScore);
        }

        // Checks whether someone set a new fastest lap
        if (IsFirstLap == false)
        {
            var fastestLapScore = lapScoresToPersist
                .OrderByDescending(e => e.Score)
                .FirstOrDefault(e => e.Score > fastestLap);

            if (fastestLapScore != null)
            {
                foreach (var raceDriver in RaceDrivers)
                    raceDriver.HasFastestLap = fastestLapScore.ResultId == raceDriver.ResultId;

                fastestLapScore.RacerEvents |= RacerEvent.FastestLap;

                fastestLap = fastestLapScore.Score;
            }
        }
    }

    private void CautionAdvance(List<LapScore> lapScoresToPersist)
    {
        // First check for the pitstops
        foreach (var driver in RaceDrivers.Where(e => e.Status == RaceStatus.Racing))
        {
            driver.SingleOccurrence = null;
            driver.InstantOvertaken = false;

            var lapScore = new LapScore { ResultId = driver.ResultId, Order = calculated };

            raceManager.HandleDriverStrategy(driver, lapScore, ValidTyres, true);

            driver.LapScores.Add(lapScore);
            lapScoresToPersist.Add(lapScore);
        }

        // Set the updated positions in the case a driver made a pitstop
        raceManager.DeterminePositions(RaceDrivers);

        // Only start closing the gap between drivers after having determined the pitstops
        int scoreAbove = 0;
        foreach (var driver in RaceDrivers.Where(e => e.Status == RaceStatus.Racing).OrderBy(e => e.AbsolutePosition))
        {
            // closes the gap to the driver above
            var scoreGap = scoreAbove - driver.LapSum;

            // Current score gap is greater than the expected SC-gap
            if (scoreGap > Model.League.SafetyCarGap)
            {
                int gapSubtractedBy = Model.League.SafetyCarGapCloser;

                if (Model.League.SafetyCarGapCloser > scoreGap)
                    gapSubtractedBy = scoreGap - Model.League.SafetyCarGap;

                var lapScore = driver.LapScores.Single(e => e.Order == calculated);
                lapScore.Score += gapSubtractedBy;
            }

            scoreAbove = driver.LapSum;
        }

        if (NumberHelper.RandomInt(Model.League.SafetyCarReturnOdds) == 0)
        {
            CurrentSituation = SituationOccurrence.Raced;
        }
    }

    // Actions to undertake after having triggered an "Advance"
    private void PostProcessAdvance()
    {
        // Cycle through the reliability, to check something else (otherwise we have way too many DNFs)
        reliablityCycler++;
        reliablityCycler %= cycleableReliablities.Length;
        ActiveReliabilityCheck = cycleableReliablities[reliablityCycler];

        // Set the next amount of tyres valid for drivers to stop on to
        ValidTyres = Tyres.FindValidTyres(GetDistanceLeft, Model.Climate.IsWet).ToList();
    }

    private void PreProcessFinish()
    {
        foreach (var driver in RaceDrivers.Where(e => e.Status == RaceStatus.Racing))
        {
            if (NumberHelper.RandomInt(Model.League.DisqualificationOdds) == 0)
            {
                driver.Incident = Incidents.TakeRandomIncident(IncidentCategory.Disqualified);
                driver.Status = RaceStatus.Dsq;
            }
        }

        raceManager.SetPositions(RaceDrivers);
    }

    private async Task Finish()
    {
        PreProcessFinish();

        // Update object references / Update the raweceek driver references based upon the race driver results
        // Just puked in my mouth because of this bit of logic, please do away with this eventually
        foreach (var raceDriver in RaceDrivers)
        {
            var matchDriver = Model.RaweCeekDrivers.First(e => e.ResultId == raceDriver.ResultId);

            matchDriver.Position = raceDriver.Position;
            matchDriver.AbsolutePosition = raceDriver.AbsolutePosition;
            matchDriver.Score = raceDriver.LapSum;
            matchDriver.Status = raceDriver.Status;
            matchDriver.TyreLife = raceDriver.TyreLife;
            matchDriver.Tyre = raceDriver.CurrentTyre;
            matchDriver.FastestLap = raceDriver.HasFastestLap;
            matchDriver.Overtaken = raceDriver.OvertakeCount;
            matchDriver.Defended = raceDriver.DefensiveCount;
            matchDriver.Incident = raceDriver.Incident;
        }

        // Should be removed when (if?) saving is done per advance
        var allLapScores = RaceDrivers
            .SelectMany(e => e.LapScores)
            .Where(e => e.Id == 0)
            .ToList();

        LapScores.AddRange(allLapScores);

        var initialHighestOccurrence = Occurrences.Max(e => e.Order);
        foreach (var occurr in AdvanceOccurrences.Where(e => e.Key > initialHighestOccurrence))
        {
            Occurrences.Add(new RaceOccurrence
            {
                Order = occurr.Key,
                Occurrences = occurr.Value,
                RaceId = Model.Race.Id,
            });
        }

        // Normally this was done by the code triggered by the onfinish invocation, now we do it here!
        await PersistRaceData();

        await OnFinish.InvokeAsync();
    }

    private async Task PersistRaceData()
    {
        if (LapScores.Count == 0 || Occurrences.Count == 0)
        {
            _snackbar.Add("Good god, saving a race for which we have NO data. That's so wrong!", Severity.Error);
            return;
        }

        var allotments = Model.Season.PointAllotments?
            .ToDictionary(e => e.Position, e => e.Points) ?? [];
        var finalResults = Model.RaweCeekDrivers
            .Select(e => e.MapToResult(RaceId))
            .ToList();
        var scoredPoints = Model.RaweCeekDrivers
            .Select(e => e.MapToScoredPoints(allotments, Model.Season.PointsPole, Model.Season.PointsFastestLap))
            .ToList();

        await _raceWeekService.PersistLapScores(LapScores);
        await _raceWeekService.PersistOccurrences(Occurrences);
        await _raceWeekService.FinishRace(RaceId, finalResults, scoredPoints);

        if (Model.League.Options.HasFlag(LeagueOptions.EnablePenalty))
            await _raceWeekService.CheckPenalties(finalResults);

        Model.Race.State = State.Closed;
    }

    private void ChangeCalculationPerAdvance(MudChip? calcChip)
    {
        if (calcChip != null)
            calculationsPerAdvance = (int)calcChip.Value;
    }

    private async Task ShowGapperChart() => _ = await _dialogService.ShowAsync<GapChartDialog>(
        "Gapper chart",
        new DialogParameters { ["Drivers"] = RaceDrivers },
        Globals.StatisticDialogDefaultOptionsXl);

    private async Task ShowPositionChart() => _ = await _dialogService.ShowAsync<PositionChangeChart>(
        "Position chart",
        new DialogParameters { ["Drivers"] = RaceDrivers },
        Globals.StatisticDialogDefaultOptionsXl);
}
