﻿using Microsoft.AspNetCore.Components;
using SimTECH.Common.Enums;
using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels.RaceWeek;

namespace SimTECH.Pages.RaceWeek.Tabs;

public partial class Practice
{
    [CascadingParameter]
    public RaweCeekModel Model { get; set; }

    [Parameter]
    public PracticeSession PracticeSession { get; set; } = new();

    [Parameter]
    public EventCallback<int> OnFinish { get; set; }

    private List<SessionDriver> PracticeDrivers { get; set; } = new();

    private bool Loading { get; set; } = true;

    string raceName = string.Empty;
    Country raceCountry = Globals.DefaultCountry;
    int amountRuns;
    int practiceRng;
    int advancedRuns;
    int highestScore;
    int lowestScore = int.MaxValue;
    bool hasRaceClasses;
    double gapMarge;

    protected override void OnInitialized()
    {
        Loading = true;

        gapMarge = Model.GapMarge / 2;

        raceName = $"{Model.Race.Name} - PRACTICE {PracticeSession.SessionIndex}";
        raceCountry = Model.Race.Track.Country;
        amountRuns = Model.Season.RunAmountSession;
        practiceRng = Model.Season.QualifyingRNG;
        hasRaceClasses = Model.Season.HasRaceClasses;

        if (PracticeSession.IsFinished)
        {
            var scoreNumbers = PracticeSession.SessionScores
                .SelectMany(e => e.Scores ?? Array.Empty<int>())
                .Where(e => e != 0)
                .ToArray();

            highestScore = scoreNumbers.Max();
            lowestScore = scoreNumbers.Min();
        }

        foreach (var driver in Model.RaweCeekDrivers)
        {
            var mappedDriver = driver.MapToSessionDriver(amountRuns);

            var driverScore = PracticeSession.SessionScores.FirstOrDefault(e => e.ResultId == driver.ResultId);

            if (driverScore?.Scores?.Any() ?? false)
            {
                mappedDriver.Scores = driverScore.Scores;
                mappedDriver.Position = driverScore.Position;
                mappedDriver.AbsolutePosition = driverScore.AbsolutePosition;

                if (mappedDriver.Position == 1)
                    mappedDriver.GapAbove = "LEADER";
                else
                    mappedDriver.GapAbove = "+" + (Math.Round((highestScore - mappedDriver.MaxScore()) * gapMarge, 2)).ToString("F2");
            }

            PracticeDrivers.Add(mappedDriver);
        }

        Loading = false;
    }

    private async void Advance()
    {
        foreach (var driver in PracticeDrivers)
        {
            var result = driver.Power + NumberHelper.RandomInt((practiceRng * -1), practiceRng);
            driver.Scores[advancedRuns] = result;

            if (result > highestScore)
                highestScore = result;
            else if (result < lowestScore)
                lowestScore = result;
        }

        var positionIndexDict = PracticeDrivers
            .Select(e => e.ClassId)
            .Distinct()
            .ToDictionary(e => e, e => 0);

        int absoluteIndex = 0;

        foreach (var driver in PracticeDrivers.OrderByDescending(e => e.MaxScore()))
        {
            driver.Position = ++positionIndexDict[driver.ClassId];
            driver.AbsolutePosition = ++absoluteIndex;

            driver.GapAbove = driver.AbsolutePosition == 1
                ? "LEADER"
                : "+" + (Math.Round((highestScore - driver.MaxScore()) * gapMarge, 2)).ToString("F2");
        }

        advancedRuns++;

        if (advancedRuns == amountRuns)
            await Finish();
    }

    private async Task Finish()
    {
        var newScores = PracticeDrivers.Select(e => new PracticeScore
        {
            Index = PracticeSession.SessionIndex,
            Scores = e.Scores,
            Position = e.Position,
            AbsolutePosition = e.AbsolutePosition,
            RaceId = Model.Race.Id,
            ResultId = e.ResultId
        })
            .ToList();

        PracticeSession.SessionScores.AddRange(newScores);

        await OnFinish.InvokeAsync(PracticeSession.SessionIndex);
    }
}