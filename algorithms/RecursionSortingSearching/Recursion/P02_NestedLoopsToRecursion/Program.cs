using System;

namespace P02_NestedLoopsToRecursion
{
    class Program
    {
        private static int[] loops;

        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            loops = new int[count];
            PrintNestedLoops(count, 0);
        }

        static void PrintNestedLoops(int count, int index)
        {
            if (count == index)
            {
                Print();
                return;
            }

            for (int counter = 1; counter <= count; counter++)
            {
                loops[index] = counter;
                PrintNestedLoops(count, index + 1);
            }
        }

        static void Print()
        {
            Console.WriteLine(string.Join(" ", loops));
        }
    }
}
