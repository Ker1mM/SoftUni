using System;
using System.Collections;
using System.Collections.Generic;

namespace IteratorsAndComparators
{
    public class Stack<T> : IEnumerable<T>
    {
        private List<T> elements;
        public Stack()
        {
            this.elements = new List<T>();
        }

        public void Push(params T[] elems)
        {
            this.elements.AddRange(elems);
        }

        public T Pop()
        {
            if (elements.Count == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var temp = this.elements[this.elements.Count - 1];
            this.elements.RemoveAt(this.elements.Count - 1);
            return temp;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = elements.Count - 1; i >= 0; i--)
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
