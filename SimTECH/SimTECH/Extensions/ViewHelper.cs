using SimTECH.Data.Models;

namespace SimTECH.Extensions
{
    public static class ViewHelper
    {
        public static string GetGradientCellStyle(string colour, string accent)
            => $"background: linear-gradient(to top left, var(--mud-palette-surface) 49.5%, {colour} 50.5%); color: {accent}";

        public static string GetFullColourStyle(string colour, string accent) => $"background-color:{colour};color:{accent}";

        public static string SetBackgroundColour(string colour) => $"background-color:{colour}";

        public static string GetSimpleStyle(string colour) => $"border-right:solid 10px {colour};";

        public static Func<SeasonDriver, string> DriverGradientStyleFunc => driver =>
        {
            if (driver.SeasonTeam is null)
                return GetGradientCellStyle("var(--mud-palette-secondary)", "var(--mud-palette-secondary-text)");

            return GetGradientCellStyle(driver.SeasonTeam.Colour, driver.SeasonTeam.Accent);
        };

        public static Func<SeasonDriver, string> DriverTeamStyleFunc => driver =>
        {
            if (driver.SeasonTeam is null)
                return GetFullColourStyle("var(--mud-palette-secondary)", "var(--mud-palette-secondary-text)");

            return GetFullColourStyle(driver.SeasonTeam.Colour, driver.SeasonTeam.Accent);
        };

        public static Func<SeasonTeam, string> TeamCellStyleFunc => team => GetFullColourStyle(team.Colour, team.Accent);

        public static Func<SeasonTeam, string> TeamSimpleStyleFunc => team => GetSimpleStyle(team.Colour);
    }
}
