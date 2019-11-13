using System;
using System.Collections.Generic;

namespace CustomStack
{
    public class StackOfStrings
    {
        private List<string> data;

        public StackOfStrings()
        {
            this.data = new List<string>();
        }

        public void Push(string item)
        {
            data.Add(item);
        }

        public string Pop()
        {
            if (this.data.Count == 0)
            {
                throw new InvalidOperationException("Stack is Empty.");
            }
            string result = data[data.Count - 1];
            this.data.RemoveAt(data.Count - 1);
            return result;
        }

        public string Peek()
        {
            if (this.data.Count == 0)
            {
                throw new InvalidOperationException("Stack is Empty.");
            }
            return data[data.Count - 1];
        }

        public bool IsEmpty()
        {
            return !(this.data.Count > 0);
        }
    }
}
