namespace SimTECH.Extensions
{
    public static class ObjectHelper
    {
        public static TSub[] CastDownwards<TSub, TSuper>(this TSuper[] super, Func<TSuper, TSub> instancePrefiller)
            where TSub : TSuper
        {
            return super.Select(e => e.CastDownwards(instancePrefiller(e))).ToArray();
        }

        public static TSub CastDownwards<TSub, TSuper>(this TSuper super, TSub sub)
            where TSub : TSuper
        {
            var properties = typeof(TSuper).GetProperties();

            foreach (var prop in properties.Where(e => e.SetMethod != null && e.GetMethod != null))
            {
                prop.SetValue(sub, prop.GetValue(super));
            }

            return sub;
        }
    }
}
