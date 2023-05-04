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

        public static State[] StatesForFilter(this StateFilter filter) => filter switch
        {
            StateFilter.All => new State[] { State.Concept, State.Active, State.Advanced, State.Closed, State.Archived },
            StateFilter.Default => new State[] { State.Concept, State.Active, State.Advanced, State.Closed },
            StateFilter.Closed => new State[] { State.Closed },
            StateFilter.Archived => new State[] { State.Archived },
            _ => new State[] { State.Concept, State.Active, State.Advanced, State.Closed }
        };

        public static RaceIncident[] GetDriverIncidents => new RaceIncident[] { RaceIncident.Damage, RaceIncident.Collision, RaceIncident.Accident, RaceIncident.Puncture };
        public static RaceIncident[] GetCarIncidents => new RaceIncident[] { RaceIncident.Electrics, RaceIncident.Exhaust, RaceIncident.Clutch, RaceIncident.Hydraulics, RaceIncident.Wheel, RaceIncident.Brakes };
        public static RaceIncident[] GetDisqualifications => new RaceIncident[] { RaceIncident.Dangerous, RaceIncident.Illegal, RaceIncident.Fuel };

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
