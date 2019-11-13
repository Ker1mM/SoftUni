using System;
using System.Collections.Generic;

namespace GenericCountMethodDouble
{
    public class StartUp
    {
        static void Main()
        {
            List<Box<double>> items = new List<Box<double>>();
            int count = int.Parse(Console.ReadLine());

            while (count-- > 0)
            {
                Box<double> item = new Box<double>(double.Parse(Console.ReadLine()));
                items.Add(item);
            }

            double swapArg = double.Parse(Console.ReadLine());

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
