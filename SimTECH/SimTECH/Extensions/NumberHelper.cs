namespace SimTECH.Extensions;

public static class NumberHelper
{
    private static int rngSeed = Environment.TickCount;
    private static readonly ThreadLocal<Random> _rng = new(() => new Random(Interlocked.Increment(ref rngSeed)));

    public static int RandomInt(int max) => RandomInt(0, max);
    public static int RandomInt(int min, int max)
    {
        if (min == max)
            return min;

        return _rng.Value?.Next(min, max + 1) ?? 0;
    }

    public static double RandomDouble(double max) => RandomDouble(0, max);
    public static double RandomDouble(double min, double max)
    {
        if (min == max)
            return min;

        if (_rng.Value == null)
            throw new Exception("Local random thread's value was NULL");

        return (_rng.Value.NextDouble() * (max - min)) + min;
    }

    public static int RoundToInt(this double number)
    {
        if (number < 0)
            return (int)(number - 0.5);

        return (int)(number + 0.5);
    }

    public static int PercentageOfNumber(this int number, int percentage)
    {
        if (percentage == 0)
            return 0;

        var valueNumber = number * ((double)percentage / 100);
        return RoundToInt(valueNumber);
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

    public static int LapCount(int raceLength, double trackLength) => (int)Math.Round(raceLength / trackLength);

    // Determines the current speed(km/h) by dividing the traversed distance(km) with the time(hr) it took
    public static double CalculateSpeed(double distance, double time) => distance / time;

    // Determines the traversed distance by multiplying the speed by time spent
    public static double CalculateDistance(double speed, double time) => speed * time;

    // Determines the time it took by dividing the distance with the travel speed
    public static double CalculateTime(double distance, double speed) => distance / speed;
}
