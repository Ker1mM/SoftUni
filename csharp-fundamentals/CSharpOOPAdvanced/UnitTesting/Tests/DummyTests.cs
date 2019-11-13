using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    class DummyTests
    {
        private const int AxeAttack = 10;
        private const int AxeDurability = 2;
        private const int DummyHealth = 10;
        private const int DummyXP = 10;
        private Axe axe;
        private Dummy dummy;

        [SetUp]
        public void TestInit()
        {
            this.axe = new Axe(AxeAttack, AxeDurability);
            this.dummy = new Dummy(DummyHealth, DummyXP);
        }

        [Test]
        public void DummyLosesHealthIfAttacked()
        {
            axe.Attack(dummy);

            Assert.That(dummy.Health, Is.EqualTo(0));
        }

        [Test]
        public void DeadDummyThrowsExceptionIfAttacked()
        {
            axe.Attack(dummy);

            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
        }

        [Test]
        public void DeadDummyGivesXp()
        {
            axe.Attack(dummy);

            Assert.That(dummy.GiveExperience, Is.EqualTo(10));
        }

        [Test]
        public void AliveDummyCantGiveXp()
        {
            Assert.That(dummy.GiveExperience, Throws.InvalidOperationException.With.Message.EqualTo("Target is not dead."));
        }
    }
}
