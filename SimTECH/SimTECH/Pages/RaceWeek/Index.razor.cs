using Microsoft.AspNetCore.Components;
using MudBlazor;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.Extensions;
using SimTECH.PageModels.Common;
using SimTECH.PageModels.RaceWeek;
using SimTECH.Pages.RaceWeek.Dialogs;

namespace SimTECH.Pages.RaceWeek;

public partial class Index
{
    [Parameter]
    public long RaceId { get; set; }

    public RaweCeekModel Model { get; set; } = new();

    private List<Manufacturer> TyreSuppliers { get; set; } = [];
    private List<Trait> TrackTraits { get; set; } = [];
    private List<Tyre> Tyres { get; set; } = [];

    private List<PracticeSession> PracticeSessions { get; set; } = [];
    private List<QualifyingSession> QualySessions { get; set; } = [];

    private List<long> ConsumablePenalties { get; set; } = [];

    private SimConfig config;

    private bool Loading { get; set; } = true;
    private bool PracticeLoaded { get; set; }
    private bool QualifyingLoaded { get; set; }

    private bool IsRaceDisabled => Model.Race.State is not State.Advanced and not State.Closed;
    private bool IsPostRaceDisabled => Model.Race.State is not State.Closed;

    protected override async Task OnInitializedAsync()
    {
        Tyres = await _tyreService.GetTyres(StateFilter.All);

        config = _config.Value;

        var race = await _raceService.GetRaceById(RaceId);
        if (race.State == State.Concept)
        {
            var idNextRace = await _raceService.GetNextRaceIdOfSeason(race.SeasonId);
            if (idNextRace.GetValueOrDefault() != race.Id)
                throw new InvalidOperationException("Somehow you landed inside a raceweek which is not valid for activation, dumbass");

            await _raceService.ActivateRace(race.Id);
            race = await _raceService.GetRaceById(RaceId);
        }

        Model.Race = race;
        Model.Season = await _seasonService.GetSeasonById(race.SeasonId);
        Model.League = await _leagueService.GetLeagueById(Model.Season.LeagueId);
        Model.Climate = await _climateService.GetClimateById(race.ClimateId);
        Model.GapMarge = config.GapMarge;

        _bread.Reset();
        _bread.SetBreadcrumbs(
        [
            new BreadcrumbItem("Home", href: ""),
            new BreadcrumbItem("Seasons", href: "seasons"),
            new BreadcrumbItem("Overview", href: $"overview/{Model.Season.Id}"),
            new BreadcrumbItem("Raceweek", href: $"raceweek/{RaceId}", disabled: true),
        ]);

        await LoadDrivers();

        Loading = false;
    }

