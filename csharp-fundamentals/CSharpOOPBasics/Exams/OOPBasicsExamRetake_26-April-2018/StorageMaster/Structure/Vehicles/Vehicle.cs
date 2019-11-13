using StorageMaster.Structure.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorageMaster.Structure.Vehicles
{
    public abstract class Vehicle
    {
        private List<Product> trunk;

        public IReadOnlyCollection<Product> Trunk
        {
            get
            {
                return this.trunk.AsReadOnly();
            }
        }

        public int Capacity { get; private set; }
        public bool IsFull { get => this.trunk.Sum(x => x.Weight) >= this.Capacity; }
        public bool IsEmpty { get => this.trunk.Count == 0; }

        protected Vehicle(int capacity)
        {
            this.Capacity = capacity;
            this.trunk = new List<Product>();
        }

        public void LoadProduct(Product product)
        {
            if (this.IsFull)
            {
                throw new InvalidOperationException(OutputMessages.FullVehicle);
            }

            this.trunk.Add(product);
        }

        public Product Unload()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException(OutputMessages.EmptyTrunk);
            }

            var lastElement = trunk.Last();
            trunk.Remove(lastElement);
            return lastElement;
        }


    }
}
