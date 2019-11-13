using InfernoInfinity.Contracts;
using InfernoInfinity.Enums;
using InfernoInfinity.Models.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace InfernoInfinity.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
        protected Weapon(string name, IWeaponState weaponState, Rarities rarity)
        {
            this.Name = name;
            this.Rarities = rarity;
            this.WeaponState = weaponState;
            
        }

        private string name;
        private IWeaponState weaponState;
        private Rarities rarity;

        public string Name{ get { return name; } private set { name = value; } }

        public IWeaponState WeaponState { get { return weaponState; } private set { weaponState = value; } }

        public Rarities Rarities { get { return rarity; } private set { rarity = value; } }

        public MagicalState MagicalStats { get; private set; }

        public void AddingGem(int socketIndex,string gemInfo)
        {
            if (socketIndex >= 0 && socketIndex < this.WeaponState.NumberOfSocket.Length)
            {
                string[] args = gemInfo.Split();

                GemModifiers gemModifier = new GemModifiers();
                if (Enum.TryParse(args[0], out GemModifiers modifiers))
                {
                    gemModifier = modifiers;
                }

                string gemName = args[1];

                Type gemType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == gemName);
                var instance = Activator.CreateInstance(gemType, new object[] { });
                PropertyInfo[] propetiesInfo = gemType.GetProperties();

                foreach (PropertyInfo propInfo in propetiesInfo)
                {
                    int defaultPropValue = (int)propInfo.GetValue(instance);
                    propInfo.DeclaringType.GetProperty($"{propInfo.Name}");

                    propInfo.SetValue(instance, defaultPropValue + (int)gemModifier,
                        BindingFlags.Instance | BindingFlags.NonPublic|BindingFlags.FlattenHierarchy, 
                        null, null, null);
                }

                //TODO ВЪЗМОЖНО Е КОГАТО OVERRIDЕ-ВАМ GEM-А В ОРЪЖИЕТО ДА ТРЯБВА ДА МАХНА СТАРОТО ВЪЗДЕЙСТВИЕ ВЪРХУ MagicalState-А!!
                this.WeaponState.AddGem((IGem)instance, socketIndex);
            }
        }

        public void RemovingGem(int socketIndex)
        {
            if (socketIndex >= 0 && socketIndex < this.WeaponState.NumberOfSocket.Length && this.WeaponState.NumberOfSocket[socketIndex] != null)
            {
                this.WeaponState.NumberOfSocket[socketIndex] = null;
            }
        }

        public string Print()
        {
            Type type = typeof(WeaponState);

            PropertyInfo MaxDamageInfo = type.GetProperty("MaxDamage");
            PropertyInfo MinDamageInfo = type.GetProperty("MinDamage");
            int maxDamageValue = (int)MaxDamageInfo.GetValue(this.WeaponState);
            int minDamageValue = (int)MinDamageInfo.GetValue(this.WeaponState);
            MaxDamageInfo.SetValue(this.WeaponState, maxDamageValue * (int)Rarities,BindingFlags.Instance|BindingFlags.NonPublic,null,null,null);
            MinDamageInfo.SetValue(this.WeaponState, minDamageValue * (int)Rarities, BindingFlags.Instance | BindingFlags.NonPublic, null, null, null);
            
            int minDamage = this.WeaponState.MinDamage;
            int maxDamage = this.WeaponState.MaxDamage;

            int strenght = 0;
            int agility = 0;
            int vitality = 0;

            foreach (var item in this.WeaponState.NumberOfSocket)
            {
                if (item == null)
                {
                    continue;
                }
                strenght += item.Strenght;
                agility += item.Agility;
                vitality += item.Vitality;

                minDamage += item.Strenght * 2;
                maxDamage += item.Strenght * 3;
                minDamage += item.Agility;
                maxDamage += item.Agility * 4;
            }
            MaxDamageInfo.SetValue(this.WeaponState, maxDamage, BindingFlags.Instance | BindingFlags.NonPublic, null, null, null);
            MinDamageInfo.SetValue(this.WeaponState, minDamage, BindingFlags.Instance | BindingFlags.NonPublic, null, null, null);


            return $"{this.Name}: {this.WeaponState.MinDamage}-{this.WeaponState.MaxDamage} Damage, +{strenght} Strength, +{agility} Agility, +{vitality} Vitality";
        }
    }
}
