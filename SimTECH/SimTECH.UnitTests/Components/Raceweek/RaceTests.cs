using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using SimTECH.Data.Models;
using SimTECH.PageModels.RaceWeek;
using static Bunit.ComponentParameterFactory;
using RaceComponent = SimTECH.Pages.RaceWeek.Tabs.Race;

namespace SimTECH.UnitTests.Components.Raceweek;

[TestFixture]
public class RaceTests : BunitTest
{
    // [Setup]?

    [Test]
    public async Task InitShouldBuildAndPrepare()
    {
        // Arrange

        // Not definitive, is example of how to pass a cascading parameter. Part of setup normally? Or some on-init thing for tests
        var placeholderModel = new RaweCeekModel()
        {
            Climate = new(),
            Race = new()
            {
                Track = new(),
            },
            RaweCeekDrivers =
            [
                new()
                {

                },
                new()
                {

                },
            ],
            Season = new(),
        };
        var placeholderConfig = new SimConfig();

        // just add return type like Action<object> if it has one
        Action placeholderOnFinish = () => { };

        var comp = Context.RenderComponent<RaceComponent>(parameters => parameters
            .Add(p => p.Model, placeholderModel)
            .Add(p => p.Config, placeholderConfig)
            .Add(p => p.OnFinish, placeholderOnFinish));

        // Looks up the advanc ebutton in the respective component
        var advanceButton = comp.Find("button.advance-btn");

        // Act
        await advanceButton.ClickAsync(new MouseEventArgs());

        // Assert
    }

    [Test]
    public async Task AdvanceShouldProgressRace()
    {
        // Arrange

        // Not definitive, is example of how to pass a cascading parameter. Part of setup normally? Or some on-init thing for tests
        var placeholderModel = new RaweCeekModel();

        var placeholderOccurences = new List<RaceOccurrence>();

        // just add return type like Action<object> if it has one
        Action placeholderOnFinish = () => { };

        var comp = Context.RenderComponent<RaceComponent>(parameters => parameters
            .Add(p => p.Model, placeholderModel)
            .Add(p => p.OnFinish, placeholderOnFinish));

        // Looks up the advanc ebutton in the respective component
        var advanceButton = comp.Find("button.advance-btn");

        // Act
        await advanceButton.ClickAsync(new MouseEventArgs());

        // Assert
    }
}
