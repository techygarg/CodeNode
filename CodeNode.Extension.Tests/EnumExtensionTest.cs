using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using SystemDesc = System.ComponentModel;
using NUnit.Framework;

namespace CodeNode.Extension.Tests
{
    [TestFixture]
    public class EnumExtensionTest
    {

        enum TestEnumType
        {
            None = 0,
            [SystemDesc.Description("Test Enum 1")]
            TestEnum1 = 1,
            TestEnum2 = 2,
            TestEnum3 = 3
        }

        struct MyStruct
        {
            
        }

        [Test]
        public void ShouldReturnDictionaryForAnEnum()
        {
            var dictionary = EnumExtensions.ToDictionary<TestEnumType>();

            dictionary.Count().Should().Be(4);
            dictionary[1].Should().Be("TestEnum1");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfAnEnumTypeIsNotPassedForToDictionary()
        {
            EnumExtensions.ToDictionary<MyStruct>();
        }

        [Test]
        public void ShouldReturnEnumFromString()
        {
            var enumType = "TestEnum1".ToEnum<TestEnumType>();
            enumType.Should().Be(TestEnumType.TestEnum1);
        }

        [Test]
        public void ShouldReturnNoneWhenStringIsNotPresentInEnum()
        {
            var enumType = "TestEnum4".ToEnum<TestEnumType>();
            enumType.Should().Be(TestEnumType.None);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldThrowExceptionIfAnEnumTypeIsNotPassedForToEnum()
        {
            var enumType = "TestEnum4".ToEnum<MyStruct>();            
        }

        [Test]
        public void ShouldGetDescriptionOfEnum()
        {
            var desc = TestEnumType.TestEnum1.GetDescription();
            desc.Should().Be("Test Enum 1");
        }

        [Test]
        public void ShouldReturnEnumNameIfDescriptionOfEnumIsNotApplied()
        {
            var desc = TestEnumType.TestEnum2.GetDescription();
            desc.Should().Be(TestEnumType.TestEnum2.ToString());
        }
    }
}
