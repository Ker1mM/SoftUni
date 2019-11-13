using System;

namespace StorageMaster.Structure.Products
{
    public abstract class Product
    {
        private double price;

        public double Weight { get; private set; }

        public double Price
        {
            get { return price; }
            private set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException(OutputMessages.NegativePrice);
                }
                price = value;
            }
        }

        protected Product(double price, double weight)
        {
            this.Price = price;
            this.Weight = weight;
        }

    }
}
