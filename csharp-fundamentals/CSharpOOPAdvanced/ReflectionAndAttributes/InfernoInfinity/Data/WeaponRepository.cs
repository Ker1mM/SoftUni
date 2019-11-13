using InfernoInfinity.Contracts;
using InfernoInfinity.Data.CustomAttributes;
using InfernoInfinity.Factories;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InfernoInfinity.Data
{
    [Info]
    class WeaponRepository : IEnumerable<IWeapon>
    {
        private IList<IWeapon> weapons;

        public WeaponRepository()
        {
            this.weapons = new List<IWeapon>();
        }

        public void AddWeapon(string[] args)
        {
            var weapon = WeaponFactory.CreateWeapon(args);
            if (!this.weapons.Any(x => x.Name == weapon.Name))
            {
                this.weapons.Add(weapon);
            }
        }

        public void AddGem(string[] args)
        {
            string weaponName = args[0];
            int index = int.Parse(args[1]);
            var gem = GemFactory.CreateGem(args.Skip(2).ToArray());
            if (this.weapons.Any(x => x.Name == weaponName))
            {
                this.weapons.FirstOrDefault(x => x.Name == weaponName).AddGem(index, gem);
            }
        }

        public void RemoveGem(string[] args)
        {
            string name = args[0];
            int index = int.Parse(args[1]);
            if (this.weapons.Any(x => x.Name == name))
            {
                this.weapons.FirstOrDefault(x => x.Name == name).RemoveGem(index);
            }
        }

        public string GetWeaponInfo(string[] args)
        {
            string name = args[0];
            string result = "No weapon with such name!";
            if (this.weapons.Any(x => x.Name == name))
            {
                var weapon = this.weapons.FirstOrDefault(x => x.Name == name);
                result = $"{weapon.Name}: {weapon.GetMinDamage()}-{weapon.GetMaxDamage()} Damage, " +
                    $"+{weapon.GetStrength()} Strength, +{weapon.GetAgility()} Agility, +{weapon.GetVitality()} Vitality";
            }

            return result;
        }

        public IEnumerator<IWeapon> GetEnumerator()
        {
            foreach (var weapon in this.weapons)
            {
                yield return weapon;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
