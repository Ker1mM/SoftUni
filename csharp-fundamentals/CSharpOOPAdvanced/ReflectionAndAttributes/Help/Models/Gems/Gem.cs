using InfernoInfinity.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfernoInfinity.Models.OtherClasses
{
    public abstract class Gem : IGem
    {
       
        protected Gem(int strenght, int agility, int vitality)
        {
            this.Strenght = strenght;
            this.Agility = agility;
            this.Vitality = vitality;
        }
        private int strenght;
        private int agility;
        private int vitality;
        
        public int Strenght { get { return strenght; } protected set { this.strenght = value; } }
        public int Agility { get { return this.agility; } protected set { this.agility = value; } }
        public int Vitality { get { return this.vitality; } protected set { this.vitality = value; } }

    }
}
