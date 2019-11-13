using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomList
{
    public class AnyType<T> where T : IComparable<T>
    {
        private List<T> items;

        public AnyType()
        {
            this.items = new List<T>();
        }

        public void Add(T element)
        {
            this.items.Add(element);
        }

        public T Remove(int index)
        {
            var item = this.items[index];
            this.items.RemoveAt(index);

            return item;
        }

        public bool Contains(T element)
        {
            return this.items.Any(x => x.Equals(element));
        }

        public void Swap(int index1, int index2)
        {
            var temp = this.items[index1];
            this.items[index1] = this.items[index2];
            this.items[index2] = temp;
        }

        public int CountGreaterThan(T element)
        {
            int counter = 0;
            foreach (var item in this.items)
            {
                if (item.CompareTo(element) > 0)
                {
                    counter++;
                }
            }

            return counter;
        }

        public T Max()
        {
            return this.items.Max();
        }

        public T Min()
        {
            return this.items.Min();
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.items);
        }
    }
}
