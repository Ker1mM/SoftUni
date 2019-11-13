using NUnit.Framework;
using P02_ExtendedDatabase;
using System;

namespace P02_ExtendedDatabase.Tests
{
    [TestFixture]
    class ExtendedDatabaseTests
    {
        Person testPerson1;
        Person testPerson2;

        [SetUp]
        public void TestInit()
        {
            this.testPerson1 = new Person("Pesho", 123456);
            this.testPerson2 = new Person("Gosho", 1234567890);
        }

        [Test]
        public void ConstructorCorrectlyInitializesNewDatabase()
        {
            var expected = new Person[] { testPerson1, testPerson2 };

            Database db = new Database(testPerson1, testPerson2);

            Assert.That(db.Fetch(), Is.EqualTo(expected));
        }

        [Test]
        public void AddCorrectlyAddsPersonToTheEnd()
        {
            var db = new Database(testPerson1);

            db.Add(testPerson2);

            var expected = db.Fetch()[1];

            Assert.That(expected, Is.EqualTo(testPerson2));
        }

        [Test]
        public void AddThrowWhenUsernameAlreadyExists()
        {
            var db = new Database(testPerson1);
            var sameUsernamePerson = new Person(testPerson1.Name, testPerson1.Id + 1);

            Assert.That(() => db.Add(sameUsernamePerson), Throws.InvalidOperationException);
        }

        [Test]
        public void AddThrowWhenIdAlreadyExists()
        {
            var db = new Database(testPerson1);
            var sameIdPerson = new Person("Test", testPerson1.Id);

            Assert.That(() => db.Add(sameIdPerson), Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveCorrectlyRemovesLastElement()
        {
            Database db = new Database(testPerson1, testPerson2);
            Person[] expected = new Person[] { testPerson1 };

            db.Remove();

            Assert.That(db.Fetch(), Is.EqualTo(expected));
        }

        [Test]
        public void RemoveReturnsLastIndexElement()
        {
            Database db = new Database(testPerson2, testPerson1);

            Assert.That(db.Remove(), Is.EqualTo(testPerson1));
        }


        [Test]
        public void RemoveThrowsOnEmptyDatabase()
        {
            Database db = new Database();

            Assert.Throws<InvalidOperationException>(() => db.Remove());
        }

        [Test]
        public void FindByUsernameThrowWhenParameterDoesNotExist()
        {
            var db = new Database(testPerson1);

            Assert.Throws<InvalidOperationException>(() => db.FindByUsername("TestUsername"));
        }

        [Test]
        public void FindByUsernameThrowWhenParameterIsNull()
        {
            var db = new Database(testPerson1);
            string nullUsername = null;

            Assert.Throws<ArgumentNullException>(() => db.FindByUsername(nullUsername));
        }

        [Test]
        public void FindByUsernameParameterIsCaseSensitive()
        {
            var db = new Database(testPerson2, testPerson1);

            Assert.Throws<InvalidOperationException>(() => db.FindByUsername("pesho"));
        }

        [Test]
        public void FindByUsernameCorrectlyReturnsPerson()
        {
            var db = new Database(testPerson1, testPerson2);

            Assert.That(db.FindByUsername("Pesho"), Is.EqualTo(testPerson1));
        }

        [Test]
        public void FindByIdCorrectlyReturnsPerson()
        {
            var db = new Database(testPerson1, testPerson2);

            Assert.That(db.FindById(123456), Is.EqualTo(testPerson1));
        }

        [Test]
        public void FindByIdThrowWhenParameterDoesNotExist()
        {
            var db = new Database(testPerson1);

            Assert.Throws<InvalidOperationException>(() => db.FindById(1));
        }

        [Test]
        public void FindByIdThrowWhenParameterIsNegative()
        {
            var db = new Database(testPerson1);

            Assert.Throws<ArgumentOutOfRangeException>(() => db.FindById(-1));
        }
    }
}
