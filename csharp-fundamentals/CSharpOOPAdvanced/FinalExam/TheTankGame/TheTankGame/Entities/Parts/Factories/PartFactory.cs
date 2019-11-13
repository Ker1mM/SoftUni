using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TheTankGame.Entities.Parts.Contracts;
using TheTankGame.Entities.Parts.Factories.Contracts;

namespace TheTankGame.Entities.Parts.Factories
{
    public class PartFactory : IPartFactory
    {
        public IPart CreatePart(string partType, string model, double weight, decimal price, int additionalParameter)
        {
            partType += "Part";
            var types = Assembly
                 .GetCallingAssembly()
                 .GetTypes()
                 .Where(t => typeof(IPart).IsAssignableFrom(t) && !t.IsAbstract)
                 .ToArray();

            var type = types.FirstOrDefault(x => x.Name == partType);

            var part = (IPart)Activator.CreateInstance(type, new object[] { model, weight, price, additionalParameter });

            return part;
        }
    }
}
