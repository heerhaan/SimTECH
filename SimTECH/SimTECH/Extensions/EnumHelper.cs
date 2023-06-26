using SimTECH.Data.Models;

namespace SimTECH.Extensions
{
    public static class EnumHelper
    {
        public static IEnumerable<Enum> GetFlagged(this Enum e)
        {
            return Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag);
        }

        public static Country DefaultCountry => Country.FM;

        public static State[] StatesForFilter(this StateFilter filter) => filter switch
        {
            StateFilter.All => new State[] { State.Concept, State.Active, State.Advanced, State.Closed, State.Archived },
            StateFilter.Default => new State[] { State.Concept, State.Active, State.Advanced, State.Closed },
            StateFilter.Active => new State[] { State.Active, State.Advanced },
            StateFilter.Closed => new State[] { State.Closed },
            StateFilter.Archived => new State[] { State.Archived },
            _ => new State[] { State.Concept, State.Active, State.Advanced, State.Closed }
        };

        public static string ReadableAspect(this Aspect aspect) => aspect switch
        {
            Aspect.Skill => "Skill",
            Aspect.Age => "Age",
            Aspect.BaseCar => "BaseCar",
            Aspect.Engine => "Engine",
            Aspect.Aero => "Aero",
            Aspect.Chassis => "Chassis",
            Aspect.Powertrain => "Powertrain",
            Aspect.Reliability => "Reliability",
            Aspect.Attack => "Attack",
            Aspect.Defense => "Defense",
            _ => "Unknown"
        };

        public static Aspect[] RangeableAspects => new Aspect[]
        {
            Aspect.Skill,
            Aspect.Age,
            Aspect.BaseCar,
            Aspect.Engine,
            Aspect.Aero,
            Aspect.Chassis,
            Aspect.Powertrain,
            Aspect.Reliability,
            Aspect.Attack,
            Aspect.Defense,
        };

        // Dictionary selectors underneath
        public static Dictionary<Entrant, string> GetEntrantSelection => new()
        {
            { Entrant.Driver, "Driver" },
            { Entrant.Team, "Team" },
            { Entrant.Track, "Track" },
        };

        public static Dictionary<CategoryIncident, string> GetIncidentCategories => new()
        {
            { CategoryIncident.Driver, "Driver" },
            { CategoryIncident.Car, "Car" },
            { CategoryIncident.Engine, "Engine" },
            { CategoryIncident.Disqualified, "Disqualified" },
            { CategoryIncident.Lethal, "Lethal" },
        };

        public static Dictionary<TeamRole, string> GetTeamRoleSelection => new()
        {
            { TeamRole.None, "None" },
            { TeamRole.Main, "Main" },
            { TeamRole.Support, "Support" },
        };
    }
}
