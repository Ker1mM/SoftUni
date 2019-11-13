using System;

namespace P01_ReverseArray
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] array = Console.ReadLine().Split();
            PrintReverse(array, array.Length - 1);
            Console.WriteLine();
        }

        static void PrintReverse(string[] array, int index)
        {
            if (index == 0)
            {
                Console.Write(array[0]);
                return;
            }
            else
            {
                Console.Write(array[index] + " ");
                PrintReverse(array, index - 1);
            }
        }
    }
}
