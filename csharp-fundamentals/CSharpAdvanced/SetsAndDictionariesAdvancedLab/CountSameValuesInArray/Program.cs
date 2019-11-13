using System;
using System.Collections.Generic;
using System.Linq;

namespace CountSameValuesInArray
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] marks = Console.ReadLine().Replace('.', ',').Split(" ").Select(double.Parse).ToArray();

            Dictionary<double, int> count = new Dictionary<double, int>();

            foreach (double mark in marks)
            {
                if (count.ContainsKey(mark) == false)
                {
                    count.Add(mark, 0);
                }
                count[mark]++;
            }

            foreach (var mark in count)
            {
                Console.WriteLine("{0} - {1} times", mark.Key, mark.Value);
            }
        }
    }
}
