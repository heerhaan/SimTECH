using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.Managers;
using SimTECH.PageModels.RaceWeek;
using SimTECH.UnitTests.Data.Factories;
using Xunit;

namespace SimTECH.UnitTests.Managers;

public class RaceManagerTests
{
    [Fact]
    public void CanAddFormationLapToDrivers()
    {
        var season = new Season()
        {
            GridBonus = 3,
        };
        var league = LeagueFactory.GenerateTestLeague;
        var incidents = IncidentFactory.GenerateTestIncidentList;
        var config = new SimConfig();

        var manager = new RaceManager(season, league, incidents, config);

        var raceDrivers = new List<RaceDriver>()
        {
            new()
            {
                SeasonDriverId = 1,
                FirstName = "aaa",
                LastName = "bbb",
                Nationality = Common.Enums.Country.NL,
                TeamName = "aaa",
                Colour = Globals.DefaultColour,
                Accent = Globals.DefaultAccent,
                CurrentTyre = TyreFactory.GenerateSoftTyre,
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
                CurrentTyre = TyreFactory.GenerateSoftTyre,
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
                CurrentTyre = TyreFactory.GenerateSoftTyre,
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
                CurrentTyre = TyreFactory.GenerateSoftTyre,
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
                CurrentTyre = TyreFactory.GenerateSoftTyre,
            },
        };

        raceDrivers.TrueForAll(e => e.LapScores.Count == 0);

        // Act
        manager.AddFormationLap(raceDrivers);

        // Assert
        raceDrivers.TrueForAll(e => e.LapScores.Count == 1);
    }
}
