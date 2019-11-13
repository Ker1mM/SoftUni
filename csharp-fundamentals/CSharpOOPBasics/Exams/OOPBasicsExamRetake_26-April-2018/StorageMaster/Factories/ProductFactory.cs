using StorageMaster.Structure.Products;
using System;

namespace StorageMaster.Factories
{
    public class ProductFactory
    {
        public static Product CreateProduct(string type, double price)
        {
            switch (type)
            {
                case "Gpu":
                    return new Gpu(price);
                case "HardDrive":
                    return new HardDrive(price);
                case "Ram":
                    return new Ram(price);
                case "SolidStateDrive":
                    return new SolidStateDrive(price);
                default:
                    throw new InvalidOperationException(OutputMessages.InvalidProductType);
            }
        }
    }
}
