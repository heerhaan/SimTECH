namespace SimTECH.Data.Models
{
    public record Stint
    {
        public long Id { get; set; }
        public int Order { get; set; }

        public StintEvent StintEvents { get; set; }
        public int RngMin { get; set; }
        public int RngMax { get; set; }

        public long RaceId { get; set; }
        public Race Race { get; set; } = default!;

        public IList<StintResult> StintResults { get; set; } = default!;
    }
}
