using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericSwapMethodIntegers
{
    public class StartUp
    {
        static void Main()
        {
            List<Box<int>> items = new List<Box<int>>();
            int count = int.Parse(Console.ReadLine());

            while (count-- > 0)
            {
                Box<int> item = new Box<int>(int.Parse(Console.ReadLine()));
                items.Add(item);
            }

            int[] swapArgs = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Swapper(items, swapArgs[0], swapArgs[1]);

            Console.WriteLine(string.Join(Environment.NewLine, items));
        }

        protected static void Swapper<T>(List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}
