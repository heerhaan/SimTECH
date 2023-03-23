namespace SimTECH.Extensions
{
    public static class NumberHelper
    {
        private static readonly Random _rng = new();

        public static int RoundDouble(this double number)
        {
            if (number < 0)
            {
                return (int)(number - 0.5);
            }
            return (int)(number + 0.5);
        }

        public static int RandomInt(int max) => RandomInt(0, max);
        public static int RandomInt(int min, int max)
        {
            return _rng.Next(min, max + 1);
        }

        public static double RandomDouble(double max) => RandomDouble(0, max);
        public static double RandomDouble(double min, double max)
        {
            return _rng.NextDouble() * (max - min) + min;
        }
    }
}
