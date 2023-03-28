namespace SimTECH.Data.Models
{
    public class Result
    {
        public long Id { get; set; }
        public int Grid { get; set; }
        public int Position { get; set; }
        public int Score { get; set; }
        public RaceStatus Status { get; set; }
        public Incident Incident { get; set; }
        public int Setup { get; set; }
        public int TyreLife { get; set; }

        public long SeasonDriverId { get; set; }
        public SeasonDriver SeasonDriver { get; set; } = default!;
        public long SeasonTeamId { get; set; }
        public SeasonTeam SeasonTeam { get; set; } = default!;
        public long RaceId { get; set; }
        public Race Race { get; set; } = default!;
        public long StrategyId { get; set; }
        public Strategy Strategy { get; set; } = default!;

        public IList<StintResult>? StintResults { get; set; }
    }
}
