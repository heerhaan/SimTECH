using SimTECH.Data.Models;

namespace SimTECH.Extensions
{
    public static class ViewHelper
    {
        public static string GetGradientCellStyle(string colour, string accent)
        {
            return $"background: linear-gradient(to top left, var(--mud-palette-surface) 49.5%, {colour} 50.5%); color: {accent}";
        }

        public static string GetTeamSimpleStyle(string colour)
        {
            return $"border-right:solid 10px {colour};";
        }

        public static Func<SeasonDriver, string> DriverGradientStyleFunc => driver =>
        {
            if (driver.SeasonTeam is null)
                return GetGradientCellStyle("var(--mud-palette-secondary)", "var(--mud-palette-secondary-text)");

            return GetGradientCellStyle(driver.SeasonTeam.Colour, driver.SeasonTeam.Accent);
        };

        public static Func<SeasonTeam, string> TeamCellStyleFunc => team => $"background-color:{team.Colour};color:{team.Accent}";
    }
}
