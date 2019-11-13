using System;
using System.Collections.Generic;
using System.Text;
using InfernoInfinity.Contracts;
using InfernoInfinity.Enums;
using InfernoInfinity.Models.OtherClasses;

namespace InfernoInfinity.Models.Weapons
{
    public class Sword : Weapon
    {
        private static IWeaponState weaponState = new WeaponState(4, 6, new IGem[3]);
        public Sword(string name, Rarities rarity) : base(name, weaponState, rarity)
        {
        }
    }
}
