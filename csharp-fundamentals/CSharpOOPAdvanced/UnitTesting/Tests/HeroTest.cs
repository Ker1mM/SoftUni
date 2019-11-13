using Moq;
using NUnit.Framework;
using Skeleton.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Fakes;

namespace Tests
{
    [TestFixture]
    class HeroTest
    {

        [Test]
        public void HeroGainsXPWhenTargetDies()
        {
            ITarget fakeTarget = new FakeTarget(10, 10);
            IWeapon fakeWeapon = new FakeWeapon(10, 10);

            Hero hero = new Hero("Pesho", fakeWeapon);

            hero.Attack(fakeTarget);

            Assert.That(hero.Experience, Is.EqualTo(10));
        }

        [Test]
        public void HeroGainsXPWhenTargetDiesWithMock()
        {
            Mock<ITarget> fakeTarget = new Mock<ITarget>();
            fakeTarget.Setup(p => p.Health).Returns(0);
            fakeTarget.Setup(p => p.GiveExperience()).Returns(10);
            fakeTarget.Setup(p => p.IsDead()).Returns(true);

            Mock<IWeapon> fakeWeapon = new Mock<IWeapon>();

            Hero hero = new Hero("Pesho", fakeWeapon.Object);
            hero.Attack(fakeTarget.Object);

            Assert.That(hero.Experience, Is.EqualTo(10));
        }
    }
}
