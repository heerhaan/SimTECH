using Microsoft.AspNetCore.Components;
using MudBlazor;
using SimTECH.Common.Enums;
using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels;
using SimTECH.PageModels.Seasons.Development;
using SimTECH.Pages.Seasons.Overview.Development.Dialogs;

namespace SimTECH.Pages.Seasons.Overview.Development;

public partial class Index
{
    [Parameter]
    public long SeasonId { get; set; }

    public DevelopModel DevelopModel { get; set; } = new();
    public List<DevelopedEntrant> DevelopedEntrants { get; set; } = new();
    public List<RaceClass>? RaceClasses { get; set; }

    private List<SeasonEngine> SeasonEngines { get; set; }
    private List<SeasonTeam> SeasonTeams { get; set; }
    private List<SeasonDriver> SeasonDrivers { get; set; }
    private List<DevelopmentRange> LeagueDevelopmentRanges { get; set; }

    private bool Loading { get; set; } = true;
    private bool HasDeveloped { get; set; }

    private int currentYear;
    private int currentMinRange = 0;
    private int currentMaxRange = 0;
    private int lastCompletedRound = 0;

    protected override async Task OnInitializedAsync()
    {
        _bread.SetBreadcrumbs(
        [
            new BreadcrumbItem("Home", href: ""),
            new BreadcrumbItem("Seasons", href: "seasons"),
            new BreadcrumbItem("Overview", href: $"overview/{SeasonId}"),
            new BreadcrumbItem("Development", href: $"developer/{SeasonId}", disabled: true),
        ]);

        SeasonEngines = await _seasonEngineService.GetSeasonEngines(SeasonId);
        SeasonTeams = await _seasonTeamService.GetSeasonTeams(SeasonId);
        SeasonDrivers = await _seasonDriverService.GetSeasonDrivers(SeasonId);

        var season = await _seasonService.GetSeasonById(SeasonId);

        if (season.HasRaceClasses)
            RaceClasses = season.RaceClasses.ToList();

        currentYear = season.Year;
        LeagueDevelopmentRanges = await _leagueService.GetLeagueDevelopmentRanges(season.LeagueId);

        var races = await _raceService.GetRacesBySeason(SeasonId);
        if (races.Any(e => e.DateFinished != null))
            lastCompletedRound = races.Where(e => e.DateFinished != null)
                .OrderByDescending(e => e.Round)
                .FirstOrDefault()?.Round
                ?? 0;

        LoadEntrants();

        Loading = false;
    }

    private void LoadEntrants()
    {
        HasDeveloped = false;

        switch (DevelopModel.ActiveEntrant)
        {
            case Entrant.Driver:
                DevelopDrivers();
                break;
            case Entrant.Team:
                DevelopTeams();
                break;
            case Entrant.Engine:
                DevelopEngines();
                break;
            default:
                throw new InvalidOperationException("wtf unexpected entrant added to develop? (how)");
        }

        if (DevelopModel.ActiveAspect == Aspect.Reliability)
        {
            foreach (var entrant in DevelopedEntrants)
                entrant.ValueMaximumLimit = 1000;
        }
    }

    private void DevelopDrivers()
    {
        var allowedTeamIds = SeasonTeams
            .Where(e => DevelopModel.ActiveRaceClassId == default || e.ClassId == DevelopModel.ActiveRaceClassId)
            .Select(e => e.Id)
            .ToList();

        DevelopedEntrants = SeasonDrivers
            .Where(e => e.SeasonTeamId.HasValue && allowedTeamIds.Contains(e.SeasonTeamId.Value))
            .Select(e => new DevelopedEntrant
            {
                Id = e.Id,
                Name = e.Driver.FullName,
                Nationality = e.Driver.Country,
                Optional = currentYear - e.Driver.DateOfBirth.Year,
                Old = e.RetrieveAspectValue(DevelopModel.ActiveAspect),
                Mark = e.Driver.Mark
            })
            .ToList();

        DevelopModel.IsOptionalColumnVisible = true;
    }

    private void DevelopTeams()
    {
        DevelopedEntrants = SeasonTeams
            .Where(e => DevelopModel.ActiveRaceClassId == default || e.ClassId == DevelopModel.ActiveRaceClassId)
            .Select(e => new DevelopedEntrant
            {
                Id = e.Id,
                Name = e.Name,
                Nationality = e.Team.Country,
                Optional = null,
                Old = e.RetrieveAspectValue(DevelopModel.ActiveAspect),
                Mark = e.Team.Mark
            })
            .ToList();

        DevelopModel.IsOptionalColumnVisible = false;
    }

