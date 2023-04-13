using SimTECH.Data.Models;

namespace SimTECH.Extensions
{
    public static class ViewHelper
    {
        public static string SetGradientTriangleStyle(string colour, string accent)
            => $"background: linear-gradient(to top left, var(--mud-palette-surface) 49.5%, {colour} 50.5%); color: {accent}";

        public static string SetFullColourstyle(string colour, string accent) => $"background-color:{colour};color:{accent}";

        public static string SetBackgroundColour(string colour) => $"background-color:{colour}";

        public static string SetBorderLeftStyle(string colour) => $"border-left:solid 10px {colour};";

        public static string SetBorderRightStyle(string colour) => $"border-right:solid 10px {colour};";

        public static Func<SeasonDriver, string> DriverGradientStyleFunc => driver =>
        {
            if (driver.SeasonTeam is null)
                return SetGradientTriangleStyle("var(--mud-palette-secondary)", "var(--mud-palette-secondary-text)");

            return SetGradientTriangleStyle(driver.SeasonTeam.Colour, driver.SeasonTeam.Accent);
        };

        public static Func<SeasonDriver, string> DriverTeamStyleFunc => driver =>
        {
            if (driver.SeasonTeam is null)
                return SetFullColourstyle("var(--mud-palette-secondary)", "var(--mud-palette-secondary-text)");

            return SetFullColourstyle(driver.SeasonTeam.Colour, driver.SeasonTeam.Accent);
        };

        public static Func<SeasonTeam, string> TeamFullStyleFunc => team => SetFullColourstyle(team.Colour, team.Accent);

        public static Func<SeasonTeam, string> TeamBorderRightStyleFunc => team => SetBorderRightStyle(team.Colour);
    }
}
