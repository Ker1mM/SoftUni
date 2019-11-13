using System;
using System.Collections.Generic;

namespace GenericCountMethodString
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

            string swapArg = Console.ReadLine();

            Console.WriteLine(Count(items, swapArg));
        }

        static int Count<T>(List<Box<T>> list, T comparer) where T : IComparable<T>
        {
            int counter = 0;
            foreach (var item in list)
            {
                if (item.CompareTo(comparer) > 0)
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
