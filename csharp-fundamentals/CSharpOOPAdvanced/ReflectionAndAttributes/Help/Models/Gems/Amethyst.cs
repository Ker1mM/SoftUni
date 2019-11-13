using InfernoInfinity.Models.OtherClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfernoInfinity.Models.Gems
{
    public class Amethyst : Gem
    {
        private const int strenght = 2;
        private const int agility = 8;
        private const int vitality = 4;

        public Amethyst() : base(strenght, agility, vitality)
        {
        }
    }
}
