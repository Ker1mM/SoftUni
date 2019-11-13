using System;
using System.Collections.Generic;
using System.Text;
using InfernoInfinity.Contracts;
using InfernoInfinity.Enums;
using InfernoInfinity.Models.OtherClasses;

namespace InfernoInfinity.Models.Weapons
{
    public class Knife : Weapon
    {
        private static IWeaponState weaponState = new WeaponState(3, 4, new IGem[2]);

        public Knife(string name, Rarities rarity) : base(name, weaponState, rarity)
        {
        }
    }
}
