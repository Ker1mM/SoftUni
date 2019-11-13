using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P01_Database
{
    public class Database
    {
        private const int Capacity = 16;

        private int[] elements;
        private int nextFree;

        public Database(params int[] integers)
        {
            this.elements = new int[Capacity];
            this.nextFree = 0;
            this.Add(integers);
        }

        public void Add(params int[] integers)
        {
            if (nextFree + integers.Length > 16)
            {
                throw new InvalidOperationException();
            }

            foreach (var element in integers)
            {
                this.elements[nextFree] = element;
                nextFree++;
            }
        }

        public int Remove()
        {
            if (this.nextFree == 0)
            {
                throw new InvalidOperationException();
            }

            this.nextFree--;
            return this.elements[this.nextFree];
        }

        public int[] Fetch()
        {
            return this.elements.Take(nextFree).ToArray();
        }
    }
}
