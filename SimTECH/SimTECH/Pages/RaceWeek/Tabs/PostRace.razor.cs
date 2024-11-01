﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.PageModels.RaceWeek;

namespace SimTECH.Pages.RaceWeek.Tabs;

public partial class PostRace
{
    [CascadingParameter] public RaweCeekModel Model { get; set; }

    private List<DriverPenalty> DriverPenalties { get; set; } = [];

    private Dictionary<int, int> Allotments { get; set; } = [];

    private List<RaceClass> RaceClasses { get; set; } = [];

    private List<GivenPenalty> UpcomingPenalties { get; set; } = [];

    private bool Loading { get; set; }

    private TableGroupDefinition<RaweCeekDriver>? raceClassDefinition;

    protected override void OnInitialized()
    {
        if (Model.Season.PointAllotments != null)
            Allotments = Model.Season.PointAllotments.ToDictionary(e => e.Position, e => e.Points);

        if (Model.Season.HasRaceClasses)
        {
            // Only assigned here since the definition is used to group, if no race class then it needs to be null
            raceClassDefinition = new()
            {
                GroupName = "Class",
                Selector = (e) => e.ClassId
            };

            foreach (var raceClass in Model.RaweCeekDrivers.Select(e => e.Class).Distinct())
            {
                if (raceClass != null)
                    RaceClasses.Add(raceClass);
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        var nextRound = 1 + Model.Race.Round;
        var nextRaceId = await _raceService.GetRaceIdByRound(Model.Season.Id, nextRound);

        if (nextRaceId.HasValue)
        {
            var nextRacePenalties = await _raceWeekService.GetRacePenalties(nextRaceId.Value);
            if (nextRacePenalties?.Any() ?? false)
                UpcomingPenalties = nextRacePenalties;
        }
    }

    protected override void OnParametersSet()
    {
        Loading = true;

        BuildDriverPenalties();

        Loading = false;
    }

    private int ReadScoredResult(RaweCeekDriver result)
    {
        var points = 0;

        if (result.Grid == 1)
            points += Model.Season.PointsPole;

        if (result.FastestLap)
            points += Model.Season.PointsFastestLap;

        if (result.Status != RaceStatus.Racing)
            return points;

        if (Allotments.TryGetValue(result.Position, out int allotedPoints))
            points += allotedPoints;

        return points;
    }

    private string GetRaceClassName(long classId)
    {
        if (!Model.Season.HasRaceClasses || RaceClasses.Count == 0)
            return string.Empty;

        return RaceClasses.FirstOrDefault(e => e.Id == classId)?.Name ?? string.Empty;
    }

    // NOTE: Not a favourable solution, could use a better implementation
    private void BuildDriverPenalties()
    {
        foreach (var penalty in UpcomingPenalties)
        {
            var driver = Model.RaweCeekDrivers.Find(e => e.SeasonDriverId == penalty.SeasonDriverId);
            if (driver != null)
            {
                DriverPenalties.Add(new DriverPenalty
                {
                    Name = driver.FullName,
                    Reason = penalty.Incident.Name,
                    Punishment = penalty.Incident.Punishment,
                    //Styles = driverResult.Status.StatusStyles(),
                });
            }
        }
    }

    // The number "2" refers to the tab-index at overview here, which in this case is the "Standings"
    private void GoToOverview() => _navigation.NavigateTo($"overview/{Model.Season.Id}/2");
}
