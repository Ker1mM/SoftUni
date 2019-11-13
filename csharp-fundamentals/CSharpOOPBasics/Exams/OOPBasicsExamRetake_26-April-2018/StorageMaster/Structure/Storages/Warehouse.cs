using StorageMaster.Structure.Vehicles;

namespace StorageMaster.Structure.Storages
{
    public class Warehouse : Storage
    {
        private const int warehouseCapacity = 10;
        private const int warehouseGarageSlots = 10;

        public Warehouse(string name) : base(name, warehouseCapacity, warehouseGarageSlots, new Vehicle[] { new Semi(), new Semi(), new Semi() })
        {

        }
    }
}
