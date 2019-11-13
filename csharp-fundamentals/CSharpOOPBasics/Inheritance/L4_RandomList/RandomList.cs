using System;
using System.Collections.Generic;

namespace CustomRandomList
{
    public class RandomList : List<string>
    {
        private Random rnd;

        public RandomList()
        {
            this.rnd = new Random();
        }

        public string RandomString()
        {
            if (this.Count > 0)
            {
                int index = rnd.Next(this.Count);

                string result = this[index];
                this.RemoveAt(index);
                return result;
            }
            throw new Exception("Empty list!");

        }
    }
}
