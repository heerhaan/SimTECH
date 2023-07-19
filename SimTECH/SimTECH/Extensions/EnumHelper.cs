using MudBlazor;

namespace SimTECH.Extensions
{
    public static class EnumHelper
    {
        public static IEnumerable<Enum> GetFlagged(this Enum e) => Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag);

        public static TEnum[] GetEnumValues<TEnum>() => (TEnum[])Enum.GetValues(typeof(TEnum));

        public static State[] StatesForFilter(this StateFilter filter) => filter switch
        {
            StateFilter.All => new State[] { State.Concept, State.Active, State.Advanced, State.Closed, State.Archived },
            StateFilter.Default => new State[] { State.Concept, State.Active, State.Advanced, State.Closed },
            StateFilter.Active => new State[] { State.Active, State.Advanced },
            StateFilter.Closed => new State[] { State.Closed },
            StateFilter.Archived => new State[] { State.Archived },
            _ => new State[] { State.Concept, State.Active, State.Advanced, State.Closed }
        };

        public static string StatusColour(this RaceStatus status) => status switch
        {
            RaceStatus.Dnf => "red",
            RaceStatus.Dsq => "black",
            RaceStatus.Dnq => "rebeccapurple",
            RaceStatus.Fatal => "white",
            _ => Constants.DefaultColour
        };

        public static string SituationColour(this SituationOccurrence situation) => situation switch
        {
            SituationOccurrence.Raced => "Green",
            SituationOccurrence.Caution => "Yellow",
            _ => "muted-background-primary"
        };

        public static string GenderIcon(this Gender gender) => gender switch
        {
            Gender.All => Icons.Material.Filled.AllInclusive,
            Gender.Male => Icons.Material.Filled.Male,
            Gender.Female => Icons.Material.Filled.Female,
            Gender.Other => Icons.Custom.Uncategorized.Baguette,
            _ => Icons.Material.Filled.QuestionMark
        };

        public static string ReadableAspect(this Aspect aspect) => aspect switch
        {
            Aspect.Skill => "Skill",
            Aspect.Age => "Age",
            Aspect.BaseCar => "Car",
            Aspect.Engine => "Engine",
            Aspect.Aero => "Aero",
            Aspect.Chassis => "Chassis",
            Aspect.Powertrain => "Powertrain",
            Aspect.Reliability => "Reliability",
            Aspect.Attack => "Attack",
            Aspect.Defense => "Defense",
            _ => "Unknown"
        };

        public static readonly Aspect[] RangeableAspects = new Aspect[]
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

        public static readonly RecordStat[] DriverRecordStats = new RecordStat[]
        {
            RecordStat.Entry,
            RecordStat.Start,
            RecordStat.Win,
            RecordStat.Pole,
            RecordStat.DNF
        };

        public static string RacerEventIcon(this RacerEvent racerEvent) => racerEvent switch
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

        // Dictionary selectors underneath
        public static readonly Dictionary<Entrant, string> GetEntrantSelection = new()
        {
            { Entrant.Driver, "Driver" },
            { Entrant.Team, "Team" },
            { Entrant.Track, "Track" },
        };

        public static readonly Dictionary<CategoryIncident, string> GetIncidentCategories = new()
        {
            { CategoryIncident.Driver, "Driver" },
            { CategoryIncident.Car, "Car" },
            { CategoryIncident.Engine, "Engine" },
            { CategoryIncident.Disqualified, "Disqualified" },
            { CategoryIncident.Lethal, "Injury" },
        };

        public static readonly Dictionary<TeamRole, string> GetTeamRoleSelection = new()
        {
            { TeamRole.None, "None" },
            { TeamRole.Main, "Main" },
            { TeamRole.Support, "Support" },
        };
    }
}
