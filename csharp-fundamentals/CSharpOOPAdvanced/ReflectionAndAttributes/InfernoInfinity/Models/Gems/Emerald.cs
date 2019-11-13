using InfernoInfinity.Data;

namespace InfernoInfinity.Models.Gems
{
    public class Emerald : Gem
    {
        private const int baseStrength = 1;
        private const int baseAgility = 4;
        private const int baseVitality = 9;

        public Emerald(Clarity clarity) : base(clarity, baseStrength, baseAgility, baseVitality)
        {
        }
    }
}
