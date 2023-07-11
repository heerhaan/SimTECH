namespace SimTECH.Extensions
{
    public static class ViewHelper
    {
        public static string SetBackgroundColour(string colour) => $"background-color:{colour}";

        public static string SetBorderRightStyle(string colour) => $"border-right:solid 10px {colour};";

        public static string SetTextNumberStyle(string colour, string accent) => $"color:{colour}; text-shadow: 2px 1px 2px {accent}";

        // Visual stuff based on enums
        public static string CountryCodeToEmoji(this Country countryCode)
        {
            return string.Concat(countryCode.ToString().Select(e => char.ConvertFromUtf32(e + 0x1F1A5)));
        }
    }
}
