using FluentAssertions;
using SimTECH.Common.Enums;
using SimTECH.Constants;
using SimTECH.Data.Models;
using SimTECH.PageModels.RaceWeek;
using SimTECH.Pages.RaceWeek;
using SimTECH.Tests.Data.Factories;
using Xunit;

namespace SimTECH.Tests.UnitTests;

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

        var situation = SituationOccurrence.Raced;

        // Act
        manager.DeterminePositions(raceDrivers, situation);

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
    public void ShouldHigherDefensePreventOvertake()
    {
        // Arrange
        var season = new Season();
        var league = LeagueFactory.GenerateTestLeague;
        var incidents = IncidentFactory.GenerateTestIncidentList;
        var config = new SimConfig();

        league.BattleRng = 2;

        var manager = new RaceManager(season, league, incidents, config);

        var leadDriver = new RaceDriver()
        {
            SeasonDriverId = 2,
            Status = Common.Enums.RaceStatus.Racing,
            AbsolutePosition = 1,
            Position = 1,
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
                },
        };

        var followingDriver = new RaceDriver()
        {
            SeasonDriverId = 1,
            Status = Common.Enums.RaceStatus.Racing,
            AbsolutePosition = 2,
            Position = 2,
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
                        Score = 20,
                    },
                },
        };

        var raceDrivers = new List<RaceDriver>() { leadDriver, followingDriver };

        var situation = SituationOccurrence.Raced;

        // Act
        manager.DeterminePositions(raceDrivers, situation);

        // Assert

        // Ordering by the lap sums also ensures that we can check the lapscores
        int expectedPosition = 0;
        foreach (var driver in raceDrivers.OrderByDescending(e => e.LapSum))
        {
            ++expectedPosition;

            // No other classes exist, thus both positions should follow the same pattern
            driver.AbsolutePosition.Should().Be(expectedPosition);
            driver.Position.Should().Be(expectedPosition);
        }

        // Lead driver had the worst lapsum initially, check whether it's defense count has gone up
        leadDriver.DefensiveCount.Should().Be(1);
    }

    [Fact]
    public void DefendingSupportDriverShouldLetMainOvertake()
    {
        // Arrange
        var season = new Season();
        var league = LeagueFactory.GenerateTestLeague;
        var incidents = IncidentFactory.GenerateTestIncidentList;
        var config = new SimConfig();

        league.BattleRng = 2;

        var manager = new RaceManager(season, league, incidents, config);

        var supportDriver = new RaceDriver()
        {
            SeasonDriverId = 1,
            SeasonTeamId = 1,
            Status = RaceStatus.Racing,
            Role = TeamRole.Support,
            AbsolutePosition = 1,
            Position = 1,
            Defense = 30,
            LapScores =
                [
                    new()
                    {
                        Order = 1,
                        Score = 15,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 15,
                    },
                ],
        };

        var mainDriver = new RaceDriver()
        {
            SeasonDriverId = 2,
            SeasonTeamId = 1,
            Status = RaceStatus.Racing,
            Role = TeamRole.Main,
            AbsolutePosition = 2,
            Position = 2,
            LapScores =
                [
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 30,
                    },
                ]
        };

        var otherDriver = new RaceDriver()
        {
            SeasonDriverId = 3,
            SeasonTeamId = 2,
            Status = RaceStatus.Racing,
            AbsolutePosition = 3,
            Position = 3,
            LapScores =
                [
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 40,
                    },
                ]
        };

        var raceDrivers = new List<RaceDriver>()
        {
            supportDriver,
            mainDriver,
            otherDriver,
        };

        var situation = SituationOccurrence.Raced;

        // Act
        manager.DeterminePositions(raceDrivers, situation);

        // Assert
        // NOTE: Currently the order is: Other overtakes Main, blocked by Support.
        // Main overtakes Other after he is blocked, then is let past by Support-driver.
        var supportMadeSwap = supportDriver.LapScores.Any(e => e.RacerEvents.HasFlag(Common.Enums.RacerEvent.Swap));

        // Check if support driver had a succesful occurrence of making a swap
        supportMadeSwap.Should().BeTrue();

        // If defending driver would not be a support driver, then main would not manage to overtake
        mainDriver.AbsolutePosition.Should().Be(1);

        // Other driver overtook main until he got blocked by the support driver
        otherDriver.OvertakeCount.Should().Be(1);
        // Being let past does not count as an overtake
        mainDriver.OvertakeCount.Should().Be(1);

        // Leading currently but is support, if main is slightly faster then they should be let through
        supportDriver.AbsolutePosition.Should().Be(2);
        // Support driver did defend his position against the third, other driver; should be 1
        supportDriver.DefensiveCount.Should().Be(1);
        // Other driver is the fastest of the bunch but will stay 3rd due to lead having a high defense
        otherDriver.AbsolutePosition.Should().Be(3);
    }

    [Fact]
    public void AttackingSupportDriverShouldMaintainPositionBehindMainDriver()
    {
        // Arrange
        var season = new Season();
        var league = LeagueFactory.GenerateTestLeague;
        var incidents = IncidentFactory.GenerateTestIncidentList;
        var config = new SimConfig();

        league.BattleRng = 2;

        var manager = new RaceManager(season, league, incidents, config);

        var mainDriverTeamOne = new RaceDriver()
        {
            SeasonDriverId = 1,
            SeasonTeamId = 1,
            Status = RaceStatus.Racing,
            Role = TeamRole.Main,
            AbsolutePosition = 1,
            Position = 1,
            Defense = 15,
            LapScores =
                [
                    new()
                    {
                        Order = 1,
                        Score = 15,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 20,
                    },
                ],
        };

        var supportDriverTeamOne = new RaceDriver()
        {
            SeasonDriverId = 2,
            SeasonTeamId = 1,
            Status = Common.Enums.RaceStatus.Racing,
            Role = Common.Enums.TeamRole.Support,
            AbsolutePosition = 2,
            Position = 2,
            Attack = 5,
            Defense = 5,
            LapScores =
                [
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 50,
                    },
                ]
        };

        var mainDriverTeamTwo = new RaceDriver()
        {
            SeasonDriverId = 3,
            SeasonTeamId = 2,
            Status = Common.Enums.RaceStatus.Racing,
            Role = Common.Enums.TeamRole.Main,
            AbsolutePosition = 4,
            Position = 4,
            Attack = 15,
            LapScores =
                [
                    new()
                    {
                        Order = 1,
                        Score = 5,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 75,
                    },
                ],
        };

        var supportDriverTeamTwo = new RaceDriver()
        {
            SeasonDriverId = 4,
            SeasonTeamId = 2,
            Status = RaceStatus.Racing,
            Role = TeamRole.Support,
            AbsolutePosition = 3,
            Position = 3,
            Defense = 50,
            LapScores =
                [
                    new()
                    {
                        Order = 1,
                        Score = 10,
                    },
                    new()
                    {
                        Order = 2,
                        Score = 40,
                    },
                ]
        };

        var raceDrivers = new List<RaceDriver>()
        {
            mainDriverTeamOne, supportDriverTeamOne, mainDriverTeamTwo, supportDriverTeamTwo
        };

        var situation = SituationOccurrence.Raced;

        // Act
        manager.DeterminePositions(raceDrivers, situation);

        // Assert

        // 1 -> 2 | Teammate stayed behind, other main overtook, other support driver got blocked by teammate support
        mainDriverTeamOne.Position.Should().Be(2);
        // 2 -> 4 | Would've end up p2 if it didn't maintain position behind teammate but it did
        supportDriverTeamOne.Position.Should().Be(4);
        // 4 -> 1 | Was fastest, support let him past, had enough speed to overtake both other drivers
        mainDriverTeamTwo.Position.Should().Be(1);
        // 3 -> 3 | Let main past, due to other support driver maintaining position it could overtake him too
        supportDriverTeamTwo.Position.Should().Be(3);

        // TODO: Implement the part where it checks whether this implementation works correctly
        // Also consider other cases here, like raceclasses and such
    }
}
