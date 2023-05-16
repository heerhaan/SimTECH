﻿using MudBlazor;
using SimTECH.Data.Models;

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

        public static string SetTextNumberStyle(string colour, string accent) => $"color:{colour}; text-shadow: 2px 1px 2px {accent}";

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

        public static string GetRacerEventIcon(this RacerEvent racerEvent) => racerEvent switch
        {
            RacerEvent.DriverDnf => IconCollection.HelmetOff,
            RacerEvent.CarDnf => IconCollection.CarCrash,
            RacerEvent.EngineDnf => IconCollection.EngineOff,
            RacerEvent.Mistake => Icons.Material.Filled.ErrorOutline,
            RacerEvent.Pitstop => Icons.Material.Filled.LocalGasStation,
            RacerEvent.Swap => Icons.Material.Filled.SwapVert,
            RacerEvent.Death => IconCollection.Skull,
            _ => Icons.Material.Filled.QuestionMark
        };

        public static string GetGenderIcon(this Gender gender) => gender switch
        {
            Gender.All => Icons.Material.Filled.AllInclusive,
            Gender.Male => Icons.Material.Filled.Male,
            Gender.Female => Icons.Material.Filled.Female,
            Gender.Other => Icons.Custom.Uncategorized.Baguette,
            _ => Icons.Material.Filled.QuestionMark
        };
    }
}
