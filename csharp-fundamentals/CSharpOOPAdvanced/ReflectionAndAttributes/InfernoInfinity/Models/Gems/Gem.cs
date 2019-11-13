using InfernoInfinity.Contracts;
using InfernoInfinity.Data;

namespace InfernoInfinity.Models.Gems
{
    public abstract class Gem
    {
        private Clarity clarity;
        private int strength;
        private int agility;
        private int vitality;

        internal int Strength
        {
            get
            {
                return this.strength + (int)this.clarity;
            }
            private set
            {
                this.strength = value;
            }
        }

        internal int Agility
        {
            get
            {
                return this.agility + (int)this.clarity;
            }
            private set
            {
                this.agility = value;
            }
        }

        internal int Vitality
        {
            get
            {
                return this.vitality + (int)this.clarity;
            }
            private set
            {
                this.vitality = value;
            }
        }

        protected Gem(Clarity clarity, int strength, int agility, int vitality)
        {
            this.Strength = strength;
            this.Agility = agility;
            this.Vitality = vitality;
            this.clarity = clarity;
        }
    }
}
