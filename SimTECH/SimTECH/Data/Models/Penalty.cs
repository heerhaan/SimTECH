namespace SimTECH.Data.Models
{
    // Deprecated
    public sealed class Penalty
    {
        public long Id { get; set; }
        public string Reason { get; set; } = default!;
        public int Punishment { get; set; }

        public long SeasonDriverId { get; set; }
        public SeasonDriver SeasonDriver { get; set; } = default!;
        public long RaceId { get; set; }
        public Race Race { get; set; } = default!;
    }

    public static class ExtendPenalty
    {
        public static double DoubledPunishment(this Penalty penalty) => (Convert.ToDouble(penalty.Punishment) + 0.2);
    }
}
