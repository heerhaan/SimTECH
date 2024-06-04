using FluentAssertions;
using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.PageModels.RaceWeek;
using SimTECH.Pages.RaceWeek;
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

    [Fact]
    public void ShouldApplyPositionsOnLapSumAndApplyOvertakes()
    {
        // Arrange
        var season = new Season();
        var league = LeagueFactory.GenerateTestLeague;
        var incidents = IncidentFactory.GenerateTestIncidentList;
        var config = new SimConfig();

        league.BattleRng = 5;

        var manager = new RaceManager(season, league, incidents, config);

        var raceDrivers = new List<RaceDriver>()
        {
            new()
            {
                SeasonDriverId = 1,
                Status = Common.Enums.RaceStatus.Racing,
                AbsolutePosition = 5,
                Position = 5,
                Attack = 10,
                Defense = 0,
                InstantOvertaken = false,
                RecentMistake = false,
                LapScores = new()
                {
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 3,
                        Score = 50,
                    },
                },
            },
            new()
            {
                SeasonDriverId = 2,
                Status = Common.Enums.RaceStatus.Racing,
                AbsolutePosition = 4,
                Position = 4,
                Attack = 10,
                Defense = 0,
                InstantOvertaken = false,
                RecentMistake = false,
                LapScores = new()
                {
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 3,
                        Score = 40,
                    },
                },
            },
            new()
            {
                SeasonDriverId = 3,
                Status = Common.Enums.RaceStatus.Racing,
                AbsolutePosition = 3,
                Position = 3,
                Attack = 10,
                Defense = 0,
                InstantOvertaken = false,
                RecentMistake = false,
                LapScores = new()
                {
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 3,
                        Score = 30,
                    },
                },
            },
            new()
            {
                SeasonDriverId = 4,
                Status = Common.Enums.RaceStatus.Racing,
                AbsolutePosition = 2,
                Position = 2,
                Attack = 10,
                Defense = 0,
                InstantOvertaken = false,
                RecentMistake = false,
                LapScores = new()
                {
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 3,
                        Score = 20,
                    },
                },
            },
            new()
            {
                SeasonDriverId = 5,
                Status = Common.Enums.RaceStatus.Racing,
                AbsolutePosition = 1,
                Position = 1,
                Attack = 10,
                Defense = 0,
                InstantOvertaken = false,
                RecentMistake = false,
                LapScores = new()
                {
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 3,
                        Score = 10,
                    },
                },
            },
        };

        // Act
        manager.DeterminePositions(raceDrivers);

        // Assert
        var distinctPositionCount = raceDrivers.Select(e => e.AbsolutePosition).Distinct().Count();

        // Distinct amount of given positions should be the same as the amount of entered drivers
        raceDrivers.Count.Should().Be(distinctPositionCount);

        // Are all positions in the correct order as defined by the lapsum?
        int previousPosition = 0;
        foreach (var driver in raceDrivers.OrderByDescending(e => e.LapSum))
        {
            ++previousPosition;

            // No other classes exist, thus both positions should follow the same pattern
            driver.AbsolutePosition.Should().Be(previousPosition);
            driver.Position.Should().Be(previousPosition);
        }

        var lastToFirstDriver = raceDrivers.First(e => e.Position == 1);

        // 5 drivers in total, bloke started last and ended up first. Should have overtaken 4 drivers.
        lastToFirstDriver.OvertakeCount.Should().Be(4);
    }

    [Fact]
    public void HighDefensiveNumberShouldPreventOvertakes()
    {
        // Arrange
        var season = new Season();
        var league = LeagueFactory.GenerateTestLeague;
        var incidents = IncidentFactory.GenerateTestIncidentList;
        var config = new SimConfig();

        league.BattleRng = 2;

        var manager = new RaceManager(season, league, incidents, config);

        var raceDrivers = new List<RaceDriver>()
        {
            new()
            {
                SeasonDriverId = 1,
                Status = Common.Enums.RaceStatus.Racing,
                AbsolutePosition = 5,
                Position = 5,
                Attack = 0,
                Defense = 15,
                InstantOvertaken = false,
                RecentMistake = false,
                LapScores = new()
                {
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 3,
                        Score = 50,
                    },
                },
            },
            new()
            {
                SeasonDriverId = 2,
                Status = Common.Enums.RaceStatus.Racing,
                AbsolutePosition = 4,
                Position = 4,
                Attack = 0,
                Defense = 15,
                InstantOvertaken = false,
                RecentMistake = false,
                LapScores = new()
                {
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 3,
                        Score = 40,
                    },
                },
            },
            new()
            {
                SeasonDriverId = 3,
                Status = Common.Enums.RaceStatus.Racing,
                AbsolutePosition = 3,
                Position = 3,
                Attack = 0,
                Defense = 15,
                InstantOvertaken = false,
                RecentMistake = false,
                LapScores = new()
                {
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 3,
                        Score = 30,
                    },
                },
            },
            new()
            {
                SeasonDriverId = 4,
                Status = Common.Enums.RaceStatus.Racing,
                AbsolutePosition = 2,
                Position = 2,
                Attack = 0,
                Defense = 15,
                InstantOvertaken = false,
                RecentMistake = false,
                LapScores = new()
                {
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 3,
                        Score = 20,
                    },
                },
            },
            new()
            {
                SeasonDriverId = 5,
                Status = Common.Enums.RaceStatus.Racing,
                AbsolutePosition = 1,
                Position = 1,
                Attack = 0,
                Defense = 15,
                InstantOvertaken = false,
                RecentMistake = false,
                LapScores = new()
                {
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 3,
                        Score = 10,
                    },
                },
            },
        };

        // Act
        manager.DeterminePositions(raceDrivers);

        // Assert

        // Check also whether the prevented overtakes also decreased the lapscores
        int previousPosition = 0;
        foreach (var driver in raceDrivers.OrderByDescending(e => e.LapSum))
        {
            ++previousPosition;

            // No other classes exist, thus both positions should follow the same pattern
            driver.AbsolutePosition.Should().Be(previousPosition);
            driver.Position.Should().Be(previousPosition);
        }

        var leadDriver = raceDrivers.First(e => e.Position == 1);

        // Lead driver had the worst lapsum initially, check whether it's defense count has gone up
        leadDriver.DefensiveCount.Should().BeGreaterThan(0);
    }
}
