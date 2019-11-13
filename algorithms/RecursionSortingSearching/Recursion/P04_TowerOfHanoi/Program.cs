using System;
using System.Collections.Generic;
using System.Linq;

namespace P04_TowerOfHanoi
{
    class Program
    {
        static Stack<int> source;
        static Stack<int> destination;
        static Stack<int> spare;
        static int stepsTaken = 0;

        static void Main(string[] args)
        {
            int discCount = int.Parse(Console.ReadLine());
            var range = Enumerable.Range(1, discCount).Reverse();

            source = new Stack<int>(range);
            destination = new Stack<int>();
            spare = new Stack<int>();

            PrintRods();
            MoveDisks(discCount, source, destination, spare);
        }

        private static void MoveDisks(int bottomDisk, Stack<int> source, Stack<int> destination, Stack<int> spare)
        {
            if (bottomDisk == 1)
            {
                stepsTaken++;
                destination.Push(source.Pop());
                Console.WriteLine($"Step #{stepsTaken}: Moved disk");
                PrintRods();
            }
            else
            {
                MoveDisks(bottomDisk - 1, source, spare, destination);
                stepsTaken++;
                destination.Push(source.Pop());
                Console.WriteLine($"Step #{stepsTaken}: Moved disk");
                PrintRods();
                MoveDisks(bottomDisk - 1, spare, destination, source);
            }
        }

        private static void PrintRods()
        {
            Console.WriteLine("Source: {0}", string.Join(", ", source.Reverse()));
            Console.WriteLine("Destination: {0}", string.Join(", ", destination.Reverse()));
            Console.WriteLine("Spare: {0}", string.Join(", ", spare.Reverse()));
            Console.WriteLine();
        }
    }
}
