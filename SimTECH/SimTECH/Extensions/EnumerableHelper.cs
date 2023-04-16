namespace SimTECH.Extensions
{
    public static class EnumerableHelper
    {
        public static IQueryable<T> TakeLastSpecial<T>(this IQueryable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }

        public static T TakeRandomItem<T>(this T[] items)
        {
            return items[NumberHelper.RandomInt(items.Length - 1)];
        }

        public static T TakeRandomItem<T>(this IList<T> items)
        {
            return items[NumberHelper.RandomInt(items.Count - 1)];
        }
    }
}
