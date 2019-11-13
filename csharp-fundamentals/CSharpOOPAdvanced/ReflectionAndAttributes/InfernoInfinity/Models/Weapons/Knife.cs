using System;
using System.Collections.Generic;
using System.Text;
using InfernoInfinity.Data;

namespace InfernoInfinity.Models.Weapons
{
    public class Knife : Weapon
    {
        private const int baseMinDamage = 3;
        private const int baseMaxDamage = 4;
        private const int baseSocketCount = 2;

        public Knife(string name, Rarity rarity)
            : base(name, rarity, baseMinDamage, baseMaxDamage, baseSocketCount)
        {
        }
    }
}
