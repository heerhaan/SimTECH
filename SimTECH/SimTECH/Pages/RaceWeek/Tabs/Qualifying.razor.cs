﻿using Microsoft.AspNetCore.Components;
using SimTECH.Common.Enums;
using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels.RaceWeek;

namespace SimTECH.Pages.RaceWeek.Tabs;

public partial class Qualifying
{
    [CascadingParameter]
    public RaweCeekModel Model { get; set; }

    [Parameter]
    public QualifyingSession QualySession { get; set; } = new();

    [Parameter]
    public int MaximumAllowed { get; set; } = int.MaxValue;

    [Parameter]
    public EventCallback<int> OnFinish { get; set; }

    private List<SessionDriver> QualyDrivers { get; set; } = [];

    bool loading = true;
    string raceName = string.Empty;
    Country raceCountry = Globals.DefaultCountry;
    int amountRuns;
    int advancedRuns;
    int qualyRng;
    int entryCutoff;
    int progressCutoff;
    int highestScore;
    int lowestScore = int.MaxValue;
    bool hasRaceClasses;

    double gapMarge;

    protected override void OnInitialized()
    {
        loading = true;

        raceName = $"{Model.Race.Name} - QUALIFYING {QualySession.SessionIndex}";
        raceCountry = Model.Race.Track.Country;
        amountRuns = Model.Season.RunAmountSession;
        qualyRng = Model.Season.QualifyingRNG;
        entryCutoff = Model.Season.MaximumDriversInRace;
        progressCutoff = QualySession.ProgressionCutoff;
        hasRaceClasses = Model.Season.HasRaceClasses;

        if (QualySession.IsFinished)
        {
            var scoreNumbers = QualySession.SessionScores
                .SelectMany(e => e.Scores ?? Array.Empty<int>())
                .Where(e => e != 0)
                .ToArray();
            highestScore = scoreNumbers.Max();
            lowestScore = scoreNumbers.Min();
        }

        gapMarge = Model.GapMarge / 2;

        foreach (var driver in Model.RaweCeekDrivers.Where(e => QualySession.AllowedEntries >= e.Grid))
        {
            var mappedDriver = driver.MapToSessionDriver(amountRuns);
            mappedDriver.PenaltyPunish = driver.Penalty.GetValueOrDefault();

            var driverScore = QualySession.SessionScores.FirstOrDefault(e => e.ResultId == driver.ResultId);

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

            QualyDrivers.Add(mappedDriver);
        }

        loading = false;
    }

    private async void Advance()
    {
        foreach (var driver in QualyDrivers)
        {
            var result = driver.Power + NumberHelper.RandomInt(qualyRng);
            driver.Scores[advancedRuns] = result;

            if (result > highestScore)
                highestScore = result;
            else if (result < lowestScore)
                lowestScore = result;
        }

        var positionIndexDict = QualyDrivers.Select(e => e.ClassId).Distinct().ToDictionary(e => e, e => 0);
        int absoluteIndex = 0;

        foreach (var driver in QualyDrivers.OrderByDescending(e => e.MaxScore()))
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
        var newScores = QualyDrivers.Select(e => new QualifyingScore
        {
            Index = QualySession.SessionIndex,
            Scores = e.Scores,
            Position = e.Position,
            AbsolutePosition = e.AbsolutePosition,
            RaceId = Model.Race.Id,
            ResultId = e.ResultId,

            PenaltyPunish = e.PenaltyPunish
        })
            .ToList();

        QualySession.SessionScores.AddRange(newScores);

        await OnFinish.InvokeAsync(QualySession.SessionIndex);
    }
}
