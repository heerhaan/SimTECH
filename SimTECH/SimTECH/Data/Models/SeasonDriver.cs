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

        public IList<Result> RaceResults { get; set; }

        public long DriverId { get; set; }
        public Driver Driver { get; set; } = default!;
        public long SeasonTeamId { get; set; }
        public SeasonTeam SeasonTeam { get; set; } = default!;
        public long SeasonId { get; set; }
        public Season Season { get; set; } = default!;
    }
}
