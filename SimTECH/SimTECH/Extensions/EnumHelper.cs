namespace SimTECH.Extensions
{
    public static class EnumHelper
    {
        public static string CountryCodeToEmoji(this Country countryCode)
        {
            return string.Concat(countryCode.ToString().Select(e => char.ConvertFromUtf32(e + 0x1F1A5)));
        }

        public static Country GetDefaultCountry() => Country.FM;
    }
}
