using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SimTECH.Extensions
{
    public static class EnumerableHelper
    {
        public static IQueryable<T> TakeLastSpecial<T>(this IQueryable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }
    }
}
