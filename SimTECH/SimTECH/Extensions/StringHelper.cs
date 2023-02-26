namespace SimTECH.Extensions
{
    public static class StringHelper
    {
        public static string CountryCodeToEmoji(this string countryCode)
        {
            if (countryCode == null || countryCode.Length != 2)
                return "[??]";

            return string.Concat(countryCode.ToUpper().Select(e => char.ConvertFromUtf32(e + 0x1F1A5)));
        }
    }
}
