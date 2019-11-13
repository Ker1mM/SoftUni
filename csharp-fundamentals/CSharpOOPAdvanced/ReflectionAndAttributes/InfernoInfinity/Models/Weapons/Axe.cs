using System;
using System.Collections.Generic;
using System.Text;
using InfernoInfinity.Data;

namespace InfernoInfinity.Models.Weapons
{
    public class Axe : Weapon
    {
        private const int baseMinDamage = 5;
        private const int baseMaxDamage = 10;
        private const int baseSocketCount = 4;

        public Axe(string name, Rarity rarity)
            : base(name, rarity, baseMinDamage, baseMaxDamage, baseSocketCount)
        {
        }
    }
}
