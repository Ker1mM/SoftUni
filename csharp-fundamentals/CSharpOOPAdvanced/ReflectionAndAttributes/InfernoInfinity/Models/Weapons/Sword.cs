using System;
using System.Collections.Generic;
using System.Text;
using InfernoInfinity.Data;

namespace InfernoInfinity.Models.Weapons
{
    public class Sword : Weapon
    {
        private const int baseMinDamage = 4;
        private const int baseMaxDamage = 6;
        private const int baseSocketCount = 3;

        public Sword(string name, Rarity rarity)
            : base(name, rarity, baseMinDamage, baseMaxDamage, baseSocketCount)
        {
        }
    }
}
