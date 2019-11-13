namespace FestivalManager.Entities.Factories
{
    using Contracts;
    using Entities.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class PerformerFactory : IPerformerFactory
    {
        public IPerformer CreatePerformer(string name, int age)
        {
            var types = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .Where(x => typeof(IPerformer).IsAssignableFrom(x) && !x.IsAbstract)
                .ToArray();

            var performerType = types.FirstOrDefault(x => x.Name == "Performer");

            var performer = (IPerformer)Activator.CreateInstance(performerType, new object[] { name, age });
            return performer;
        }
    }
}