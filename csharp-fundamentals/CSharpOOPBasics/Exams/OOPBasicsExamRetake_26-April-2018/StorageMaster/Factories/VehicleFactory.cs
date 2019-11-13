using StorageMaster.Structure.Vehicles;
using System;

namespace StorageMaster.Factories
{
    public class VehicleFactory
    {
        public static Vehicle CreateVehicle(string type)
        {
            switch (type)
            {
                case "Semi":
                    return new Semi();
                case "Truck":
                    return new Truck();
                case "Van":
                    return new Van();
                default:
                    throw new InvalidOperationException(OutputMessages.InvalidVehicleType);
            }
        }
    }
}
