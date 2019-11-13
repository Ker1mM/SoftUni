using StorageMaster.Structure.Products;
using StorageMaster.Structure.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorageMaster.Structure.Storages
{
    public abstract class Storage
    {
        private List<Vehicle> garage; //Might need to change to array;
        private List<Product> products;

        public string Name { get; private set; }
        public int Capacity { get; private set; }
        public int GarageSlots { get; private set; }
        public bool IsFull { get => this.products.Sum(x => x.Weight) >= this.Capacity; }

        protected Storage(string name, int capacity, int garageSlots, IEnumerable<Vehicle> vehicles)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.GarageSlots = garageSlots;
            this.garage = new List<Vehicle>();
            this.products = new List<Product>();
            this.garage.AddRange(vehicles);
            this.Fill();
        }

        private void Fill()
        {
            int difference = this.GarageSlots - this.garage.Count;
            if (difference > 0)
            {
                garage.AddRange(new Vehicle[difference]);
            }
        }

        public IReadOnlyCollection<Vehicle> Garage
        {
            get
            {
                return this.garage.AsReadOnly();
            }
        }

        public IReadOnlyCollection<Product> Products
        {
            get
            {
                return this.products.AsReadOnly();
            }
        }

        public Vehicle GetVehicle(int garageSlot)
        {
            if (garageSlot >= this.GarageSlots)
            {
                throw new InvalidOperationException(OutputMessages.InvalidGarageSlot);
            }

            if (garageSlot >= this.garage.Count || this.garage[garageSlot] == null)
            {
                throw new InvalidOperationException(OutputMessages.EmptyGarageSlot);
            }
;
            return this.garage[garageSlot];
        }

        private int FindFreeSlot()
        {
            int freeSlot = this.garage.FindIndex(x => x == null);

            if (freeSlot < 0 || freeSlot >= this.GarageSlots)
            {
                throw new InvalidOperationException(OutputMessages.FullGarage);
            }

            return freeSlot;
        }

        private void AddVehicleToGarage(Vehicle vehicle, int slot)
        {
            this.garage[slot] = vehicle;
        }


        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            int freeSlot = deliveryLocation.FindFreeSlot();
            var vehicle = this.GetVehicle(garageSlot);
            int indexOfVehicle = this.garage.IndexOf(vehicle);
            this.garage[indexOfVehicle] = null;
            deliveryLocation.AddVehicleToGarage(vehicle, freeSlot);

            return freeSlot;
        }

        public int UnloadVehicle(int garageSlot)
        {

            if (IsFull)
            {
                throw new InvalidOperationException(OutputMessages.FullStorage);
            }

            var vehicle = this.GetVehicle(garageSlot);
            int unloadedProductCount = 0;
            while (!IsFull && !vehicle.IsEmpty)
            {
                this.products.Add(vehicle.Unload());
                unloadedProductCount++;
            }

            return unloadedProductCount;
        }

        public double GetTotalWorth()
        {
            return this.products.Sum(x => x.Price);
        }

        public override string ToString()
        {
            return $"Storage worth: ${this.GetTotalWorth():F2}";
        }
    }
}
