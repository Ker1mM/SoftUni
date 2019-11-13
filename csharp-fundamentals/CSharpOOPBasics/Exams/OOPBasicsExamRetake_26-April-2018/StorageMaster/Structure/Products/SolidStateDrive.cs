namespace StorageMaster.Structure.Products
{
    public class SolidStateDrive : Product
    {
        private const double solidStateDriveWeight = 0.2;

        public SolidStateDrive(double price) : base(price, solidStateDriveWeight)
        {

        }
    }
}
