using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using CodeNode.Core.Utils;

namespace CodeNode.Extension
{
    /// <summary>
    ///     Extensions for <see cref="System.String" />
    ///    Credits: Some functions credit goes Ben-Foster and team
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether the specified target is equal with StringComparison.InvariantCultureIgnoreCase.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static bool IsEqual(this string source, string target)
        {
            return string.Equals(source, target, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Check that string contains with StringComparison.InvariantCultureIgnoreCase
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static bool DoContains(this string source, string target)
        {
            return source == null || target == null ? false : source.Contains(target, StringComparison.InvariantCultureIgnoreCase);
        }

        public static SecureString ToSecureString(this string value)
        {
            Ensure.Argument.NotNull(value, "value");

            var secureString = new SecureString();
            foreach (var c in value)
                secureString.AppendChar(c);

            return secureString;
        }

        /// <summary>
        ///     A nicer way of calling <see cref="System.String.IsNullOrEmpty(string)" />
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is null or an empty string (""); otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        ///     A nicer way of calling the inverse of <see cref="System.String.IsNullOrEmpty(string)" />
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>true if the value parameter is not null or an empty string (""); otherwise, false.</returns>
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !value.IsNullOrEmpty();
        }

        /// <summary>
        ///     A nicer way of calling <see cref="System.String.Format(string, object[])" />
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>
        ///     A copy of format in which the format items have been replaced by the string representation of the
        ///     corresponding objects in args.
        /// </returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        ///     Allows for using strings in null coalescing operations
        /// </summary>
        /// <param name="value">The string value to check</param>
        /// <returns>Null if <paramref name="value" /> is empty or the original value of <paramref name="value" />.</returns>
        public static string NullIfEmpty(this string value)
        {
            if (value == string.Empty)
                return null;

            return value;
        }

        /// <summary>
        ///     Slugifies a string
        /// </summary>
        /// <param name="value">The string value to slugify</param>
        /// <param name="maxLength">An optional maximum length of the generated slug</param>
        /// <returns>A URL safe slug representation of the input <paramref name="value" />.</returns>
        public static string ToSlug(this string value, int? maxLength = null)
        {
            Ensure.Argument.NotNull(value, "value");

            // if it's already a valid slug, return it
            if (RegexUtils.SlugRegex.IsMatch(value))
                return value;

            return GenerateSlug(value, maxLength);
        }

        /// <summary>
        ///     Converts a string into a slug that allows segments e.g.
        ///     <example>.blog/2012/07/01/title</example>
        ///     .
        ///     Normally used to validate user entered slugs.
        /// </summary>
        /// <param name="value">The string value to slugify</param>
        /// <returns>A URL safe slug with segments.</returns>
        public static string ToSlugWithSegments(this string value)
        {
            Ensure.Argument.NotNull(value, "value");

            var segments = value.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var result = segments.Aggregate(string.Empty, (slug, segment) => slug + ("/" + segment.ToSlug()));
            return result.Trim('/');
        }

        /// <summary>
        ///     Separates a PascalCase string
        /// </summary>
        /// <example>
        ///     "ThisIsPascalCase".SeparatePascalCase(); // returns "This Is Pascal Case"
        /// </example>
        /// <param name="value">The value to split</param>
        /// <returns>The original string separated on each uppercase character.</returns>
        public static string SeparatePascalCase(this string value)
        {
            Ensure.Argument.NotNullOrEmpty(value, "value");
            return Regex.Replace(value, "([A-Z])", " $1").Trim();
        }

        /// <summary>
        ///     Credit for this method goes to http://stackoverflow.com/questions/2920744/url-slugify-alrogithm-in-cs
        /// </summary>
        private static string GenerateSlug(string value, int? maxLength = null)
        {
            // prepare string, remove accents, lower case and convert hyphens to whitespace
            var result = RemoveAccent(value).Replace("-", " ").ToLowerInvariant();

            result = Regex.Replace(result, @"[^a-z0-9\s-]", string.Empty); // remove invalid characters
            result = Regex.Replace(result, @"\s+", " ").Trim(); // convert multiple spaces into one space

            if (maxLength.HasValue) // cut and trim
                result = result.Substring(0, result.Length <= maxLength ? result.Length : maxLength.Value).Trim();

            return Regex.Replace(result, @"\s", "-"); // replace all spaces with hyphens
        }

        /// <summary>
        ///     Returns a string array containing the trimmed substrings in this <paramref name="value" />
        ///     that are delimited by the provided <paramref name="separators" />.
        /// </summary>
        public static IEnumerable<string> SplitAndTrim(this string value, params char[] separators)
        {
            Ensure.Argument.NotNull(value, "source");
            return value.Trim().Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
        }

        /// <summary>
        ///     Checks if the <paramref name="source" /> contains the <paramref name="input" /> based on the provided
        ///     <paramref name="comparison" /> rules.
        /// </summary>
        public static bool Contains(this string source, string input, StringComparison comparison)
        {
            return source.IndexOf(input, comparison) >= 0;
        }


        private static string RemoveAccent(string value)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}