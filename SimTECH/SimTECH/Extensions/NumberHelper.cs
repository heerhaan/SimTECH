namespace SimTECH.Extensions
{
    public static class NumberHelper
    {
        private static int rngSeed = Environment.TickCount;
        private static readonly ThreadLocal<Random> _rng = new(() => new Random(Interlocked.Increment(ref rngSeed)));
        private static readonly Random _oldRng = new();

        public static int OldRandom(int max) => _oldRng.Next(max);

        public static int RandomInt(int max) => RandomInt(0, max);
        public static int RandomInt(int min, int max)
        {
            return _rng.Value!.Next(min, max + 1);
        }

        public static int GetQualyBonus(int grid, int driverCount, int bonus) =>
            (driverCount * bonus) - ((grid - 1) * bonus);

        public static double RandomDouble(double max) => RandomDouble(0, max);
        public static double RandomDouble(double min, double max)
        {
            return (_rng.Value!.NextDouble() * (max - min)) + min;
        }

        public static int RoundDouble(this double number)
        {
            if (number < 0)
                return (int)(number - 0.5);

            return (int)(number + 0.5);
        }

        public static double Percentage(int number, int max) => Percentage(number, (double)max);
        public static double Percentage(int number, double max)
        {
            var perc = number / max;
            return Math.Round(100 * perc, 2);
        }

        public static double Average(int sum, int amountValues) => Average((double)sum, amountValues);
        public static double Average(double sum, int amountValues)
        {
            var avg = sum / amountValues;
            return Math.Round(avg, 2);
        }
        public static double Average(params int[] numbers)
        {
            var length = numbers.Length;
            if (length == 0)
                return 0;

            var sum = (double)numbers.Sum();
            var average = sum / length;
            return Math.Round(average, 1);
        }

        public static int CalcLapCount(int raceLength, double trackLength) =>
            (int)Math.Round(raceLength / trackLength);
    }
}
