using InfernoInfinity.Data;

namespace InfernoInfinity.Models.Gems
{
    public class Ruby : Gem
    {
        private const int baseStrength = 7;
        private const int baseAgility = 2;
        private const int baseVitality = 5;

        public Ruby(Clarity clarity) : base(clarity, baseStrength, baseAgility, baseVitality)
        {
        }
    }
}
