using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.PageModels.RaceWeek;
using SimTECH.Tests.Data.Factories;
using SimTECH.Tests.Extensions;
using SimTECH.Tests.Infrastructure;
using Xunit;
using static Bunit.ComponentParameterFactory;
using RaceComponent = SimTECH.Pages.RaceWeek.Tabs.Race;

namespace SimTECH.Tests.Components.Raceweek;

public class RaceTests : IClassFixture<DataFixture>
{
    private readonly DataFixture _dataFixture;

    public RaceTests(DataFixture dataFixture)
    {
        _dataFixture = dataFixture;
    }

    [Fact]
    public async Task InitShouldBuildAndPrepare()
    {
        // Arrange
        using var ctx = new TestContext();

        ctx.AddTestServices();

        // Not definitive, is example of how to pass a cascading parameter. Part of setup normally? Or some on-init thing for tests
        var tempModel = new RaweCeekModel()
        {
            Climate = ClimateFactory.ClimateDry,
            Race = new()
            {
                RaceLength = 200,
                Track = TrackFactory.GenerateTrack(),
            },
            RaweCeekDrivers =
            [
                new()
                {
                    SeasonDriverId = 1,
                    FirstName = "aaa",
                    LastName = "bbb",
                    Nationality = Common.Enums.Country.NL,
                    TeamName = "aaa",
                    Colour = Globals.DefaultColour,
                    Accent = Globals.DefaultAccent,
                    Tyre = TyreFactory.GenerateSoftTyre,
                },
                new()
                {
                    SeasonDriverId = 2,
                    FirstName = "aaa",
                    LastName = "bbb",
                    Nationality = Common.Enums.Country.NL,
                    TeamName = "aaa",
                    Colour = Globals.DefaultColour,
                    Accent = Globals.DefaultAccent,
                    Tyre = TyreFactory.GenerateSoftTyre,
                },
                new()
                {
                    SeasonDriverId = 3,
                    FirstName = "aaa",
                    LastName = "bbb",
                    Nationality = Common.Enums.Country.NL,
                    TeamName = "aaa",
                    Colour = Globals.DefaultColour,
                    Accent = Globals.DefaultAccent,
                    Tyre = TyreFactory.GenerateSoftTyre,
                },
                new()
                {
                    SeasonDriverId = 4,
                    FirstName = "aaa",
                    LastName = "bbb",
                    Nationality = Common.Enums.Country.NL,
                    TeamName = "aaa",
                    Colour = Globals.DefaultColour,
                    Accent = Globals.DefaultAccent,
                    Tyre = TyreFactory.GenerateSoftTyre,
                },
                new()
                {
                    SeasonDriverId = 5,
                    FirstName = "aaa",
                    LastName = "bbb",
                    Nationality = Common.Enums.Country.NL,
                    TeamName = "aaa",
                    Colour = Globals.DefaultColour,
                    Accent = Globals.DefaultAccent,
                    Tyre = TyreFactory.GenerateSoftTyre,
                },
            ],
            Season = new()
            {
                GridBonus = 10,
            },
            League = new(),
            GapMarge = 0.08,
        };

        var comp = ctx.RenderComponent<RaceComponent>(parameters => parameters
            .Add(p => p.Model, tempModel)
            .Add(p => p.Config, new SimConfig()));

        // Looks up the advanc ebutton in the respective component
        var advanceButton = comp.Find("button.advance-btn");

        // Act
        await advanceButton.ClickAsync(new MouseEventArgs());

        // Assert
    }

    [Fact]
    public async Task AdvanceShouldProgressRace()
    {
        // Arrange
        using var ctx = new TestContext();

        ctx.AddTestServices();

        // Not definitive, is example of how to pass a cascading parameter. Part of setup normally? Or some on-init thing for tests
        var placeholderModel = new RaweCeekModel()
        {
            Climate = ClimateFactory.ClimateDry,
            Race = new()
            {
                RaceLength = 200,
                Track = TrackFactory.GenerateTrack(),
            },
            RaweCeekDrivers =
            [
                new()
                {
                    SeasonDriverId = 1,
                    FirstName = "aaa",
                    LastName = "bbb",
                    Nationality = Common.Enums.Country.NL,
                    TeamName = "aaa",
                    Tyre = TyreFactory.GenerateSoftTyre,
                },
                new()
                {
                    SeasonDriverId = 2,
                    FirstName = "aaa",
                    LastName = "bbb",
                    Nationality = Common.Enums.Country.NL,
                    TeamName = "aaa",
                    Tyre = TyreFactory.GenerateSoftTyre,
                },
                new()
                {
                    SeasonDriverId = 3,
                    FirstName = "aaa",
                    LastName = "bbb",
                    Nationality = Common.Enums.Country.NL,
                    TeamName = "aaa",
                    Tyre = TyreFactory.GenerateSoftTyre,
                },
                new()
                {
                    SeasonDriverId = 4,
                    FirstName = "aaa",
                    LastName = "bbb",
                    Nationality = Common.Enums.Country.NL,
                    TeamName = "aaa",
                    Tyre = TyreFactory.GenerateSoftTyre,
                },
                new()
                {
                    SeasonDriverId = 5,
                    FirstName = "aaa",
                    LastName = "bbb",
                    Nationality = Common.Enums.Country.NL,
                    TeamName = "aaa",
                    Tyre = TyreFactory.GenerateSoftTyre,
                },
            ],
            Season = new()
            {
                GridBonus = 10,
            },
            League = new(),
            GapMarge = 0.08,
        };

        var placeholderOccurences = new List<RaceOccurrence>();

        // just add return type like Action<object> if it has one
        Action placeholderOnFinish = () => { };

        var comp = ctx.RenderComponent<RaceComponent>(parameters => parameters
            .Add(p => p.Model, placeholderModel)
            .Add(p => p.Config, new SimConfig())
            .Add(p => p.OnFinish, placeholderOnFinish));

        // Looks up the advanc ebutton in the respective component
        var advanceButton = comp.Find("button.advance-btn");

        // Act
        await advanceButton.ClickAsync(new MouseEventArgs());

        // Assert
    }
}
