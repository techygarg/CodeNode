using System;
using System.Linq;
using System.Collections.Generic;
using CodeNode.Core.Utils;
using System.ComponentModel;

namespace CodeNode.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Enum To the dictionary.
        /// </summary>
        /// <typeparam name="T">Type of the enum</typeparam>
        /// <returns>Returns the dictionary from the enumeration</returns>
        public static Dictionary<int, string> ToDictionary<T>() where T : struct
        {
            var enumType = typeof(T);

            Ensure.Argument.Is(typeof(Enum).IsAssignableFrom(enumType), "Type should be of Enum.");

            return Enum.GetValues(enumType).Cast<int>()
                       .ToDictionary(v => v, v => Enum.GetName(enumType, v));
        }

        /// <summary>
        /// Convert the string to the enumeration provided
        /// </summary>
        /// <typeparam name="T">enum type</typeparam>
        /// <param name="input">string input</param>
        /// <returns>enum equivalent of string</returns>
        public static T ToEnum<T>(this string input) where T : struct
        {
            Ensure.Argument.Is(typeof(T).IsEnum, "Type should be of Enum.");

            T result;
            Enum.TryParse<T>(input, true, out result);

            return result;
        }

        /// <summary>
        /// Return the description of Enum property 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum source)
        {
            var field = source.GetType().GetField(source.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : source.ToString();
        }
    }
}