using System;
using System.Linq;

namespace P02_Searching
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var element = int.Parse(Console.ReadLine());

            int index = FindElement(element, input);
            Console.WriteLine(index);

        }

        static int FindElement(int element, int[] array)
        {
            int result = -1;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }
    }
}