    private async Task LoadDrivers()
    {
        var race = Model.Race;
        var climate = Model.Climate;

        var seasonDrivers = await _seasonDriverService.GetSeasonDrivers(race.SeasonId);
        var seasonTeams = await _seasonTeamService.GetSeasonTeams(race.SeasonId);
        var seasonEngines = await _seasonEngineService.GetSeasonEngines(race.SeasonId);
        var manufacturers = await _manufacturerService.GetManufacturers(StateFilter.All);
        var raceResults = await _raceWeekService.GetResultsOfRace(RaceId);
        var allTraits = await _traitService.GetTraits();
        var incidents = await _incidentService.GetIncidents(StateFilter.All);

        var traits = allTraits.Where(e => !e.ForWetConditions || e.ForWetConditions == climate.IsWet).ToList();

        var trackTraitIds = race.Track.TrackTraits?.Select(e => e.TraitId).ToList() ?? [];
        TrackTraits = traits.Where(e => trackTraitIds.Contains(e.Id)).ToList();

        var raweceekDrivers = new List<RaweCeekDriver>();
        foreach (var result in raceResults)
        {
            var driverTraits = new List<Trait>(TrackTraits);

            var driver = seasonDrivers.First(e => e.Id == result.SeasonDriverId);
            var team = seasonTeams.First(e => e.Id == result.SeasonTeamId);

            var engine = seasonEngines.First(e => e.Id == team.SeasonEngineId);
            var manufacturer = manufacturers.First(e => e.Id == team.ManufacturerId);

            if (driver.Driver.DriverTraits?.Any() == true)
                driverTraits.AddRange(traits.Where(e => driver.Driver.DriverTraits.Select(dt => dt.TraitId).Contains(e.Id)));
            if (team.Team.TeamTraits?.Any() == true)
                driverTraits.AddRange(traits.Where(e => team.Team.TeamTraits.Select(dt => dt.TraitId).Contains(e.Id)));

            var driverPower = driver.Skill + driver.RetrieveStatusBonus(Model.League.DriverStatusPaceModifier);
            var carPower = team.BaseValue + team.ModifierApplication(race.Track);
            var enginePower = (engine.Power * climate.EngineMultiplier).RoundDouble();
            var totalPower = driverPower + carPower + enginePower;

            var actualDefense = ((driver.Defense + driverTraits.Sum(e => e.Defense)) * race.Track?.DefenseMod ?? 1.0).RoundDouble();

            var raweDriver = new RaweCeekDriver
            {
                SeasonDriverId = driver.Id,
                FirstName = driver.Driver.FirstName,
                LastName = driver.Driver.LastName,
                Nationality = driver.Driver.Country,
                Number = driver.Number,
                Role = driver.TeamRole,

                SeasonTeamId = team.Id,
                TeamName = team.Name,
                Colour = team.Colour,
                Accent = team.Accent,
                Parts = new CarParts(team.Aero, team.Chassis, team.Powertrain),

                ManufacturerId = manufacturer.Id,
                ManufacturerName = manufacturer.Name,
                ManufacturerColour = manufacturer.Colour,
                ManufacturerAccent = manufacturer.Accent,

                QualyPower = totalPower + driverTraits.Sum(e => e.QualifyingPace),
                RacePower = totalPower + driverTraits.Sum(e => e.RacePace),
                Attack = driver.Attack + driverTraits.Sum(e => e.Attack),
                Defense = actualDefense,
                DriverReliability = driver.Reliability + driverTraits.Sum(e => e.DriverReliability) + climate.ReliablityModifier,
                CarReliability = team.Reliability + driverTraits.Sum(e => e.CarReliability),
                EngineReliability = engine.Reliability + driverTraits.Sum(e => e.EngineReliability),
                WearMinMod = driverTraits.Sum(e => e.WearMin),
                WearMaxMod = driverTraits.Sum(e => e.WearMax),
                RngMinMod = driverTraits.Sum(e => e.RngMin) - climate.RngModifier,
                RngMaxMod = driverTraits.Sum(e => e.RngMax) + climate.RngModifier,

                ResultId = result.Id,
                Grid = result.Grid,
                Position = result.Position,
                AbsoluteGrid = result.AbsoluteGrid,
                AbsolutePosition = result.AbsolutePosition,
                Status = result.Status,
                Setup = result.Setup,
                TyreLife = result.TyreLife,
                FastestLap = result.FastestLap,
                Overtaken = result.Overtaken,
                Defended = result.Defended,

                TraitNames = driverTraits.Where(e => e.Type != Entrant.Track).Select(e => e.Name).ToList(),

                Engine = engine.Engine,
                Incident = incidents.FirstOrDefault(e => e.Id == result.IncidentId.GetValueOrDefault()),
                Tyre = Tyres.First(e => e.Id == result.TyreId),
            };

            if (Model.Season.HasRaceClasses)
            {
                raweDriver.ClassId = team.ClassId.GetValueOrDefault();
                raweDriver.Class = Model.Season.RaceClasses.FirstOrDefault(e => e.Id == team.ClassId.GetValueOrDefault());
            }

            if (!climate.IsWet)
            {
                // Adds on to the existing tyre
                // TODO: Should probably be added when activating instead
                raweDriver.TyreLife += manufacturer.Pace;

                raweDriver.LifeBonus = manufacturer.Pace;
                raweDriver.WearMinMod += manufacturer.WearMin;
                raweDriver.WearMaxMod += manufacturer.WearMax;
            }

            raweceekDrivers.Add(raweDriver);
        }

        var penalties = await _raceWeekService.GetRacePenalties(RaceId);

        foreach (var penalty in penalties.GroupBy(e => e.SeasonDriverId))
        {
            var matchingDriver = raweceekDrivers.FirstOrDefault(e => e.SeasonDriverId == penalty.Key);

            if (matchingDriver != null)
                matchingDriver.Penalty = penalty.Sum(e => e.Incident.Punishment);
        }

        foreach (var driverGroup in raweceekDrivers.GroupBy(e => e.ClassId))
        {
            int indexer = 0;
            foreach (var driver in driverGroup.OrderByDescending(e => e.RelativePower))
                driver.ExpectedPosition = ++indexer;
        }

        TyreSuppliers = manufacturers
            .Where(e => raweceekDrivers.Select(d => d.ManufacturerId).Contains(e.Id))
            .ToList();

        Model.RaweCeekDrivers = raweceekDrivers;
    }

