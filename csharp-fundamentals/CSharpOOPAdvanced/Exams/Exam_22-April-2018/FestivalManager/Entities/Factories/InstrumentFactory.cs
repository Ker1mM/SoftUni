namespace FestivalManager.Entities.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices.WindowsRuntime;
    using Contracts;
    using Entities.Contracts;
    using Instruments;

    public class InstrumentFactory : IInstrumentFactory
    {
        public IInstrument CreateInstrument(string typeName)
        {
            var types = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .Where(t => typeof(IInstrument).IsAssignableFrom(t) && !t.IsAbstract)
                .ToArray();

            var type = types.FirstOrDefault(x => x.Name == typeName);

            if (type == null)
            {
                throw new InvalidOperationException("Invalid Instrument type!");
            }

            return (IInstrument)Activator.CreateInstance(type, new object[] { });
        }
    }
}