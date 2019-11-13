using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TheTankGame.Entities.Miscellaneous;
using TheTankGame.Entities.Miscellaneous.Contracts;
using TheTankGame.Entities.Vehicles.Contracts;
using TheTankGame.Entities.Vehicles.Factories.Contracts;

namespace TheTankGame.Entities.Vehicles.Factories
{
    public class VehicleFactory : IVehicleFactory
    {
        public IVehicle CreateVehicle(string vehicleType, string model, double weight, decimal price, int attack, int defense, int hitPoints)
        {
            var types = Assembly
                 .GetCallingAssembly()
                 .GetTypes()
                 .Where(x => typeof(IVehicle).IsAssignableFrom(x) && !x.IsAbstract)
                 .ToArray();

            var type = types.FirstOrDefault(x => x.Name == vehicleType);

            IAssembler assembler = new VehicleAssembler();

            var vehicle = (IVehicle)Activator
                .CreateInstance(type, new object[] { model, weight, price, attack, defense, hitPoints, assembler });

            return vehicle;
        }
    }
}
