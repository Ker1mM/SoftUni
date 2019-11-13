using DungeonsAndCodeWizards.Enums;
using DungeonsAndCodeWizards.Interfaces;
using DungeonsAndCodeWizards.Messages;
using DungeonsAndCodeWizards.Models.Bags;
using System;

namespace DungeonsAndCodeWizards.Models.Characters
{
    public class Cleric : Character, IHealable
    {
        public Cleric(string name, Faction faction) : base(name, 50, 25, 40, new Backpack(), faction)
        {
            base.RestHealMultiplier = 0.5;
        }

        public void Heal(Character character)
        {
            this.Alive();
            character.Alive();

            if (this.Faction != character.Faction)
            {
                throw new InvalidOperationException(OutputMessages.DifferentFaction());
            }

            character.HealCharacter(this.AbilityPoints);
        }
    }
}
