
using System.Linq;
using System.Reflection;

namespace FestivalManager.Entities.Factories
{
    using Contracts;
    using Entities.Contracts;
    using System;

    public class SetFactory : ISetFactory
    {
        public ISet CreateSet(string name, string type)
        {
            var types = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .Where(x => typeof(ISet).IsAssignableFrom(x) && !x.IsAbstract)
                .ToArray();

            var setType = types.FirstOrDefault(x => x.Name == type);

            if (setType == null)
            {
                throw new InvalidOperationException("Invalid Set type!");
            }

            var set = (ISet)Activator.CreateInstance(setType, new object[] { name });
            return set;
        }
    }




}
