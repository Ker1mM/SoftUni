using InfernoInfinity.Contracts;
using InfernoInfinity.Data;
using System;

namespace InfernoInfinity.Factories
{
    class WeaponFactory
    {
        public static IWeapon CreateWeapon(string[] args)
        {
            string[] weaponInfo = args[0].Split();
            Rarity rarity = (Rarity)Enum.Parse(typeof(Rarity), weaponInfo[0]);

            Type type = Type.GetType("InfernoInfinity.Models.Weapons." + weaponInfo[1]);

            var obj = Activator.CreateInstance(type, new object[] { args[1], rarity });

            return (IWeapon)obj;
        }
    }
}
