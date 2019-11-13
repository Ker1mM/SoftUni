using DungeonsAndCodeWizards.Messages;
using DungeonsAndCodeWizards.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndCodeWizards.Models.Bags
{
    public abstract class Bag
    {
        private List<Item> items; //Might need further work.

        public int Capacity { get; private set; } //Default 100;
        public int Load { get => items.Sum(x => x.Weight); }

        public IReadOnlyCollection<Item> Items
        {
            get { return this.items.AsReadOnly(); }
        }

        protected Bag(int capacity)
        {
            this.Capacity = capacity;
            this.items = new List<Item>();
        }

        internal bool HasEnoughSpace(int itemWeight)
        {
            bool result = (itemWeight + this.Load) <= this.Capacity;
            return result;
        }

        public void AddItem(Item item)
        {
            if (!HasEnoughSpace(item.Weight))
            {
                throw new InvalidOperationException(OutputMessages.FullBag());
            }

            this.items.Add(item);
        }

        public Item GetItem(string name)
        {
            if (this.Items.Count == 0)
            {
                throw new InvalidOperationException(OutputMessages.EmptyBag());
            }

            var item = this.Items.FirstOrDefault(x => x.GetType().Name == name);

            if (item == null)
            {
                throw new ArgumentException(string.Format(OutputMessages.NoItemWithThatName(), name));
            }

            this.items.Remove(item);
            return item;
        }
    }
}
