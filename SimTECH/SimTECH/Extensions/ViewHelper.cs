using MudBlazor;
using SimTECH.Data.Models;
using System.Drawing;

namespace SimTECH.Extensions
{
    public static class ViewHelper
    {
        public static string SetGradientTriangleStyle(string colour, string accent)
            => $"background: linear-gradient(to top left, var(--mud-palette-surface) 49.5%, {colour} 50.5%); color: {accent}";

        public static string SetFullColourstyle(string colour, string accent) => $"background-color:{colour};color:{accent}";

        public static string SetBackgroundColour(string colour) => $"background-color:{colour}";

        public static string SetAccentColour(string accent) => $"color:{accent}";

        public static string SetBorderLeftStyle(string colour) => $"border-left:solid 10px {colour};";

        public static string SetBorderRightStyle(string colour) => $"border-right:solid 10px {colour};";

        public static string SetTextStroke(string colour, int size = 1) => $"-webkit-text-stroke: {size}px {colour}";

        public static string SetTextColourStyle(string colour, string accent) => $"-webkit-text-stroke: 1px {accent}; color:{colour}";

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

        // Visual stuff based on enums
        public static string CountryCodeToEmoji(this Country countryCode)
        {
            return string.Concat(countryCode.ToString().Select(e => char.ConvertFromUtf32(e + 0x1F1A5)));
        }

        public static string GetGenderIcon(this Gender gender) => gender switch
        {
            Gender.All => Icons.Material.Filled.AllInclusive,
            Gender.Male => Icons.Material.Filled.Male,
            Gender.Female => Icons.Material.Filled.Female,
            Gender.Other => Icons.Custom.Uncategorized.Baguette,
            _ => Icons.Material.Filled.QuestionMark
        };

        [Obsolete]
        public static string GetWeatherIcon(this Weather weather) => weather switch
        {
            Weather.Sunny => Icons.Material.Filled.WbSunny,
            Weather.Overcast => Icons.Material.Filled.Cloud,
            Weather.Rain => Icons.Material.Filled.WaterDrop,
            Weather.Storm => Icons.Material.Filled.Tsunami,
            _ => Icons.Material.Filled.QuestionMark
        };
    }
}
