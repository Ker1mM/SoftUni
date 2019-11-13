using System.Collections;
using System.Collections.Generic;

namespace LinkedListTraversal
{
    public class MyLinkedList<T> : IEnumerable<T>
    {
        private List<T> elements;

        public int Count => this.elements.Count;

        public MyLinkedList()
        {
            this.elements = new List<T>();
        }

        public void Add(T element)
        {
            this.elements.Add(element);
        }

        public bool Remove(T element)
        {
            return this.elements.Remove(element);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
