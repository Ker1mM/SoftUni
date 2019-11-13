using StorageMaster.Factories;
using StorageMaster.Structure.Products;
using StorageMaster.Structure.Storages;
using StorageMaster.Structure.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster.Logic
{
    public class StorageMaster
    {
        private List<Product> productPool;
        private List<Storage> storageRegistry;
        private Vehicle currentVehicle;

        public StorageMaster()
        {
            this.productPool = new List<Product>();
            this.storageRegistry = new List<Storage>();
        }

        public string AddProduct(string type, double price)
        {
            var product = ProductFactory.CreateProduct(type, price);
            this.productPool.Add(product);

            return $"Added {type} to pool";
        }

        public string RegisterStorage(string type, string name)
        {
            var storage = StorageFactory.CreateStorage(type, name);
            this.storageRegistry.Add(storage);

            return $"Registered {name}";
        }

        public string SelectVehicle(string storageName, int garageSlot)
        {
            this.currentVehicle = storageRegistry.FirstOrDefault(x => x.Name == storageName).GetVehicle(garageSlot);

            return $"Selected {this.currentVehicle.GetType().Name}";
        }

        public string LoadVehicle(IEnumerable<string> productNames)
        {
            int addedProdcutCount = 0;
            foreach (var productName in productNames)
            {
                int index = this.productPool.FindLastIndex(x => x.GetType().Name == productName);
                if (index == -1)
                {
                    throw new InvalidOperationException(string.Format(OutputMessages.OutOfStock, productName));
                }
                if (this.currentVehicle.IsFull)
                {
                    break;
                }
                this.currentVehicle.LoadProduct(this.productPool[index]);
                addedProdcutCount++;
                this.productPool.RemoveAt(index);
            }

            return $"Loaded {addedProdcutCount}/{productNames.Count()} products into {this.currentVehicle.GetType().Name}";
        }

        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {
            if (!this.storageRegistry.Any(x => x.Name == sourceName))
            {
                throw new InvalidOperationException(OutputMessages.InvalidSourceStorage);
            }
            if (!this.storageRegistry.Any(x => x.Name == destinationName))
            {
                throw new InvalidOperationException(OutputMessages.InvalidDestinationStorage);
            }

            Storage sourceStorage = this.storageRegistry.FirstOrDefault(x => x.Name == sourceName);
            Storage destinationStorage = this.storageRegistry.FirstOrDefault(x => x.Name == destinationName);

            string vehicleType = sourceStorage.GetVehicle(sourceGarageSlot).GetType().Name;
            int slot = sourceStorage.SendVehicleTo(sourceGarageSlot, destinationStorage);

            return $"Sent {vehicleType} to {destinationName} (slot {slot})";
        }

        public string UnloadVehicle(string storageName, int garageSlot)
        {
            var storage = this.storageRegistry.FirstOrDefault(x => x.Name == storageName);
            int productsInVehicle = storage.GetVehicle(garageSlot).Trunk.Count;
            int unloadedProductsCount = storage.UnloadVehicle(garageSlot);

            return $"Unloaded {unloadedProductsCount}/{productsInVehicle} products at {storageName}";
        }

        private string GetProductStatus(List<Product> products)
        {
            Dictionary<string, int> counter = new Dictionary<string, int>();
            foreach (var product in products)
            {
                string name = product.GetType().Name;
                if (!counter.ContainsKey(name))
                {
                    counter.Add(name, 0);
                }
                counter[name]++;
            }

            List<string> result = new List<string>();
            foreach (var product in counter.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                result.Add($"{product.Key} ({product.Value})");
            }

            return string.Join(", ", result);
        }

        private string GetGarageStatus(List<Vehicle> garage)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var car in garage)
            {
                string carType = "empty";
                if (car != null)
                {
                    carType = car.GetType().Name;
                }
                sb.Append($"{carType}|");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        public string GetStorageStatus(string storageName)
        {
            var storage = this.storageRegistry.FirstOrDefault(x => x.Name == storageName);

            double totalWeight = storage.Products.Sum(x => x.Weight);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Stock ({totalWeight}/{storage.Capacity}): [{GetProductStatus(storage.Products.ToList())}]");
            sb.AppendLine($"Garage: [{GetGarageStatus(storage.Garage.ToList())}]");

            return sb.ToString().TrimEnd();
        }

        public string GetSummary()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var storage in storageRegistry.OrderByDescending(x => x.GetTotalWorth()))
            {
                sb.AppendLine($"{storage.Name}:");
                sb.AppendLine($"{storage.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }

    }
}
