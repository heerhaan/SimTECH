namespace SimTECH.Data.Models
{
    public class SeasonDriver
    {
        public long Id { get; set; }
        public int Number { get; set; }
        public int Points { get; set; }
        public int HiddenPoints { get; set; }
        public int Skill { get; set; }
        public int Reliability { get; set; }
        public TeamRole TeamRole { get; set; }

        public long SeasonId { get; set; }
        public Season Season { get; set; } = default!;
        public long DriverId { get; set; }
        public Driver Driver { get; set; } = default!;
        // SeasonTeam is nullable, if it were null then the driver would be 'dropped' from the team
        public long? SeasonTeamId { get; set; }
        public SeasonTeam? SeasonTeam { get; set; }

        public IList<Result> Results { get; set; } = default!;
    }

    public static class ExtendSeasonDriver
    {
        public static int RetrieveStatusBonus(this SeasonDriver driver, int modifier) => driver.TeamRole switch
        {
            TeamRole.Main => modifier,
            TeamRole.Support => modifier * -1,
            _ => 0,
        };
    }
}
