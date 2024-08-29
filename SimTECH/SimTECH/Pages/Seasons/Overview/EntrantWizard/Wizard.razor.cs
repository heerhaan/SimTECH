using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudExtensions;
using SimTECH.Common.Enums;
using SimTECH.Data.EditModels;
using SimTECH.Data.Models;
using SimTECH.PageModels.Seasons;

namespace SimTECH.Pages.Seasons.Overview.EntrantWizard;

public partial class Wizard
{
    [Parameter]
    public long SeasonId { get; set; }

    private MudStepperExtended stepper;

    public AddEntrantsModel Model { get; set; } = new();
    public Season Season { get; set; }
    public Season? PreviousSeason { get; set; }
    public List<Contract> Contracts { get; set; } = [];
    public IEnumerable<Manufacturer> Manufacturers { get; set; } = [];

    private EntrantWizardStep ActiveStep { get; set; } = EntrantWizardStep.EnginesAdd;

    private bool allowPersonalNumbers;

    private bool loading = true;

    private List<string> errors = [];

    private string stepTitle => ActiveStep.GetStepTitle();

    protected override async Task OnInitializedAsync()
    {
        Season = await _seasonService.GetSeasonById(SeasonId);
        PreviousSeason = await _seasonService.FindRecentClosedSeason(Season.LeagueId);
        Contracts = await _contractService.GetLeagueContracts(Season.LeagueId);
        Manufacturers = await _manufacturerService.GetManufacturers();

        var league = await _leagueService.GetLeagueById(Season.LeagueId);

        Model.LeagueId = league.Id;
        Model.HasContracting = league.Options.HasFlag(LeagueOptions.AllowContracting);
        allowPersonalNumbers = league.Options.HasFlag(LeagueOptions.PersonalNumbers);

        if (Season.RaceClasses?.Any() == true)
            Model.Classes.AddRange(Season.RaceClasses);

        _bread.SetBreadcrumbs(
        [
            new BreadcrumbItem("Home", href: ""),
            new BreadcrumbItem("Seasons", href: "seasons"),
            new BreadcrumbItem("Overview", href: $"overview/{Season.Id}"),
            new BreadcrumbItem("Wizard", href: $"entrantwizard/{Season.Id}"),
        ]);

        loading = false;
    }

    private Task<bool> InvalidStepPrevention(StepChangeDirection direction, int activeIndex)
    {
        var lastStep = (EntrantWizardStep)activeIndex - 1;

        errors.Clear();

        if (direction == StepChangeDirection.Backward)
        {
            return Task.FromResult(false);
        }

        loading = true;

        switch (lastStep)
        {
            case EntrantWizardStep.EnginesAdd:
                if (!(Model.BaseEngines?.Any() == true))
                {
                    errors.Add("Suggestion: add some goddamn engines to the season");
                }
                break;
            case EntrantWizardStep.EnginesEdit:
                {
                    if (Model.SeasonEngines.Count != 0)
                    {
                        foreach (var engine in Model.SeasonEngines)
                            engine.Validate(errors);
                    }
                    else
                    {
                        errors.Add("You fucked up");
                    }
                }
                break;
            case EntrantWizardStep.TeamsAdd:
                if (!(Model.BaseTeams?.Any() == true))
                {
                    errors.Add("Suggestion: add some goddamn teams to the season");
                }
                break;
            case EntrantWizardStep.TeamsEdit:
                {
                    if (Model.SeasonTeams.Count != 0)
                    {
                        foreach (var team in Model.SeasonTeams)
                            team.Validate(errors);
                    }
                    else
                    {
                        errors.Add("You fucked up");
                    }
                }
                break;
            case EntrantWizardStep.TeamsAssignment:
                if (Model.SeasonTeams.Any(e => e.BaseEngineId == 0))
                {
                    errors.Add("Oi oi mate, how is a team going to drive without an engine? Try again.");
                }
                break;
            case EntrantWizardStep.RaceClassAssignment:
                if (Model.Classes.Count != 0 && Model.SeasonTeams.Any(e => e.ClassId == 0))
                {
                    errors.Add("See these race classes? Yeah? Well, you better give every team one.");
                }
                break;
            case EntrantWizardStep.DriversAdd:
                if (!(Model.BaseDrivers?.Any() == true))
                {
                    errors.Add("Suggestion: add some goddamn drivers to the season");
                }
                break;
            case EntrantWizardStep.DriversEdit:
                {
                    if (Model.SeasonDrivers.Count != 0)
                    {
                        foreach (var driver in Model.SeasonDrivers)
                            driver.Validate(errors);
                    }
                    else
                    {
                        errors.Add("You fucked up");
                    }
                }
                break;
            case EntrantWizardStep.DriversAssignment:
                {
                    if (!allowPersonalNumbers)
                    {
                        int numberIndexer = 0;
                        foreach (var driver in Model.SeasonDrivers.OrderBy(e => e.BaseTeamId))
                            driver.Number = ++numberIndexer;
                    }
                }
                break;
        }

        loading = false;

        if (errors.Count != 0)
            return Task.FromResult(true);

        return Task.FromResult(false);
    }

    private async Task HandleStepChange(int activeIndex)
    {
        ActiveStep = (EntrantWizardStep)activeIndex;

        switch (ActiveStep)
        {
            case EntrantWizardStep.EnginesEdit:
                {
                    await SetEngineEditors();
                }
                break;
            case EntrantWizardStep.TeamsEdit:
                {
                    await SetTeamEditors();
                }
                break;
            case EntrantWizardStep.DriversEdit:
                {
                    await SetDriverEditors();
                }
                break;
            case EntrantWizardStep.DriversAssignment:
                {
                    if (!allowPersonalNumbers)
                    {
                        int numberIndexer = 0;
                        foreach (var driver in Model.SeasonDrivers.OrderBy(e => e.BaseTeamId))
                            driver.Number = ++numberIndexer;
                    }
                }
                break;
        }
    }

