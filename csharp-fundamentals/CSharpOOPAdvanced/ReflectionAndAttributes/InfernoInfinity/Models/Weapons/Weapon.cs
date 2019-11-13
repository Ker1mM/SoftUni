using InfernoInfinity.Contracts;
using InfernoInfinity.Data;
using InfernoInfinity.Models.Gems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfernoInfinity.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
        private Gem[] gems;
        private Rarity rarity;
        private int minDamage;
        private int maxDamage;
        private int strength;
        private int agility;
        private int vitality;

        public string Name { get; private set; }
        public int SocketCount { get; private set; }

        protected Weapon(string name, Rarity rarity, int minDamage, int maxDamage, int socketCount)
        {
            this.Name = name;
            this.rarity = rarity;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
            this.SocketCount = socketCount;
            this.strength = 0;
            this.agility = 0;
            this.vitality = 0;
            this.gems = new Gem[socketCount];
        }

        public int GetStrength()
        {
            int result = this.gems.Where(x => x != null).Sum(x => x.Strength);

            return result;
        }

        public int GetAgility()
        {
            int result = this.gems.Where(x => x != null).Sum(x => x.Agility);

            return result;
        }

        public int GetVitality()
        {
            int result = this.gems.Where(x => x != null).Sum(x => x.Vitality);

            return result;
        }

        public int GetMaxDamage()
        {
            int result = (this.maxDamage * (int)this.rarity);
            result += this.GetStrength() * 3;
            result += this.GetAgility() * 4;

            return result;
        }

        public int GetMinDamage()
        {
            int result = (this.minDamage * (int)this.rarity);
            result += this.GetStrength() * 2;
            result += this.GetAgility() * 1;

            return result;
        }

        public void AddGem(int index, Gem gem)
        {
            if (index >= 0 && index < this.gems.Length)
            {
                this.gems[index] = gem;
            }
        }

        public void RemoveGem(int index)
        {
            if (index >= 0 && index < this.gems.Length)
            {
                this.gems[index] = null;
            }
        }


    }
}