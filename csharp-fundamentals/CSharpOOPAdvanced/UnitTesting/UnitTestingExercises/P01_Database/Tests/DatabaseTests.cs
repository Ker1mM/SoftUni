using NUnit.Framework;
using P01_Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_Database.Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        private int[] testArray;
        private int[] limitArray;
        private int[] oversizedArray;

        [SetUp]
        public void TestInit()
        {
            this.testArray = new int[] { 0, 1, 2, 3, 4, 5 };
            this.limitArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            this.oversizedArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        }

        [Test]
        public void ConstructorThrowsWithTooManyParameters()
        {
            Database db;

            Assert.Throws<InvalidOperationException>(() => db = new Database(oversizedArray));
        }

        [Test]
        public void FetchReturnsCorrectArray()
        {
            var db = new Database(testArray);

            int[] actual = db.Fetch();

            Assert.That(actual, Is.EqualTo(testArray));
        }

        [Test]
        public void AddThrowsWhenAddingTooManyElements()
        {
            Database db = new Database(limitArray);

            Assert.Throws<InvalidOperationException>(() => db.Add(16));
        }

        [Test]
        public void AddCorrectlyAddsElementsToTheEnd()
        {
            Database db = new Database(testArray);
            int[] expectedArray = new int[] { 0, 1, 2, 3, 4, 5, 6 };

            db.Add(6);

            Assert.That(db.Fetch(), Is.EqualTo(expectedArray));
        }

        [Test]
        public void RemoveCorrectlyRemovesLastElement()
        {
            Database db = new Database(testArray);
            int[] expectedArray = new int[] { 0, 1, 2, 3, 4 };

            db.Remove();

            Assert.That(db.Fetch(), Is.EqualTo(expectedArray));
        }

        [Test]
        public void RemoveReturnsLastIndexElement()
        {
            Database db = new Database(testArray);

            Assert.That(db.Remove(), Is.EqualTo(5));
        }


        [Test]
        public void RemoveThrowsOnEmptyDatabase()
        {
            Database db = new Database();

            Assert.Throws<InvalidOperationException>(() => db.Remove());
        }

        [Test]
        public void ComplexityTest()
        {
            Database db = new Database();

            db.Add(0);
            db.Add(1);
            db.Add(1);
            db.Remove();
            db.Add(2);
            db.Add(3);
            db.Add(4);
            db.Add(4);
            db.Remove();
            db.Add(5);
            db.Remove();
            db.Add(5);

            Assert.That(db.Fetch(), Is.EqualTo(testArray));
        }
    }
}