    private void DevelopEngines()
    {
        var allowedEngineIds = SeasonTeams
            .Where(e => DevelopModel.ActiveRaceClassId == default || e.ClassId == DevelopModel.ActiveRaceClassId)
            .Select(e => e.SeasonEngineId)
            .Distinct()
            .ToList();

        DevelopedEntrants = SeasonEngines
            .Where(e => allowedEngineIds.Contains(e.Id))
            .Select(e => new DevelopedEntrant
            {
                Id = e.Id,
                Name = e.Name,
                Nationality = null,
                Optional = null,
                Old = e.RetrieveAspectValue(DevelopModel.ActiveAspect),
            })
            .ToList();

        DevelopModel.IsOptionalColumnVisible = false;
    }

    private void ChipChanged(bool reloadEntrants)
    {
        if (reloadEntrants)
            LoadEntrants();

        ApplyCommon();
    }

    private void RaceClassChipChanged(MudChip? classChip)
    {
        if (classChip == null)
            return;

        DevelopModel.ActiveRaceClassId = (long)classChip.Value;

        ChipChanged(true);
    }

    private void EntrantChipChanged(MudChip? entrantChip)
    {
        if (entrantChip == null)
            return;

        DevelopModel.ActiveAspect = Aspect.Reliability;
        DevelopModel.ActiveEntrant = (Entrant)entrantChip.Value;

        ChipChanged(true);
    }

    private void TargetChipChanged(MudChip? devChip)
    {
        if (devChip == null)
            return;

        DevelopModel.ActiveAspect = (Aspect)devChip.Value;

        ChipChanged(true);
    }

    private void TypeChipChanged(MudChip? typeChip)
    {
        if (typeChip == null)
            return;

        DevelopModel.ActiveQuantifier = (Quantifier)typeChip.Value;

        ChipChanged(false);
    }

    private void ApplyCommon()
    {
        if (DevelopModel.ActiveQuantifier == Quantifier.Set)
        {
            ApplyDefaultRanges();
        }
        else if (DevelopModel.ActiveQuantifier == Quantifier.Range && currentMinRange != currentMaxRange)
        {
            ApplyMinRange(currentMinRange);
            ApplyMaxRange(currentMaxRange);
        }
    }

    private void ApplyDefaultRanges()
    {
        List<DevelopmentRange> setCompareRange = LeagueDevelopmentRanges.GetAspectRanges(DevelopModel.ActiveAspect);
        List<DevelopmentRange>? additionalCompareRange = LeagueDevelopmentRanges.GetAspectRanges(Aspect.Age);

        // I know, I know this is awful
        if (DevelopModel.ActiveEntrant != Entrant.Driver || DevelopModel.ActiveAspect != Aspect.Skill)
            additionalCompareRange = null;

        if (setCompareRange.Count == 0 && !(additionalCompareRange?.Any() == true))
        {
            // Fallback minimum and maximum with the defaults
            foreach (var entrant in DevelopedEntrants)
            {
                entrant.Min = -2;
                entrant.Max = 2;
            }

            return;
        }

        foreach (var entrant in DevelopedEntrants)
        {
            int min = 0;
            int max = 0;

            var matchRange = setCompareRange.FirstOrDefault(e => e.Comparer >= entrant.Old);

            matchRange ??= setCompareRange[^1];

            min = matchRange.Minimum;
            max = matchRange.Maximum;

            if (additionalCompareRange?.Any() == true && entrant.Optional.HasValue)
            {
                var optRange = additionalCompareRange.FirstOrDefault(e => e.Comparer >= entrant.Optional.Value);

                if (optRange != null)
                {
                    min += optRange.Minimum;
                    max += optRange.Maximum;
                }
            }

            entrant.Min = min;
            entrant.Max = max;
        }
    }

    private void ApplyMinRange(int newMin)
    {
        currentMinRange = newMin;

        foreach (var entrant in DevelopedEntrants)
            entrant.Min = newMin;
    }

    private void ApplyMaxRange(int newMax)
    {
        currentMaxRange = newMax;

        foreach (var entrant in DevelopedEntrants)
            entrant.Max = newMax;
    }

    private void RunDevelop()
    {
        foreach (var entrant in DevelopedEntrants)
        {
            if (entrant.Min > entrant.Max)
            {
                _snack.Add("oi you cunt, a minimum should be less than the maximum. you better fix that shit first", Severity.Error);
                return;
            }

            entrant.Change = NumberHelper.RandomInt(entrant.Min, entrant.Max);
            entrant.New = entrant.Old + entrant.Change;

            // Sets the minimum and maximum value limit if those are set
            if (entrant.New < entrant.ValueMinimumLimit)
                entrant.New = entrant.ValueMinimumLimit;
            else if (entrant.New > entrant.ValueMaximumLimit)
                entrant.New = entrant.ValueMaximumLimit;

            // Re-apply the change one more time in the case a min/max-limit was set
            entrant.Change = entrant.New - entrant.Old;
        }

        var allChanges = DevelopedEntrants.Select(e => e.Change).Where(e => e != 0).ToArray();

        if (allChanges.Any())
        {
            DevelopModel.MinChange = allChanges.Min();
            DevelopModel.MaxChange = allChanges.Max();

            HasDeveloped = true;
        }
    }

