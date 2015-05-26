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
    public class EnumerableExtensionsTest
    {
        IEnumerable<int> collection;

        [SetUp]
        public void Setup()
        {
            collection = new List<int>() { 1, 2, 3, 4 };
        }

        [Test]
        public void ShouldPerformActionForEachItemInCollection()
        {            
            int collectionSum = 0;
            collection.ForEach<int>(i => collectionSum = collectionSum + i);
            collection.Sum().Should().Be(collectionSum);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptionIfSourceIsNullForEachItemMethod()
        {
            IEnumerable<int> collection = null;
            collection.ForEach(i => i++);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptionIfActionIsNullForEachItemMethod()
        {            
            collection.ForEach(null);
        }

        [Test]
        public void ShouldReturnTrueIfAllElementsInCollectionAreEqual()
        {
            IEnumerable<int> collection = new List<int>() { 1, 1, 1, 1 };
            collection.IsAllEqual().Should().BeTrue();
        }

        [Test]
        public void ShouldReturnFalseIfAllElementsInCollectionAreNotEqual()
        {
            IEnumerable<int> collection = new List<int>() { 1, 1, 1, 2 };
            collection.IsAllEqual().Should().BeFalse();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptionIfSourceIsNullForIsAllEqual()
        {
            IEnumerable<int> collection = null;
            collection.IsAllEqual();
        }

        [Test]
        public void ShouldReturnTrueIfAllStringElementsInCollectionAreEqual()
        {
            IEnumerable<string> collection = new List<string>() { "this", "this", "this" };
            collection.IsAllEqual().Should().BeTrue();
        }

        [Test]
        public void ShouldReturnCollectionFromWhereTheElementHasBeenFound()
        {
            var filteredCollection = collection.AllBeforeFirstOccuranceOf(i => i == 2);
            filteredCollection.Count().Should().Be(1);
        }

    }
}
