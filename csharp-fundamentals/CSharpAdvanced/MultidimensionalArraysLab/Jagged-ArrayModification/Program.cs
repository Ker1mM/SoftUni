using System;
using System.Linq;

namespace Jagged_ArrayModification
{
    class Program
    {
        static void Main(string[] args)
        {
            int rowCount = int.Parse(Console.ReadLine());

            int[][] jaggedArray = new int[rowCount][];

            for (int i = 0; i < rowCount; i++)
            {
                int[] currentRow = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
                jaggedArray[i] = currentRow;
            }

            string input = Console.ReadLine();
            while (input.Equals("END") == false)
            {
                string[] tokens = input.Split(" ");

                if (tokens.Length == 4)
                {
                    string command = tokens[0];
                    int row = int.Parse(tokens[1]);
                    int column = int.Parse(tokens[2]);
                    int amount = int.Parse(tokens[3]);
                    if (row < rowCount && row >= 0 && column >= 0 && column < jaggedArray[row].Length)
                    {
                        if (command.Equals("Add"))
                        {
                            jaggedArray[row][column] += amount;
                        }
                        else if (command.Equals("Subtract"))
                        {
                            jaggedArray[row][column] -= amount;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid coordinates");
                    }
                }
                input = Console.ReadLine();
            }

            for (int i = 0; i < jaggedArray.Length; i++)
            {
                Console.WriteLine(String.Join(" ", jaggedArray[i]));
            }
        }
    }
}
