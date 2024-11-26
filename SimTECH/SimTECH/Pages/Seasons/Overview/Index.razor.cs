using Microsoft.AspNetCore.Components;
using MudBlazor;
using SimTECH.Common.Enums;
using SimTECH.Data.EditModels;
using SimTECH.Data.Models;
using SimTECH.PageModels.Seasons;
using SimTECH.Pages.Seasons.Dialogs;
using SimTECH.Pages.Seasons.Overview.Dialogs;

namespace SimTECH.Pages.Seasons.Overview;

public partial class Index
{
    [Parameter]
    public long SeasonId { get; set; }

    [Parameter]
    public int TabIndex { get; set; }

    // TIP: You should only make changes at a cascading model at the provider level, aka here for OverviewModel
    // Even better is to not use ONE model for the entire overview, jesus christ
    public OverviewModel OverviewModel { get; set; } = new();

    private Race? NextRace { get; set; }
    private List<League> Leagues { get; set; } = [];
    private List<Climate> Climates { get; set; } = [];

    private int activeTabIndex;
    private int driverStatusMod;
    private bool loading = true;
    private bool ContractsEnabled { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _bread.SetBreadcrumbs(
        [
            new("Home", href: ""),
            new("Seasons", href: "seasons"),
            new("Overview", href: $"overview/{SeasonId}", disabled: true),
        ]);

        activeTabIndex = TabIndex;

        OverviewModel.Season = await _seasonService.GetSeasonById(SeasonId);
        OverviewModel.Races = await _raceService.GetRacesBySeason(SeasonId);
        //OverviewModel.SeasonEngines = await _seasonEngineService.GetSeasonEngines(SeasonId);
        OverviewModel.SeasonTeams = await _seasonTeamService.GetSeasonTeams(SeasonId);
        OverviewModel.SeasonDrivers = await _seasonDriverService.GetSeasonDrivers(SeasonId);
        OverviewModel.Results = await _resultService.GetResultsOfSeason(SeasonId);
        OverviewModel.Manufacturers = await _manufacturerService.GetManufacturers(StateFilter.All);

        if (OverviewModel.Season.State == State.Active)
            NextRace = OverviewModel.Races.FindNext();

        if (OverviewModel.Season.HasRaceClasses)
            OverviewModel.ActiveClassId = OverviewModel.Season.RaceClasses.First().Id;

        Climates = await _climateService.GetClimates(StateFilter.All);
        Leagues = await _leagueService.GetLeagues();

        var currentLeague = Leagues.Find(e => e.Id == OverviewModel.Season.LeagueId);
        driverStatusMod = currentLeague?.DriverStatusPaceModifier ?? 0;
        ContractsEnabled = currentLeague?.Options.HasFlag(LeagueOptions.AllowContracting) ?? false;

        loading = false;
    }

    private async Task StartSeason()
    {
        if (OverviewModel.Season == null)
            return;

        var error = await _seasonService.ActivateSeason(SeasonId);

        if (!string.IsNullOrEmpty(error))
        {
            _snackbar.Add(error, Severity.Error);
            return;
        }

        // Subtract active and added contract durations with 1 (if it is valid)
        await _contractService.SubtractDurations(OverviewModel.Season.LeagueId, SeasonId);

        OverviewModel.Season.State = State.Active; // NOTE: It always changes to active if started
        NextRace = await _raceService.GetNextRaceOfSeason(SeasonId);

        _snackbar.Add($"Season {OverviewModel.Season.Year} has been activated!", Severity.Success);

        StateHasChanged();
    }

    private async Task UpdateSeason()
    {
        var editModel = new EditSeasonModel(OverviewModel.Season);

        var parameters = new DialogParameters { ["Model"] = editModel, ["Leagues"] = Leagues };

        var dialog = await _dialogService.ShowAsync<SeasonEditor>("Modify season", parameters);
        var result = await dialog.Result;

        if (result == null || result.Canceled)
            return;

        if (!result.Canceled && result.Data != null && result.Data is Season updatedItem)
        {
            await _seasonService.UpdateSeason(updatedItem);
            OverviewModel.Season = updatedItem;
        }
    }

    private async Task FinishSeason()
    {
        bool? confirm = await _dialogService.ShowMessageBox(
            "Warning",
            "Are you sure you want to finish this season, this action cannot be undone!",
            yesText: "Yep", cancelText: "Nope");

        if (confirm == true)
        {
            await _seasonService.FinishSeason(SeasonId);
            _nav.NavigateTo("/seasons");
        }
    }

    private void SetActiveRaceClass(long raceClassId)
    {
        OverviewModel.ActiveClassId = raceClassId;
        StateHasChanged();
    }

