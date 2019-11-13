using Skeleton.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Fakes
{
    public class FakeWeapon : IWeapon
    {
        private int attPoints;
        private int durabilityPoints;

        public FakeWeapon(int attPoints, int durabilityPoints)
        {
            this.attPoints = attPoints;
            this.durabilityPoints = durabilityPoints;
        }

        public int AttackPoints => this.attPoints;

        public int DurabilityPoints => this.durabilityPoints;

        public void Attack(ITarget target)
        {
            target.TakeAttack(this.attPoints);
        }
    }
}
