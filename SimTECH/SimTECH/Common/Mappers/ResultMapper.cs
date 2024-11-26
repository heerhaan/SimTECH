using SimTECH.Data.Models;
using SimTECH.PageModels.Seasons.Standings;

namespace SimTECH.Common.Mappers;

public static class ResultMapper
{
    public static StandingDriverModel MapToStandingDriver(this SeasonDriver driver, SeasonTeam? team, int pos)
    {
        return new StandingDriverModel
        {
            Id = driver.Id,
            Position = pos,
            Name = driver.Driver.FullName,
            Number = driver.Number,
            Nationality = driver.Driver.Country,
            Colour = team?.Colour ?? "var(--mud-palette-primary)",
            Accent = team?.Accent ?? "var(--mud-palette-primary-text)",
            Points = driver.Points,
            HiddenPoints = driver.HiddenPoints,
            Mark = driver.Driver.Mark,
            SeasonTeamId = driver.SeasonTeamId,
            Team = team?.Team?.Name ?? "None",
        };
    }

    public static StandingTeamModel MapToStandingTeam(this SeasonTeam team, int pos)
    {
        return new StandingTeamModel
        {
            Id = team.Id,
            Position = pos,
            Name = team.Name,
            Principal = team.Principal,
            Nationality = team.Team.Country,
            Colour = team.Colour,
            Accent = team.Accent,
            Points = team.Points,
            HiddenPoints = team.HiddenPoints,
            Mark = team.Team.Mark,
        };
    }

    public static StandingResultCell MapToStandingResult(this Result result, int round)
    {
        return new StandingResultCell
        {
            Position = result.Position,
            Status = result.Status,
            Round = round,
            Pole = result.Grid == 1,
            FL = result.FastestLap,
        };
    }
}
