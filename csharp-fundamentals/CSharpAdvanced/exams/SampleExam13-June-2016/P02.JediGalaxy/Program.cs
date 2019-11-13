using System;
using System.Linq;

namespace P02.JediGalaxy
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimensions = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int[][] galaxy = new int[dimensions[0]][];

            for (int i = 0; i < dimensions[0]; i++)
            {
                galaxy[i] = new int[dimensions[1]];
                for (int j = 0; j < galaxy[i].Length; j++)
                {
                    galaxy[i][j] = i * dimensions[1] + j;
                }
            }

            long result = 0;
            string input;
            while ((input = Console.ReadLine()) != "Let the Force be with you")
            {
                int[] ivoCoord = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                input = Console.ReadLine();
                int[] evilCoord = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                Destroy(galaxy, evilCoord[0], evilCoord[1]);
                result += AddStars(galaxy, ivoCoord[0], ivoCoord[1]);
            }

            Console.WriteLine(result);
        }

        public static long AddStars(int[][] matrix, int ivoRow, int ivoCol)
        {
            int maxRow = matrix.Length;
            int maxCol = matrix[0].Length;
            int counter = 0;
            long totalScore = 0;

            while (ivoRow - counter >= 0 && ivoCol + counter < maxCol)
            {
                if (ivoRow - counter < maxRow && ivoCol + counter >= 0)
                {
                    totalScore += matrix[ivoRow - counter][ivoCol + counter];
                }
                counter++;
            }
            return totalScore;

        }

        public static void Destroy(int[][] matrix, int evilRow, int evilCol)
        {
            int maxRow = matrix.Length;
            int maxCol = matrix[0].Length;
            int counter = 0;

            while (evilRow - counter >= 0 && evilCol - counter >= 0)
            {
                if (evilRow - counter < maxRow && evilCol - counter < maxCol)
                {
                    matrix[evilRow - counter][evilCol - counter] = 0;
                }
                counter++;
            }

        }
    }
}
