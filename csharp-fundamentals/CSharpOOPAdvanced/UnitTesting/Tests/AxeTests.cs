using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestFixture]
    class AxeTests
    {
        [Test]
        public void TestWeaponLosingDurabilityAfterAttack()
        {
            Axe axe = new Axe(10, 10);
            Dummy dummy = new Dummy(100, 100);

            axe.Attack(dummy);

            Assert.That(axe.DurabilityPoints, Is.EqualTo(9), "Axe durability does not lower by 1 after attack!");
        }

        [Test]
        public void TestAttackingWithBrokenWeapon()
        {
            Axe axe = new Axe(10, 0);
            Dummy dummy = new Dummy(10, 10);

            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
        }
    }
}
