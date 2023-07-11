namespace SimTECH.Data.Models
{
    public sealed class SeasonDriver : ModelBase
    {
        public int Number { get; set; }
        public int Points { get; set; }
        public int HiddenPoints { get; set; }
        public int Skill { get; set; }
        public int Reliability { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public TeamRole TeamRole { get; set; }

        public long SeasonId { get; set; }
        public Season Season { get; set; }
        public long DriverId { get; set; }
        public Driver Driver { get; set; }
        // SeasonTeam is nullable, if it were null then the driver would be 'dropped' from the team
        public long? SeasonTeamId { get; set; }
        public SeasonTeam? SeasonTeam { get; set; }

        public IList<Result> Results { get; set; } = default!;
        public IList<GivenPenalty> GivenPenalties { get; set; } = default!;
    }

    public static class ExtendSeasonDriver
    {
        public static int RetrieveStatusBonus(this SeasonDriver driver, int modifier) => driver.TeamRole switch
        {
            TeamRole.Main => modifier,
            TeamRole.Support => modifier * -1,
            _ => 0,
        };

        public static int RetrieveAspectValue(this SeasonDriver driver, Aspect aspect) => aspect switch
        {
            Aspect.Skill => driver.Skill,
            Aspect.Reliability => driver.Reliability,
            Aspect.Attack => driver.Attack,
            Aspect.Defense => driver.Defense,
            _ => throw new InvalidOperationException("Invalid aspect for this entrant")
        };

        public static int GimmeAge(this SeasonDriver driver, int currentYear) => currentYear - driver.Driver?.DateOfBirth.Year ?? 0;
    }
}
