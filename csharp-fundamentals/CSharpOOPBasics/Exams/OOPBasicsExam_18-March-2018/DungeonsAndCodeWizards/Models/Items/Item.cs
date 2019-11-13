using DungeonsAndCodeWizards.Messages;
using DungeonsAndCodeWizards.Models.Characters;
using System;

namespace DungeonsAndCodeWizards.Models.Items
{
    public abstract class Item
    {
        private int weight;

        public int Weight
        {
            get
            {
                return this.weight;
            }
            private set
            {
                this.weight = value;
            }
        }

        protected Item(int weight)
        {
            this.Weight = weight;
        }

        public virtual void AffectCharacter(Character character)
        {
            character.Alive();
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
