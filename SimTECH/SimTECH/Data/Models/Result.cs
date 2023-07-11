namespace SimTECH.Data.Models
{
    public sealed class Result : ModelBase
    {
        public int Grid { get; set; }
        public int Position { get; set; }
        public int Score { get; set; }
        public RaceStatus Status { get; set; }
        public int Setup { get; set; }
        public int TyreLife { get; set; }
        public bool FastestLap { get; set; }
        public int Overtaken { get; set; }
        public int Defended { get; set; }

        public long SeasonDriverId { get; set; }
        public SeasonDriver SeasonDriver { get; set; }
        public long SeasonTeamId { get; set; }
        public SeasonTeam SeasonTeam { get; set; }
        public long RaceId { get; set; }
        public Race Race { get; set; }
        public long TyreId { get; set; }
        public Tyre Tyre { get; set; }
        public long? IncidentId { get; set; }
        public Incident? Incident { get; set; }

        public IList<LapScore> LapScores { get; set; } = new List<LapScore>();
        public IList<QualifyingScore> QualifyingScores { get; set; } = new List<QualifyingScore>();
        public IList<PracticeScore> PracticeScores { get; set; } = new List<PracticeScore>();
    }
}
