using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeNode.Extention
{
    public static class EnumerableExtensions
    {
        public static T Aggregate<T>(this IEnumerable<T> list, Func<T, T, T> aggregateFunction)
        {
            return Aggregate(list, default(T), aggregateFunction);
        }

        public static T Aggregate<T>(this IEnumerable<T> list, T defaultValue,Func<T, T, T> aggregateFunction)
        {
            var enumerable = list as IList<T> ?? list.ToList();
            return !enumerable.Any()
                ? defaultValue
                : enumerable.Aggregate(aggregateFunction);
        }
    }
}