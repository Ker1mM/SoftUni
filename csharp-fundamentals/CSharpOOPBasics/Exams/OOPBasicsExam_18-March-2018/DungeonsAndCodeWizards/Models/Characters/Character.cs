using DungeonsAndCodeWizards.Enums;
using DungeonsAndCodeWizards.Messages;
using DungeonsAndCodeWizards.Models.Bags;
using DungeonsAndCodeWizards.Models.Items;
using System;

namespace DungeonsAndCodeWizards.Models.Characters
{
    public abstract class Character
    {
        private string name;
        private double health;
        private double armor;

        public double AbilityPoints { get; private set; }
        public double BaseHealth { get; private set; }
        public double BaseArmor { get; private set; }

        public double RestHealMultiplier { get; protected set; } //Default 0.2

        protected Character(string name, double health, double armor, double abilityPoints, Bag bag, Faction faction)
        {
            this.Name = name;
            this.BaseHealth = health;
            this.Health = health;
            this.BaseArmor = armor;
            this.Armor = armor;
            this.AbilityPoints = abilityPoints;
            this.Bag = bag;
            this.Faction = faction;
            this.IsAlive = true;
            this.RestHealMultiplier = 0.2;
        }

        public Bag Bag { get; private set; }

        public Faction Faction { get; private set; }

        public bool IsAlive { get; private set; }

        public void Alive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException(OutputMessages.DeadCharacter());
            }
        }

        public double Armor
        {
            get { return armor; }
            private set
            {
                if (value < 0)
                {
                    this.armor = 0;
                }
                else if (value > this.BaseArmor)
                {
                    this.armor = this.BaseArmor;
                }
                else
                {
                    this.armor = value;
                }
            }
        }

        public double Health
        {
            get { return health; }
            private set
            {
                if (value <= 0)
                {
                    this.health = 0;
                    IsAlive = false;
                }
                else if (value > this.BaseHealth)
                {
                    this.health = this.BaseHealth;
                }
                else
                {
                    this.health = value;
                }

            }
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(OutputMessages.NullOrWhitespaceName());
                }
                this.name = value;
            }
        }


        public void TakeDamage(double hitPoints)
        {
            this.Alive();
            if (hitPoints > this.Armor)
            {
                hitPoints -= this.Armor;
                this.Armor = 0;
                this.Health -= hitPoints;
            }
            else
            {
                this.Armor -= hitPoints;
            }
        }

        public void TakeDirectDamage(double hitPoints)
        {
            this.Alive();
            this.Health -= hitPoints;
        }

        public void HealCharacter(double hitPoints)
        {
            this.Alive();
            this.Health += hitPoints;
        }

        public void Rest()
        {
            this.Alive();
            this.Health += this.BaseHealth * this.RestHealMultiplier;
        }

        public void RepairArmor()
        {
            this.Armor = this.BaseArmor;
        }

        public void UseItem(Item item)
        {
            item.AffectCharacter(this);
        }

        public void UseItemOn(Item item, Character character)
        {
            this.Alive();
            item.AffectCharacter(character);
        }

        public void ReceiveItem(Item item)
        {
            this.Alive();
            this.Bag.AddItem(item);
        }

        public void GiveCharacterItem(Item item, Character character)
        {
            this.Alive();
            character.Alive();
            character.ReceiveItem(item);
        }

        public override string ToString()
        {
            string status = "Dead";
            if (IsAlive)
            {
                status = "Alive";
            }
            return $"{this.Name} - HP: {this.Health}/{this.BaseHealth}, AP: {this.Armor}/{this.BaseArmor}, Status: {status}";
        }
    }
}
