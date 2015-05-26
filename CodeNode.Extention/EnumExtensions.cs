using System;
using System.Linq;
using System.Collections.Generic;
using CodeNode.Core.Utils;
using System.ComponentModel;

namespace CodeNode.Extension
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Enum To the dictionary.
        /// </summary>
        /// <typeparam name="T">Type of the enum</typeparam>
        /// <returns>Returns the dictionary from the enumeration</returns>
        public static Dictionary<int, string> ToDictionary<T>() where T : struct
        {
            Type enumType = typeof(T);

            Ensure.Argument.Is(typeof(Enum).IsAssignableFrom(enumType), "Type should be of Enum.");

            return Enum.GetValues(enumType).Cast<int>()
                       .ToDictionary(v => v, v => Enum.GetName(enumType, v));
        }

        /// <summary>
        /// Convert the string to the enumeration provided
        /// </summary>
        /// <typeparam name="T">enum type</typeparam>
        /// <param name="input">string input</param>
        /// <returns>enum equivalant of string</returns>
        public static T ToEnum<T>(this string input) where T : struct
        {
            Ensure.Argument.Is(typeof(T).IsEnum, "Type should be of Enum.");

            var result = default(T);
            Enum.TryParse<T>(input, true, out result);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum source)
        {
            var fi = source.GetType().GetField(source.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
    }
}