    private async Task AddRaces()
    {
        var league = Leagues.First(e => e.Id == OverviewModel.Season.LeagueId);

        var availableTracks = await _trackService.GetAvailableTracks(SeasonId);

        if (availableTracks.Count == 0)
        {
            _snackbar.Add("there aren't any tracks left to add to this season, ya absolute willy", Severity.Error);
            return;
        }

        var parameters = new DialogParameters
        {
            ["Tracks"] = availableTracks,
            ["SeasonId"] = SeasonId,
            ["OffsetRound"] = 1 + OverviewModel.Races.Count,
            ["DefaultRaceLength"] = league.RaceLength,
            ["Climates"] = Climates.Where(e => e.State == State.Active).ToList(),
        };

        var dialog = await _dialogService.ShowAsync<AddRaces>("Insert races", parameters);
        var result = await dialog.Result;

        if (result == null || result.Canceled)
            return;

        if (!result.Canceled && result.Data != null && result.Data is List<Race> addedRaces)
        {
            await _raceService.InsertRaces(addedRaces);
            OverviewModel.Races = await _raceService.GetRacesBySeason(SeasonId);
        }
    }

    private async Task UpdateRace(Race item)
    {
        var league = Leagues.First(e => e.Id == OverviewModel.Season.LeagueId);

        var parameters = new DialogParameters
        {
            ["Race"] = item,
            ["SeasonId"] = SeasonId,
        };

        var dialog = await _dialogService.ShowAsync<RaceEditor>("Modify Race", parameters);
        var result = await dialog.Result;

        if (result == null || result.Canceled)
            return;

        if (!result.Canceled && result.Data != null && result.Data is Race updatedRace)
        {
            await _raceService.UpdateRace(updatedRace);
            OverviewModel.Races = await _raceService.GetRacesBySeason(SeasonId);
        }
    }

    private async Task DeleteRace(long raceId)
    {
        await _raceService.DeleteRace(raceId);
        OverviewModel.Races = await _raceService.GetRacesBySeason(SeasonId);
    }

    private async Task UpdateDriver(SeasonDriver? item)
    {
        var drivers = await _driverService.GetAvailableDrivers(SeasonId);

        var takenNumbers = OverviewModel.SeasonDrivers.Select(e => e.Number).ToArray();

        var parameters = new DialogParameters
        {
            ["SeasonId"] = OverviewModel.Season.Id,
            ["SeasonTeams"] = OverviewModel.SeasonTeams,
            ["Drivers"] = drivers,
            ["SeasonDriver"] = item,
            ["TakenNumbers"] = takenNumbers,
        };

        var dialog = await _dialogService.ShowAsync<SeasonDriverEditor>("Modify in-season driver", parameters);
        var result = await dialog.Result;

        if (result == null || result.Canceled)
            return;

        if (!result.Canceled && result.Data != null && result.Data is SeasonDriver updatedDriver)
        {
            await _seasonDriverService.UpdateSeasonDriver(updatedDriver);
            OverviewModel.SeasonDrivers = await _seasonDriverService.GetSeasonDrivers(SeasonId);
        }
    }

    private async Task UpdateTeam(SeasonTeam? item)
    {
        var teams = await _teamService.GetAvailableTeams(SeasonId);
        var seasonEngines = await _seasonEngineService.GetSeasonEngines(SeasonId);

        var activeManufacturers = OverviewModel.Manufacturers.Where(e => e.State == State.Active).ToList();

        var parameters = new DialogParameters
        {
            ["SeasonId"] = SeasonId,
            ["Teams"] = teams,
            ["Manufacturers"] = activeManufacturers,
            ["SeasonEngines"] = seasonEngines,
            ["SeasonTeam"] = item,
        };

        var dialog = await _dialogService.ShowAsync<SeasonTeamEditor>("Modify in-season team", parameters);
        var result = await dialog.Result;

        if (result == null || result.Canceled)
            return;

        if (!result.Canceled && result.Data != null && result.Data is SeasonTeam updatedTeam)
        {
            await _seasonTeamService.UpdateSeasonTeam(updatedTeam);
            OverviewModel.SeasonTeams = await _seasonTeamService.GetSeasonTeams(SeasonId);
        }
    }

    private async Task UpdateEngine(SeasonEngine? item)
    {
        var engines = await _engineService.GetAvailableEngines(SeasonId);

        var parameters = new DialogParameters
        {
            ["SeasonId"] = SeasonId,
            ["Engines"] = engines,
            ["SeasonEngine"] = item,
        };

        var dialog = await _dialogService.ShowAsync<SeasonEngineEditor>("Modify in-season engine", parameters);
        var result = await dialog.Result;

        if (result == null || result.Canceled)
            return;

        if (!result.Canceled && result.Data != null && result.Data is SeasonEngine updatedEngine)
        {
            await _seasonEngineService.UpdateSeasonEngine(updatedEngine);
            //OverviewModel.SeasonEngines = await _seasonEngineService.GetSeasonEngines(SeasonId);
        }
    }
}
