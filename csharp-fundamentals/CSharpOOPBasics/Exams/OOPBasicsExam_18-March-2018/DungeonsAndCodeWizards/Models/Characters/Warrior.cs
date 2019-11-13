using DungeonsAndCodeWizards.Enums;
using DungeonsAndCodeWizards.Interfaces;
using DungeonsAndCodeWizards.Messages;
using DungeonsAndCodeWizards.Models.Bags;
using System;

namespace DungeonsAndCodeWizards.Models.Characters
{
    public class Warrior : Character, IAttackable
    {
        public Warrior(string name, Faction faction) : base(name, 100, 50, 40, new Satchel(), faction)
        {
        }

        public void Attack(Character character)
        {
            this.Alive();
            character.Alive();
            if (character == this)
            {
                throw new InvalidOperationException(OutputMessages.CantAttackSelf());
            }
            if (character.Faction == this.Faction)
            {
                throw new ArgumentException(string.Format(OutputMessages.SameFaction(), this.Faction));
            }

            character.TakeDamage(this.AbilityPoints);
        }
    }
}
