using System;
using System.Collections.Generic;
using System.Linq;
using CodeNode.Core.Utils;

namespace CodeNode.Extension
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Aggregates the specified aggregate function. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="aggregateFunction">The aggregate function.</param>
        /// <returns></returns>
        public static T Aggregate<T>(this IEnumerable<T> items, Func<T, T, T> aggregateFunction)
        {
            Ensure.Argument.NotNull(items, "IEnumerable");
            Ensure.Argument.NotNull(aggregateFunction, "AggregateFunction");
            return Aggregate(items, default(T), aggregateFunction);
        }

        /// <summary>
        /// Aggregates the specified default value. Return default value in place of null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="aggregateFunction">The aggregate function.</param>
        /// <returns></returns>
        public static T Aggregate<T>(this IEnumerable<T> items, T defaultValue,Func<T, T, T> aggregateFunction)
        {
            Ensure.Argument.NotNull(items, "IEnumerable");
            Ensure.Argument.NotNull(aggregateFunction, "AggregateFunction");

            var enumerable = items as IList<T> ?? items.ToList();
            return !enumerable.Any()
                ? defaultValue
                : enumerable.Aggregate(aggregateFunction);
        }

        /// <summary>
        /// Invoke action for all elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            Ensure.Argument.NotNull(items, "items");
            Ensure.Argument.NotNull(action, "action");

            foreach (var p in items)
                action.Invoke(p);
        }

        /// <summary>
        /// Checks if all elements in a sequence are equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool IsAllEqual<T>(this IEnumerable<T> items)
        {
            Ensure.Argument.NotNull(items, "items");

            return items.Distinct().Count() == 1;
        }

        /// <summary>
        /// Returns all elements from items that precede the first occurrence of an element that satisfies a specified condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> AllBeforeFirstOccuranceOf<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            Ensure.Argument.NotNull(items, "items");
            Ensure.Argument.NotNull(predicate, "predicate");

            return items.TakeWhile(x => !predicate.Invoke(x));
        }

        /// <summary>
        /// Provide all the combinations of other IEnumerable.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="source1">The source1.</param>
        /// <param name="source2">The source2.</param>
        /// <returns></returns>
        public static IEnumerable<Tuple<T1, T2>> AllCombinationsOf<T1, T2>(this IEnumerable<T1> source1, IEnumerable<T2> source2)
        {
            Ensure.Argument.NotNull(source1, "source1");
            Ensure.Argument.NotNull(source2, "source2");

            // this line just reduce the chance of multiple enumeration of  IEnumerable
            var enumerable = source2 as T2[] ?? source2.ToArray();
            return
                from e1 in source1
                from e2 in enumerable
                select new Tuple<T1, T2>(e1, e2);
        }

        /// <summary>
        /// Provide all the combinations with self.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static IEnumerable<Tuple<T,T>> AllCombinationsOf<T>(this IEnumerable<T> items)
        {
            return items.AllCombinationsOf(items);
        }

    }
}