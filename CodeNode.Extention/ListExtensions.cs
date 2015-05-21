using System;
using System.Collections.Generic;
using CodeNode.Core.Utils;

namespace CodeNode.Extension
{
    public static class ListExtensions
    {
        /// <summary>
        ///     Moves the item matching the <paramref name="itemSelector" /> to the <paramref name="newIndex" /> in the
        ///     <paramref name="list" />.
        /// </summary>
        public static void Move<T>(this List<T> list, Predicate<T> itemSelector, int newIndex)
        {
            Ensure.Argument.NotNull(list, "list");
            Ensure.Argument.NotNull(itemSelector, "itemSelector");
            Ensure.Argument.Is(newIndex >= 0, "New index must be greater than or equal to zero.");

            var currentIndex = list.FindIndex(itemSelector);
            Ensure.That<ArgumentException>(currentIndex >= 0, "No item was found that matches the specified selector.");

            if (currentIndex == newIndex)
                return;

            // Copy the item
            var item = list[currentIndex];

            // Remove the item from the list
            list.RemoveAt(currentIndex);

            // Finally insert the item at the new index
            list.Insert(newIndex, item);
        }
    }
}