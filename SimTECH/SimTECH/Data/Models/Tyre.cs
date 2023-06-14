namespace SimTECH.Data.Models
{
    public sealed class Tyre
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public string Colour { get; set; } = default!;
        public State State { get; set; }

        public int Pace { get; set; }
        public int WearMin { get; set; }
        public int WearMax { get; set; }
        public int DistanceMin { get; set; }
        public int DistanceMax { get; set; }
        public bool ForWet { get; set; }
    }

    public static class ExtendTyre
    {
        public static int ExpectedLength(this Tyre tyre, int distance)
        {
            if (tyre.Pace == 0 || (tyre.WearMin == 0 && tyre.WearMax == 0))
                return 0;

            var wearAvg = (tyre.WearMin + tyre.WearMax) / 2;

            return tyre.Pace / wearAvg * distance;
        }
    }
}
