namespace SimTECH.Extensions
{
    public static class NumberHelper
    {
        public static int RoundDouble(this double number)
        {
            if (number < 0)
            {
                return (int)(number - 0.5);
            }
            return (int)(number + 0.5);
        }
    }
}
