namespace SimTECH.Extensions
{
    public static class EnumerableHelper
    {
        public static T TakeRandomItem<T>(this T[] items)
        {
            return items[NumberHelper.RandomInt(items.Length - 1)];
        }

        public static T TakeRandomItem<T>(this IList<T> items)
        {
            return items[NumberHelper.RandomInt(items.Count - 1)];
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
            => self.Select((item, index) => (item, index));

        public static T[] TempExample<T>(this T[] items, int amount)
        {
            return Random.Shared.GetItems(items, amount);
        }
    }
}
