using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CodeNode.Extension.Tests
{
    [TestFixture]
    public class StringExtensionsTest
    {

        private const string EMPTY = "";

        [TestCase("my string", "my string", true)]
        [TestCase("my string", "my String", true)]
        [TestCase("my string", "mystring", false)]
        [TestCase(EMPTY, EMPTY, true)]
        [TestCase(EMPTY, null, false)]
        [TestCase(null, null, true)]
        public void ShouldReturnResultWhenComparingSourceAndTargetString(string source, string target, bool testResult)
        {
            var result = source.IsEqual(target);
            result.Should().Be(testResult);
        }

        [TestCase("my string is this", "my string", true)]
        [TestCase("my String is this", "my string", true)]
        [TestCase("my String is this", "mystring", false)]
        [TestCase(EMPTY, EMPTY, true)]
        [TestCase(EMPTY, null, false)]
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
        [TestCase(EMPTY, null)]        
        public void ShouldReturnResultWhenStringIsEmptyNullOrNonNull(string str, string result)
        {
            str.NullIfEmpty().Should().Be(result);
        }

    }
}
