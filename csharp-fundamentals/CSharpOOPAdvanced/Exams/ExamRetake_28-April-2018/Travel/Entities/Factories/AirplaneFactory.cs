namespace Travel.Entities.Factories
{
    using Contracts;
    using Airplanes.Contracts;
    using Travel.Entities.Airplanes;
    using System;
    using System.Linq;
    using System.Reflection;

    public class AirplaneFactory : IAirplaneFactory
    {
        public IAirplane CreateAirplane(string type)
        {
            var airplaneTypes = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .Where(t => typeof(IAirplane).IsAssignableFrom(t) && !t.IsAbstract)
                .ToArray();

            var airplaneType = airplaneTypes.FirstOrDefault(x => x.Name == type);

            if (airplaneType == null)
            {
                throw new InvalidOperationException("Invalid product type!");
            }

            var airplane = (IAirplane)Activator.CreateInstance(airplaneType);

            return airplane;
        }
    }
}