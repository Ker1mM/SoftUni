using InfernoInfinity.Contracts;
using InfernoInfinity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace InfernoInfinity.Core
{
    public class InfinityInferno : IInfinityInferno
    {
        public InfinityInferno()
        {
            this.weapons = new List<IWeapon>();
        }

        private List<IWeapon> weapons;
        public IReadOnlyCollection<IWeapon> Weapons { get { return this.weapons; } }


        public void Add(string[] args)
        {
            string weaponName = args[1];
            int socketIndex = int.Parse(args[2]);
            string gemType = args[3];

            IWeapon weapon = this.Weapons.FirstOrDefault(x => x.Name == weaponName);
            if (weapon != null)
            {
                int weaponIndex = this.weapons.IndexOf(weapon);

                weapon.AddingGem(socketIndex, gemType);

                this.weapons[weaponIndex] = weapon;
            }
            
        }

        public void Create(string[] args)
        {
            string[] weaponTypeInfo = args[1].Split();
            Rarities weaponRairity = new Rarities() ;

            if (Enum.TryParse(weaponTypeInfo[0],out Rarities rarities))
            {
                weaponRairity = rarities;
            }
            Type weaponType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == weaponTypeInfo[1]);
            if (weaponType != null)
            {
                string weaponName = args[2];
                IWeapon instance = (IWeapon)Activator.CreateInstance(weaponType, new object[] { weaponName, weaponRairity });

                weapons.Add(instance);
            }
            

        }

        public void Remove(string[] args)
        {
            string weaponName = args[1];
            int socketIndex = int.Parse(args[2]);

            IWeapon weapon = this.Weapons.FirstOrDefault(x => x.Name == weaponName);
            if (weapon != null)
            {
                int weaponIndex = this.weapons.IndexOf(weapon);

                weapon.RemovingGem(socketIndex);
                this.weapons[weaponIndex] = weapon;
            }
            

        }

        public void Print(string[] args)
        {
            string weaponName = args[1];

            IWeapon weapon = this.Weapons.FirstOrDefault(x => x.Name == weaponName);
            if (weapon != null)
            {
                Console.WriteLine(weapon.Print());
            }
            
        }
    }
}
