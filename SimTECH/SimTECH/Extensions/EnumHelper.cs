﻿using MudBlazor;

namespace SimTECH.Extensions
{
    public static class EnumHelper
    {
        public static IEnumerable<Enum> GetFlagged(this Enum e)
        {
            return Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag);
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

        public static Incident[] GetDriverIncidents => new Incident[] { Incident.Damage, Incident.Collision, Incident.Accident, Incident.Puncture };
        public static Incident[] GetCarIncidents => new Incident[] { Incident.Electrics, Incident.Exhaust, Incident.Clutch, Incident.Hydraulics, Incident.Wheel, Incident.Brakes };

        // Dictionary selectors underneath
        public static Dictionary<Entrant, string> GetEntrantSelection() => new()
        {
            { Entrant.Driver, "Driver" },
            { Entrant.Team, "Team" },
            { Entrant.Track, "Track" },
        };

        public static Dictionary<TeamRole, string> GetTeamRoleSelection() => new()
        {
            { TeamRole.None, "None" },
            { TeamRole.Main, "Main" },
            { TeamRole.Support, "Support" },
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
