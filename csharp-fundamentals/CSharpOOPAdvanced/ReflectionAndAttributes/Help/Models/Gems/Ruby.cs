using InfernoInfinity.Models.OtherClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfernoInfinity.Models.Gems
{
    public class Ruby : Gem
    {
        private const int strenght = 7;
        private const int agility = 2;
        private const int vitality = 5;
        
        public Ruby() : base(strenght, agility, vitality)
        {
        }
    }
}