    private async Task OnOpenPractice()
    {
        if (PracticeLoaded)
            return;

        var practiceScores = await _raceWeekService.GetPracticeScores(RaceId);

        foreach (var groupedPracticeScores in practiceScores.GroupBy(e => e.Index))
        {
            PracticeSessions.Add(new PracticeSession
            {
                SessionIndex = groupedPracticeScores.Key,
                IsFinished = true,
                SessionScores = groupedPracticeScores.ToList(),
            });
        }

        if (Model.Race.State != State.Advanced && Model.Race.State != State.Closed)
            AddNewPracticeSession();

        PracticeLoaded = true;
    }

    private async Task OnOpenQualifying()
    {
        if (QualifyingLoaded)
            return;

        var qualyScores = await _raceWeekService.GetQualifyingScores(RaceId);
        var qSessionCount = Model.Season.QualifyingFormat.SessionCount();
        var previousSessionIsFinished = true;

        for (int i = 1; i <= qSessionCount; i++)
        {
            var currentScores = qualyScores.Where(e => e.Index == i).ToList();

            var qSession = new QualifyingSession
            {
                SessionIndex = i,
                IsDisabled = !previousSessionIsFinished,
                IsFinished = currentScores.Count != 0,
                SessionScores = currentScores,
            };

            previousSessionIsFinished = qSession.IsFinished;

            // TODO: Er is hier vast een betere oplossing voor
            if (Model.Season.QualifyingFormat == QualyFormat.TripleEliminate)
            {
                if (qSession.SessionIndex == 1)
                {
                    qSession.ProgressionCutoff = Model.Season.QualifyingAmountQ2;
                }
                else if (qSession.SessionIndex == 2)
                {
                    qSession.AllowedEntries = Model.Season.QualifyingAmountQ2;
                    qSession.ProgressionCutoff = Model.Season.QualifyingAmountQ3;
                }
                else if (qSession.SessionIndex == 3)
                {
                    qSession.AllowedEntries = Model.Season.QualifyingAmountQ3;
                }
            }

            QualySessions.Add(qSession);
        }

        QualifyingLoaded = true;
    }

    private async Task PickRaceTyre(long resultId)
    {
        var availableTyres = Tyres
            .Where(e => e.State == State.Active
                && (e.LeagueTyres?.Any(e => e.LeagueId == Model.Season.LeagueId) ?? false))
            .ToList();

        var parameters = new DialogParameters
        {
            ["ResultId"] = resultId,
            ["Tyres"] = availableTyres,
        };

        var dialog = await _dialogService.ShowAsync<TyrePicker>("Set tyre", parameters);
        var result = await dialog.Result;

        if (!result.Canceled && result.Data != null && result.Data is Tyre pickedTyre)
        {
            await _raceWeekService.PickTyre(resultId, pickedTyre);

            // Assign the picked tyre and ofc dont forget the life bonus
            var driver = Model.RaweCeekDrivers.First(e => e.ResultId == resultId);
            driver.Tyre = pickedTyre;
            driver.TyreLife = pickedTyre.Pace + driver.LifeBonus;
        }
    }

    private void AddNewPracticeSession()
    {
        var nextSession = PracticeSessions.Count + 1;
        PracticeSessions.Add(new PracticeSession
        {
            SessionIndex = nextSession,
        });
    }

