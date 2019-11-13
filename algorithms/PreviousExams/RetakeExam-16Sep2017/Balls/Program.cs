using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balls
{
    class Program
    {
        static StringBuilder result;
        static void Main(string[] args)
        {
            int pocketCount = int.Parse(Console.ReadLine());
            int ballCount = int.Parse(Console.ReadLine());
            int capacity = int.Parse(Console.ReadLine());
            result = new StringBuilder();
            int[] vector = new int[pocketCount];
            GetCombination(vector, ballCount, capacity, 0);
            Console.WriteLine(result.ToString().TrimEnd());
        }

        private static void GetCombination(int[] vector, int ballsLeft, int pocketCap, int index)
        {
            if (index == vector.Length)
            {
                result.AppendLine(string.Join(", ", vector));
            }
            else
            {
                if (index == vector.Length - 1)
                {
                    if (ballsLeft <= pocketCap)
                    {
                        vector[index] = ballsLeft;
                        result.AppendLine(string.Join(", ", vector));
                    }
                }
                else
                {

                    int ballCount = 0;
                    if (pocketCap <= ballsLeft - (vector.Length - index - 1))
                    {
                        ballCount = pocketCap;
                    }
                    else
                    {
                        ballCount = ballsLeft - (vector.Length - index - 1);
                    }

                    for (int i = ballCount; i >= 1; i--)
                    {
                        vector[index] = i;
                        GetCombination(vector, ballsLeft - i, pocketCap, index + 1);
                    }
                }
            }
        }
    }
}
