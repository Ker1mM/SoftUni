using System.Collections;
using System.Collections.Generic;

namespace Froggy
{
    public class Lake : IEnumerable<int>
    {
        private List<int> stones;
        private string[] names = new string[] { "dale", "dale", "don", "dale" };

        public Lake(List<int> inputStones)
        {
            this.stones = inputStones;
        }

        public IEnumerator<int> GetEnumerator()
        {
            int count = this.stones.Count - 1;

            for (int i = 0; i <= count; i += 2)
            {
                yield return this.stones[i];
            }

            if (count % 2 == 0)
            {
                count--;
            }

            for (int i = count; i > 0; i -= 2)
            {
                yield return this.stones[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