    private async Task PersistPractice(int practiceIndex)
    {
        var practiceSession = PracticeSessions.First(e => e.SessionIndex == practiceIndex);
        var gridResults = practiceSession.SessionScores.ToDictionary(e => e.ResultId, e => e.Position);

        await _raceWeekService.PersistPracticeScores(practiceSession.SessionScores);

        practiceSession.IsFinished = true;

        // Update the new grid and position
        foreach (var res in gridResults)
        {
            var matchingDriver = Model.RaweCeekDrivers.First(e => e.ResultId == res.Key);
            matchingDriver.Grid = res.Value;
            matchingDriver.Position = res.Value;
        }

        AddNewPracticeSession();
    }

    private async Task PersistQualifying(int qualyIndex)
    {
        var qSessionCount = Model.Season.QualifyingFormat.SessionCount();
        var qualySession = QualySessions.First(e => e.SessionIndex == qualyIndex);

        if (qualySession.SessionScores.Count == 0)
            throw new InvalidOperationException("No score data to persist");

        qualySession.IsFinished = true;

        if (qSessionCount == qualyIndex)
        {
            var finalQualyPosition = new Dictionary<long, (int, int)>();
            var qualySessionScores = QualySessions.SelectMany(e => e.SessionScores).ToList();

            var resultsByRaceClass = Model.RaweCeekDrivers
                .GroupBy(e => e.ClassId)
                .ToDictionary(
                    e => e.Key,
                    e => e.Select(rd => rd.ResultId).ToArray());

            foreach (var seasonGroup in resultsByRaceClass)
            {
                int positionIndexer = 0;
                var gridResults = QualySessions
                    .SelectMany(e => e.SessionScores)
                    .Where(e => seasonGroup.Value.Contains(e.ResultId))
                    .GroupBy(e => e.ResultId)
                    .Select(e => e.MaxBy(r => r.Index))
                    .ToList();

                foreach (var penaltyResult in gridResults.OrderBy(e => e!.PenaltyPosition()))
                    finalQualyPosition.Add(penaltyResult!.ResultId, (++positionIndexer, penaltyResult.AbsolutePosition));
            }

            await _raceWeekService.FinishQualifying(
                qualySession.SessionScores, finalQualyPosition, RaceId, Model.Season.MaximumDriversInRace);

            // Update the new grid, position and status
            foreach (var res in finalQualyPosition)
            {
                var matchingDriver = Model.RaweCeekDrivers.First(e => e.ResultId == res.Key);
                //var sessionScore = qualySessionScores.First(e => e.ResultId == res.Key);

                matchingDriver.Grid = res.Value.Item1;
                matchingDriver.Position = res.Value.Item1;
                matchingDriver.AbsoluteGrid = res.Value.Item2;
                matchingDriver.AbsolutePosition = res.Value.Item2;
                matchingDriver.Status = res.Value.Item2 > Model.Season.MaximumDriversInRace ? RaceStatus.Dnq : RaceStatus.Racing;
            }

            Model.Race.State = State.Advanced;
        }
        else
        {
            await _raceWeekService.PersistQualifyingScores(qualySession.SessionScores);

            // Update the new grid and position
            foreach (var res in qualySession.SessionScores)
            {
                var matchingDriver = Model.RaweCeekDrivers.First(e => e.ResultId == res.ResultId);
                matchingDriver.Grid = res.Position;
                matchingDriver.Position = res.Position;
                matchingDriver.AbsoluteGrid = res.AbsolutePosition;
                matchingDriver.AbsolutePosition = res.AbsolutePosition;
            }

            var nextSession = QualySessions.First(e => e.SessionIndex == (1 + qualyIndex));
            nextSession.IsDisabled = false;
        }

        if (qualyIndex == 1 && PracticeLoaded && PracticeSessions.Any())
        {
            // Removes the last added practice session
            var latestPracticeSession = PracticeSessions.MaxBy(e => e.SessionIndex);
            if (latestPracticeSession != null)
                PracticeSessions.Remove(latestPracticeSession);
        }
    }

    private void HandleRaceFinished()
    {
        Model.Race.State = State.Closed;
    }
}
