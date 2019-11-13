using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IteratorsAndComparators
{
    public class ListyIterator<T> : IEnumerable<T>
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

        public string PrintAll()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var element in elements)
            {
                sb.Append($"{element} ");
            }

            return sb.ToString().TrimEnd();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                yield return this.elements[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
