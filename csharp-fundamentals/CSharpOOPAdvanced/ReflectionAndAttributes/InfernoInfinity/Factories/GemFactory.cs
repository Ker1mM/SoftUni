using InfernoInfinity.Data;
using InfernoInfinity.Models.Gems;
using System;

namespace InfernoInfinity.Factories
{
    class GemFactory
    {
        public static Gem CreateGem(string[] args)
        {
            string[] gemInfo = args[0].Split();
            Clarity clarity = (Clarity)Enum.Parse(typeof(Clarity), gemInfo[0]);

            Type type = Type.GetType("InfernoInfinity.Models.Gems." + gemInfo[1]);

            var obj = Activator.CreateInstance(type, new object[] { clarity });

            return (Gem)obj;
        }
    }
}
