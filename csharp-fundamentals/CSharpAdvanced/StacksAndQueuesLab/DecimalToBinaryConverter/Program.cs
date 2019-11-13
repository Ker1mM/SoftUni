using System;
using System.Collections.Generic;

namespace DecimalToBinaryConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());

            Stack<int> result = new Stack<int>();

            if (input == 0)
            {
                result.Push(0);
            }
            else
            {
                while (input > 0)
                {
                    int remaining = input % 2;
                    result.Push(remaining);
                    input = input / 2;
                }
            }

            int count = result.Count;

            while(count > 0)
            {
                Console.Write(result.Pop());
                count = result.Count;
            }
            Console.WriteLine();
        }
    }
}
