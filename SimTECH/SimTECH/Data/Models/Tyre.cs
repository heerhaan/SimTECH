using SimTECH.Extensions;

namespace SimTECH.Data.Models
{
    public sealed class Tyre : ModelState
    {
        public string Name { get; set; }
        public string Colour { get; set; }

        public int Pace { get; set; }
        public int PitWhenBelow { get; set; }
        public int WearMin { get; set; }
        public int WearMax { get; set; }
        public int DistanceMin { get; set; }
        public int DistanceMax { get; set; }
        public bool ForWet { get; set; }

        public IList<LeagueTyre> LeagueTyres { get; set; }
    }

    public static class ExtendTyre
    {
        public static int ExpectedLength(this Tyre tyre, int distance)
        {
            if (tyre.Pace == 0 || (tyre.WearMin == 0 && tyre.WearMax == 0))
                return 0;

            var wearAvg = (double)(tyre.WearMin + tyre.WearMax) / 2;
            var averageLength = (tyre.Pace  / wearAvg) * distance;

            return averageLength.RoundDouble();
        }

        public static IEnumerable<Tyre> FindValidTyres(this IEnumerable<Tyre> tyres, int distanceLeft, bool isWet)
        {
            if (!tyres.Any())
                return tyres;

            return tyres
                .Where(e => e.DistanceMin < distanceLeft
                     && e.DistanceMax > distanceLeft
                     && e.ForWet == isWet)
                .ToList();
        }
    }
}
