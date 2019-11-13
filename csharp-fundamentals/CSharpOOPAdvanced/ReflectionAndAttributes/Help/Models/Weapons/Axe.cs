using System;
using System.Collections.Generic;
using System.Text;
using InfernoInfinity.Contracts;
using InfernoInfinity.Enums;
using InfernoInfinity.Models.OtherClasses;

namespace InfernoInfinity.Models.Weapons
{
    public class Axe : Weapon
    {
        private static  IWeaponState weaponState = new WeaponState(5, 10, new IGem[4]);
        public Axe(string name, Rarities rarity) : base(name, weaponState, rarity)
        {
        }
    }
}
