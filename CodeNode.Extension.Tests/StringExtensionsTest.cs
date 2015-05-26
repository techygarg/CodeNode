using NUnit.Framework;
using System;
using System.Collections.Generic;
using FluentAssertions;

namespace CodeNode.Extension.Tests
{
    [TestFixture]
    public class StringExtensionsTest
    {

        private const string Empty = "";

        [TestCase("my string", "my string", true)]
        [TestCase("my string", "my String", true)]
        [TestCase("my string", "mystring", false)]
        [TestCase(Empty, Empty, true)]
        [TestCase(Empty, null, false)]
        [TestCase(null, null, true)]
        public void ShouldReturnResultWhenComparingSourceAndTargetString(string source, string target, bool testResult)
        {
            var result = source.IsEqual(target);
            result.Should().Be(testResult);
        }

        [TestCase("my string is this", "my string", true)]
        [TestCase("my String is this", "my string", true)]
        [TestCase("my String is this", "mystring", false)]
        [TestCase(Empty, Empty, true)]
        [TestCase(Empty, null, false)]
        [TestCase(null, null, false)]
        [TestCase("my string is this", null, false)]
        [TestCase(null, "my string is this", false)]
        public void ShouldReturnResultWhenSourceContainsTargetString(string source, string target, bool testResult)
        {
            var result = source.DoContains(target);
            result.Should().Be(testResult);
        }

        [TestCase("my string is this", 17)]
        public void ShouldReturnResultForSecuredString(string str, int result)
        {
            str.ToSecureString().Length.Should().Be(result);
        }

        [TestCase("my string is this", "my string is this")]
        [TestCase(Empty, null)]
        public void ShouldReturnResultWhenStringIsEmptyNullOrNonNull(string str, string result)
        {
            str.NullIfEmpty().Should().Be(result);
        }

        [Test]
        public void ShouldReturnResultWhenSplittingAndTrimmingString()
        {
            var inputString = " hi;my;name ;is, ,;test ";
            var splitterArray = new[] { ';', ',' };
            inputString.SplitAndTrim(splitterArray).Should().Equal(new List<string>() { "hi", "my", "name", "is", string.Empty, "test" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptionWhenSplittingAndTrimmingNullString()
        {
            string inputString = null;
            inputString.SplitAndTrim();
        }

        [TestCase("hi this is test string. will this pass?", "hi-this-is-test-string-will-this-pass", null)]
        [TestCase("hi this is test string. will this pass?", "hi-this", 8)]
        [TestCase("hi this is test string. will this pass?", "hi", 3)]
        [TestCase("hi this is test string. will this pass?", "hi", 2)]
        [TestCase("hi this is test string. will this pass?", Empty, 0)]
        [TestCase("hi-this-is-test-string-will-this-pass", "hi-this-is-test-string-will-this-pass", null)]
        public void ShouldReturnUrlSlugFromTheStringPassed(string input, string expected, int? maxSize)
        {
            input.ToSlug(maxSize).Should().Be(expected);
        }

        [TestCase("ThisIsMyTest", "This Is My Test")]
        [TestCase("ThisisMyTest", "Thisis My Test")]
        [TestCase("ThisisMy Test", "Thisis My Test"), Ignore("This scenario needs to be fixed. Currently it is given two spaces which is undesirable.")]
        public void ShouldSeparatePascalCaseString(string input, string output)
        {
            input.SeparatePascalCase().Should().Be(output);
        }

        [TestCase(Empty)]
        [TestCase(null)]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionWhenStringIsNullOrEmptyForSeparatePascalCase(string input)
        {
            input.SeparatePascalCase();
        }
    }
}
