using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectingCables
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToList();

            Console.WriteLine($"Maximum pairs connected: {FindPairs(input)}");
        }

        private static int FindPairs(List<int> array)
        {
            var orderedArray = array.OrderBy(x => x).ToList();

            List<int> top = new List<int>();
            List<int> bottom = new List<int>();

            int counter = 0;

            for (int i = 0; i < orderedArray.Count; i++)
            {
                var topElement = orderedArray[i];
                var bottomElement = array[i];

                if (bottomElement == topElement)
                {
                    counter++;
                    top.Clear();
                    bottom.Clear();
                    continue;
                }

                if (bottom.Contains(topElement))
                {
                    counter++;
                    top.Clear();

                    while (bottom.Count > 0)
                    {
                        int element = bottom[0];
                        bottom.RemoveAt(0);

                        if (element == topElement)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    top.Add(topElement);
                }

                if (top.Contains(bottomElement))
                {
                    counter++;

                    while (top.Count > 0)
                    {
                        int element = top[0];
                        top.RemoveAt(0);

                        if (element == bottomElement)
                        {
                            break;
                        }
                    }

                    bottom.Clear();
                }
                else
                {
                    bottom.Add(bottomElement);
                }
            }

            return counter;
        }
    }
}
