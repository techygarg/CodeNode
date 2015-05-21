using System;
using System.Collections.Generic;
using CodeNode.Core.Utils;

namespace CodeNode.Extension
{
    public static class EnumExtentions
    {
        /// <summary>
        ///     Enum To the dictionary. typeof(Enum).ToDictionary();
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        public static Dictionary<int, string> ToDictionary(this Type enumType)
        {
            Ensure.Argument.Is(typeof (Enum).IsAssignableFrom(enumType), "Type should be of Enum.");

            var result = new Dictionary<int, string>();
            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType);
            for (var i = 0; i < names.Length; i++)
            {
                result.Add((int) values.GetValue(i), (string) names.GetValue(i));
            }
            return result;
        }
    }
}