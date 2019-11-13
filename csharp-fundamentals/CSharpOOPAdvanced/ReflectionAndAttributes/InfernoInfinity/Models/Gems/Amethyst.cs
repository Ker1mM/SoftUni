using InfernoInfinity.Data;

namespace InfernoInfinity.Models.Gems
{
    public class Amethyst : Gem
    {
        private const int baseStrength = 2;
        private const int baseAgility = 8;
        private const int baseVitality = 4;

        public Amethyst(Clarity clarity) : base(clarity, baseStrength, baseAgility, baseVitality)
        {
        }
    }
}
