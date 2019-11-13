using StorageMaster.Structure.Storages;
using System;

namespace StorageMaster.Factories
{
    public class StorageFactory
    {
        public static Storage CreateStorage(string type, string name)
        {
            switch (type)
            {
                case "AutomatedWarehouse":
                    return new AutomatedWarehouse(name);
                case "DistributionCenter":
                    return new DistributionCenter(name);
                case "Warehouse":
                    return new Warehouse(name);
                default:
                    throw new InvalidOperationException(OutputMessages.InvalidStorageType);
            }
        }
    }
}
