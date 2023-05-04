namespace SimTECH.Data.Models
{
    public sealed class Result
    {
        public long Id { get; set; }
        public int Grid { get; set; }
        public int Position { get; set; }
        public int Score { get; set; }
        public RaceStatus Status { get; set; }
        public RaceIncident Incident { get; set; }
        public int Setup { get; set; }
        public int TyreLife { get; set; }

        public long SeasonDriverId { get; set; }
        public SeasonDriver SeasonDriver { get; set; }
        public long SeasonTeamId { get; set; }
        public SeasonTeam SeasonTeam { get; set; }
        public long RaceId { get; set; }
        public Race Race { get; set; }
        public long StrategyId { get; set; }
        public Strategy Strategy { get; set; }
        public long? IncidentId { get; set; }
        public Incident? RacingIncident { get; set; }

        public IList<LapScore> LapScores { get; set; } = new List<LapScore>();
    }
}
