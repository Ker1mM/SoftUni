using Skeleton.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Fakes
{
    public class FakeTarget : ITarget
    {
        private int health;
        private int experience;

        public FakeTarget(int health, int experience)
        {
            this.health = health;
            this.experience = experience;
        }

        public int Health => this.health;

        public int GiveExperience()
        {
            return this.experience;
        }

        public bool IsDead()
        {
            return this.health <= 0;
        }

        public void TakeAttack(int attackPoints)
        {
            this.health -= attackPoints;
        }
    }
}