    private async Task SaveDevelopment()
    {
        var developedValues = DevelopedEntrants.ToDictionary(e => e.Id, e => e.New);

        switch (DevelopModel.ActiveEntrant)
        {
            case Entrant.Driver:
                await _seasonDriverService.SaveDriverDevelopment(developedValues, DevelopModel.ActiveAspect);
                SeasonDrivers = await _seasonDriverService.GetSeasonDrivers(SeasonId);
                break;
            case Entrant.Team:
                await _seasonTeamService.SaveTeamDevelopment(developedValues, DevelopModel.ActiveAspect);
                SeasonTeams = await _seasonTeamService.GetSeasonTeams(SeasonId);
                break;
            case Entrant.Engine:
                await _seasonEngineService.SaveEngineDevelopment(developedValues, DevelopModel.ActiveAspect);
                SeasonEngines = await _seasonEngineService.GetSeasonEngines(SeasonId);
                break;
        }

        var developLogs = DevelopedEntrants.Select(e =>
            new DevelopmentLog
            {
                EntrantId = e.Id,
                EntrantGroup = DevelopModel.ActiveEntrant,
                DevelopedAspect = DevelopModel.ActiveAspect,
                Initial = e.Old,
                Change = e.Change,
                AfterRound = lastCompletedRound,
                SeasonId = SeasonId,
            })
            .ToList();

        await _seasonService.SaveDevelopmentLog(developLogs);

        LoadEntrants();
    }

    private async Task ShowWeightedDevelopmentRanges()
    {
        var aspectRanges = new Dictionary<Aspect, List<DevelopmentRange>>();
        var activeRanges = LeagueDevelopmentRanges.GetAspectRanges(DevelopModel.ActiveAspect);
        var ageRanges = LeagueDevelopmentRanges.GetAspectRanges(Aspect.Age);

        // TODO: Currently there is no easy way to get all the development ranges meant for a certain entrant

        if (activeRanges?.Any() == true)
            aspectRanges.Add(DevelopModel.ActiveAspect, activeRanges);

        if (DevelopModel.ActiveEntrant == Entrant.Driver && ageRanges?.Any() == true)
            aspectRanges.Add(Aspect.Age, ageRanges);

        var parameters = new DialogParameters
        {
            ["ForEntrant"] = DevelopModel.ActiveEntrant,
            ["AspectRanges"] = aspectRanges
        };

        await _dialogService.ShowAsync<WeightedDevelopRanges>("Weighted development ranges", parameters, Globals.StatisticDialogDefaultOptionsXl);
    }

    private async Task ShowDevelopmentSummary()
    {
        var developmentLog = await _seasonService.GetDevelopmentLog(SeasonId);

        var parameters = new DialogParameters
        {
            ["DevelopmentLog"] = developmentLog,
            ["SeasonDrivers"] = SeasonDrivers,
            ["SeasonTeams"] = SeasonTeams,
            ["SeasonEngines"] = SeasonEngines
        };

        await _dialogService.ShowAsync<SummaryDevelopment>("Summary development", parameters, Globals.StatisticDialogDefaultOptionsXl);
    }

    private async Task ShowDevelopmentProgressionChart()
    {
        List<ChartData> developData;

        switch (DevelopModel.ActiveEntrant)
        {
            case Entrant.Driver:
                developData = await _seasonDriverService.GetSeasonDevelopmentProgressionData(SeasonId, DevelopModel.ActiveAspect);
                break;
            case Entrant.Team:
                developData = await _seasonTeamService.GetSeasonDevelopmentProgressionData(SeasonId, DevelopModel.ActiveAspect);
                break;
            case Entrant.Engine:
                developData = await _seasonEngineService.GetSeasonDevelopmentProgressionData(SeasonId, DevelopModel.ActiveAspect);
                break;
            default:
                return;
        }

        var parameters = new DialogParameters()
        {
            ["DataSets"] = developData,
            ["Aspect"] = DevelopModel.ActiveAspect,
        };

        await _dialogService.ShowAsync<SeasonDevelopChartDialog>("Season aspect development", parameters, Globals.StatisticDialogDefaultOptionsXl);
    }
}
