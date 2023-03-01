namespace SimTECH.Extensions
{
    public static class EnumHelper
    {
        public static string CountryCodeToEmoji(this Country countryCode)
        {
            return string.Concat(countryCode.ToString().Select(e => char.ConvertFromUtf32(e + 0x1F1A5)));
        }

        public static Country GetDefaultCountry() => Country.FM;

        public static Dictionary<Entrant, string> RetrieveEntrantDict() => EntrantDict;

        #region global values

        private static readonly Dictionary<Entrant, string> EntrantDict = new()
        {
            { Entrant.Driver, "Driver" },
            { Entrant.Team, "Team" },
            { Entrant.Track, "Track" },
        };

        #endregion
    }
}
