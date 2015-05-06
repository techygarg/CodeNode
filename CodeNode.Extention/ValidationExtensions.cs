using System.Text.RegularExpressions;
using CodeNode.Core.Utils;

namespace CodeNode.Extention
{
    // Credits: Ben-Foster

    public static class ValidationExtensions
    {
        /// <summary>
        ///     Validates whether the provided
        ///     <param name="value">string</param>
        ///     is a valid slug.
        /// </summary>
        public static bool IsValidSlug(this string value)
        {
            return Match(value, RegexUtils.SlugRegex);
        }

        /// <summary>
        ///     Validates whether the provided
        ///     <param name="value">string</param>
        ///     is a valid (absolute) URL.
        /// </summary>
        public static bool IsValidUrl(this string value) // absolute
        {
            return Match(value, RegexUtils.UrlRegex);
        }

        /// <summary>
        ///     Validates whether the provided
        ///     <param name="value">string</param>
        ///     is a valid Email Address.
        /// </summary>
        public static bool IsValidEmail(this string value)
        {
            return Match(value, RegexUtils.EmailRegex);
        }

        /// <summary>
        ///     Validates whether the provided
        ///     <param name="value">string</param>
        ///     is a valid IP Address.
        /// </summary>
        public static bool IsValidIpAddress(this string value)
        {
            return Match(value, RegexUtils.IpAddressRegex);
        }

        private static bool Match(string value, Regex regex)
        {
            return value.IsNotNullOrEmpty() || regex.IsMatch(value);
        }
    }
}