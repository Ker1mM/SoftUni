using StorageMaster.Structure.Vehicles;

namespace StorageMaster.Structure.Storages
{
    public class AutomatedWarehouse : Storage
    {
        private const int automatedWarehouseCapacity = 1;
        private const int automatedWarehouseGarageSlots = 2;

        public AutomatedWarehouse(string name) : base(name, automatedWarehouseCapacity, automatedWarehouseGarageSlots, new Vehicle[] { new Truck() })
        {

        }
    }
}
