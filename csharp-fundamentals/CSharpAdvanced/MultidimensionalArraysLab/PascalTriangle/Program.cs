using System;

namespace PascalTriangle
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputNumber = int.Parse(Console.ReadLine());

            long[][] pascalTriangle = new long[inputNumber][];
            pascalTriangle[0] = new long[] { 1 };

            for (int i = 1; i < inputNumber; i++)
            {
                pascalTriangle[i] = new long[i + 1];

                for (int j = 0; j < i + 1; j++)
                {
                    long firstNum = 0;
                    if (j - 1 >= 0)
                    {
                        firstNum = pascalTriangle[i - 1][j - 1];
                    }
                    long secondNum = 0;
                    if (j < pascalTriangle[i - 1].Length)
                    {
                        secondNum = pascalTriangle[i - 1][j];
                    }
                    pascalTriangle[i][j] = firstNum + secondNum;
                }
            }

            for (int i = 0; i < pascalTriangle.Length; i++)
            {
                Console.WriteLine(String.Join(" ", pascalTriangle[i]));
            }
        }
    }
}