    private async Task SetEngineEditors()
    {
        Model.SeasonEngines.Clear();

        foreach (var engine in Model.BaseEngines.Where(e => !(Model.SeasonEngines.Select(se => se.EngineId).Contains(e.Id))))
        {
            var previousEngine = await _seasonEngineService.FindRecentSeasonEngine(engine.Id, Model.LeagueId);

            var model = new EditSeasonEngineModel(previousEngine);
            model.ResetIdentifierFields();
            model.SeasonId = SeasonId;
            model.EngineId = engine.Id;
            model.Name = string.IsNullOrEmpty(model.Name) ? engine.Name : model.Name;

            Model.SeasonEngines.Add(model);
        }

        Model.RemoveUnsetEngines();
    }

    private async Task SetTeamEditors()
    {
        Model.SeasonTeams.Clear();

        List<SeasonEngine>? previousEngines = null;
        Dictionary<string, long> alikeClasses = [];

        if (PreviousSeason != null)
        {
            var existingEngineIds = Model.SeasonEngines.Select(e => e.EngineId).ToArray();
            var allPrevious = await _seasonEngineService.GetSeasonEngines(PreviousSeason.Id);
            previousEngines = allPrevious.Where(e => existingEngineIds.Contains(e.EngineId)).ToList();

            if (PreviousSeason?.RaceClasses?.Any() == true)
            {
                foreach (var oldClass in PreviousSeason.RaceClasses)
                {
                    var similairClass = Model.Classes.Find(e => e.Tag.Equals(oldClass.Tag, StringComparison.OrdinalIgnoreCase));
                    if (similairClass != null)
                        alikeClasses.Add(oldClass.Tag, similairClass.Id);
                }
            }
        }

        foreach (var team in Model.BaseTeams.Where(e => !(Model.SeasonTeams.Select(st => st.TeamId).Contains(e.Id))))
        {
            var previousTeam = await _seasonTeamService.FindRecentSeasonTeam(team.Id, Season.LeagueId);

            var model = new EditSeasonTeamModel(previousTeam);
            model.ResetIdentifierFields();
            model.SeasonId = SeasonId;
            model.TeamId = team.Id;
            model.Team = team;
            model.Name = string.IsNullOrEmpty(model.Name) ? team.Name : model.Name;
            model.Principal = string.IsNullOrEmpty(model.Principal) ? "Principal" : model.Principal;
            model.ManufacturerId = model.ManufacturerId == 0 ? Manufacturers.First().Id : model.ManufacturerId;

            if (previousTeam != null && previousEngines != null)
                model.BaseEngineId = previousEngines.Find(e => e.Id == previousTeam.SeasonEngineId)?.EngineId ?? 0;

            if (previousTeam != null && previousTeam.ClassId.HasValue && PreviousSeason?.RaceClasses?.Any() == true)
            {
                var previousTag = PreviousSeason.RaceClasses.FirstOrDefault(e => e.Id == previousTeam.ClassId.Value)?.Tag ?? string.Empty;
                var similairTag = Model.Classes.Find(e => e.Tag.Equals(previousTag));

                model.ClassId = similairTag?.Id ?? 0;
            }

            Model.SeasonTeams.Add(model);
        }

        Model.RemoveUnsetTeams();
    }

    private async Task SetDriverEditors()
    {
        Model.SeasonDrivers.Clear();

        List<SeasonTeam>? previousTeams = null;

        if (PreviousSeason != null)
        {
            var existingTeamIds = Model.SeasonTeams.Select(e => e.TeamId).ToArray();
            var allPrevious = await _seasonTeamService.GetSeasonTeams(PreviousSeason.Id);

            previousTeams = allPrevious.Where(e => existingTeamIds.Contains(e.TeamId)).ToList();
        }

        int indexer = 0;
        foreach (var driver in Model.BaseDrivers.Where(e => !(Model.SeasonDrivers.Select(sd => sd.DriverId).Contains(e.Id))))
        {
            var previousDriver = await _seasonDriverService.FindRecentSeasonDriver(driver.Id);

            // Create a new edit model from the nullable entity, reset the identifying fields and assign the new ones
            var model = new EditSeasonDriverModel(previousDriver);
            model.ResetIdentifierFields();
            model.SeasonId = SeasonId;
            model.DriverId = driver.Id;
            model.Driver = driver;

            if (!allowPersonalNumbers)
                model.Number = ++indexer;

            if (Contracts.Count != 0)
            {
                var possibleContract = Contracts.FirstOrDefault(e => e.DriverId == driver.Id);
                if (possibleContract != null)
                {
                    model.BaseTeamId = possibleContract.TeamId;
                    model.Contracted = true;
                }
            }

            if (model.BaseTeamId == 0 && previousDriver != null && previousTeams != null)
                model.BaseTeamId = previousTeams.Find(e => e.Id == previousDriver.SeasonTeamId)?.TeamId ?? 0;

            // TODO: Above can be slimmed down, instead just auto-pick the last driver setup used here.
            model.PastSetups = await _seasonDriverService.PreviousDriverSetups(driver.Id);

            Model.SeasonDrivers.Add(model);
        }

        Model.RemoveUnsetDrivers();
    }

    private async Task AddAllEntrants()
    {
        var rootEntrants = Model.CombineEntrantsToRoot();

        await _seasonService.PersistSeasonEntrants(rootEntrants);

        _nav.NavigateTo($"/overview/{SeasonId}");
    }
}
