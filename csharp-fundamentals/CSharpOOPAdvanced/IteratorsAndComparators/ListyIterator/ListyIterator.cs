using System;
using System.Collections.Generic;
using System.Linq;

namespace IteratorsAndComparators
{
    public class ListyIterator<T>
    {
        private List<T> elements;
        private int currentIndex;

        public ListyIterator(params T[] list)
        {
            this.elements = list.ToList();
            this.currentIndex = 0;
        }

        public bool Move()
        {
            if (this.HasNext())
            {
                this.currentIndex++;
                return true;
            }
            return false;
        }

        public bool HasNext()
        {
            return currentIndex + 1 < elements.Count;
        }

        public string Print()
        {
            if (elements.Count == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return elements[currentIndex].ToString();
        }
    }
}
