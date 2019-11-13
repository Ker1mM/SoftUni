using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericSwapMethodStrings
{
    public class StartUp
    {
        static void Main()
        {
            List<Box<string>> items = new List<Box<string>>();
            int count = int.Parse(Console.ReadLine());

            while (count-- > 0)
            {
                Box<string> item = new Box<string>(Console.ReadLine());
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
