﻿using System.Collections.Generic;

namespace BoxOfT
{
    public class Box<T>
    {
        private Stack<T> elements;

        public int Count { get => this.elements.Count; }

        public Box()
        {
            this.elements = new Stack<T>();
        }

        public void Add(T element)
        {
            this.elements.Push(element);
        }

        public T Remove()
        {
            return this.elements.Pop();
        }
    }
}
