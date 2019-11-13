using CustomLinkedList;
using NUnit.Framework;
using System;

namespace P03_CustomLinkedList.Tests
{
    [TestFixture]
    public class CustomLinkedListTests
    {
        private const int InitialCount = 0;
        private DynamicList<int> testList;

        [SetUp]
        public void TestInit()
        {
            this.testList = new DynamicList<int>();
        }

        [Test]
        public void ConstructorInitializesWithCountZero()
        {
            Assert.That(testList.Count, Is.EqualTo(InitialCount));
        }

        [Test]
        public void IndexOperatorGetterThrowsWhenIndexIsOutOfRange()
        {
            testList.Add(15);

            Assert.That(() => testList[1], Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void IndexOperatorSetterThrowsWhenIndexIsOutOfRange()
        {
            testList.Add(15);

            Assert.That(() => testList[1] = 2, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void IndexOperatorGetsCorrectValue()
        {
            testList.Add(15);
            testList.Add(20);

            Assert.That(testList[1], Is.EqualTo(20));
        }

        [Test]
        public void IndexOperatorSetsCorrectValue()
        {
            testList.Add(15);
            testList.Add(20);
            testList.Add(25);

            testList[1] = -200;

            Assert.That(testList[1], Is.EqualTo(-200));
        }

        [Test]
        public void AddShouldAddelementToTheEndOfList()
        {
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            int lastElement = testList[testList.Count - 1];

            Assert.That(lastElement, Is.EqualTo(4));
        }

        [Test]
        public void AddIncreasesCount()
        {
            for (int i = 0; i < 100; i++)
            {
                testList.Add(i);
            }

            Assert.That(testList.Count, Is.EqualTo(100));
        }

        [Test]
        public void AddWorksWithEmptyList()
        {
            testList.Add(1);

            Assert.That(testList[0], Is.EqualTo(1));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(10)]
        public void RemoveAtThrowsWhenIndexIsOutOfRange(int index)
        {
            testList.Add(1);

            Assert.Throws<ArgumentOutOfRangeException>(() => testList.RemoveAt(index));
        }

        [Test]
        [TestCase(0)]
        [TestCase(3)]
        public void RemoveAtReturnCorrectIndexValue(int index)
        {
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(1);

            Assert.That(testList[index], Is.EqualTo(1));
        }

        [Test]
        public void RemoveAtCorrectlyDecreasesCount()
        {
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            testList.RemoveAt(0);
            testList.RemoveAt(0);

            Assert.That(testList.Count, Is.EqualTo(2));
        }

        [Test]
        public void RemoveAtRemovesCorrectElement()
        {
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);
            testList.Add(5);

            testList.RemoveAt(2);

            Assert.That(testList[2], Is.EqualTo(4));
        }

        [Test]
        public void RemoveReturnsMinusOneIfElementNotFound()
        {
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            Assert.That(testList.Remove(5), Is.EqualTo(-1));
        }

        [Test]
        public void RemoveReturnsCorrectIndex()
        {
            testList.Add(11);
            testList.Add(-22);
            testList.Add(32231);
            testList.Add(-44);
            testList.Add(-44);

            Assert.That(testList.Remove(-44), Is.EqualTo(3));
        }

        [Test]
        public void RemoveCorrectlyDecreasesCount()
        {
            testList.Add(1);
            testList.Add(2);
            testList.Add(2);
            testList.Add(2);

            testList.Remove(2);

            Assert.That(testList.Count, Is.EqualTo(3));
        }

        [Test]
        public void RemoveDeletesCorrectItem()
        {
            testList.Add(1);
            testList.Add(2);
            testList.Add(1);
            testList.Add(3);

            testList.Remove(1);

            Assert.That(testList[0], Is.EqualTo(2));
        }

        [Test]
        public void IndexOfReturnsMinusOneIfItemDoesNotExistInTheList()
        {
            Assert.That(testList.IndexOf(1), Is.EqualTo(-1));
        }

        [Test]
        [TestCase(5, 1)]
        [TestCase(2, 0)]
        [TestCase(10, 4)]
        public void IndexOfReturnsCorrectIndex(int element, int expected)
        {
            testList.Add(2);
            testList.Add(5);
            testList.Add(4);
            testList.Add(5);
            testList.Add(10);

            Assert.That(testList.IndexOf(element), Is.EqualTo(expected));
        }

        [Test]
        [TestCase(2, true)]
        [TestCase(5, true)]
        [TestCase(12, false)]
        public void ContainsReturnsCorrectValue(int element, bool expected)
        {
            testList.Add(2);
            testList.Add(5);
            testList.Add(4);
            testList.Add(5);
            testList.Add(10);

            Assert.That(testList.Contains(element), Is.EqualTo(expected));
        }

    }
}
