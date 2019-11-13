using StorageMaster.Structure.Vehicles;

namespace StorageMaster.Structure.Storages
{
    public class DistributionCenter : Storage
    {
        private const int distributionCenterCapacity = 2;
        private const int distributionCenterGarageSlots = 5;

        public DistributionCenter(string name) : base(name, distributionCenterCapacity, distributionCenterGarageSlots, new Vehicle[] { new Van(), new Van(), new Van() })
        {

        }
    }
}
