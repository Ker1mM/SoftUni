using System;
using System.Linq;

namespace P03_JediGalaxy
{
    class StartUp
    {
        static void Main()
        {
            int[] dimensions =
                Console.ReadLine()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[,] matrix = StarManipulation.FillMatrix(dimensions);

            string command = Console.ReadLine();
            long sum = 0;
            while (command != "Let the Force be with you")
            {
                int[] ivoCoordinates =
                    command
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                int[] evilCoordinates =
                    Console.ReadLine()
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                StarManipulation.DestroyStars(matrix, evilCoordinates);
                sum += StarManipulation.CollectStars(matrix, ivoCoordinates);

                command = Console.ReadLine();
            }

            Console.WriteLine(sum);
        }
    }
}
