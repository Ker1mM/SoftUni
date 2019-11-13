using InfernoInfinity.Models.OtherClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfernoInfinity.Models.Gems
{
    public class Emerald : Gem
    {
        private const int strenght = 1;
        private const int agility = 4;
        private const int vitality = 9;

        public Emerald() : base(strenght, agility, vitality)
        {
        }
    }
}
