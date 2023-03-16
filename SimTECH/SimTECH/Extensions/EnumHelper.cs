namespace SimTECH.Extensions
{
    public static class EnumHelper
    {
        public static string CountryCodeToEmoji(this Country countryCode)
        {
            return string.Concat(countryCode.ToString().Select(e => char.ConvertFromUtf32(e + 0x1F1A5)));
        }

        public static Country GetDefaultCountry() => Country.FM;

        public static Weather GetRandomWeather()
        {
            var randomNumber = NumberHelper.RandomInt(20);

            return randomNumber switch
            {
                int n when n <= 8 => Weather.Sunny,
                int n when n > 8 && n <= 16 => Weather.Overcast,
                int n when n > 16 && n <= 19 => Weather.Rain,
                int n when n > 19 => Weather.Storm,
                _ => Weather.Sunny,
            };
        }

        // Dictionary selectors underneath
        public static Dictionary<Entrant, string> GetEntrantSelection() => new()
        {
            { Entrant.Driver, "Driver" },
            { Entrant.Team, "Team" },
            { Entrant.Track, "Track" },
        };

        public static Dictionary<Weather, string> GetWeatherSelection() => new()
        {
            { Weather.Sunny, "Sunny" },
            { Weather.Overcast, "Overcast" },
            { Weather.Rain, "Rain" },
            { Weather.Storm, "Storm" },
        };
    }
}
