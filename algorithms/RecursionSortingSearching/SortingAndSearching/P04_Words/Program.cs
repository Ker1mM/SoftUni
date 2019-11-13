using System;
using System.Collections.Generic;
using System.Linq;

namespace P04_Words
{
    class Program
    {
        static int validCombinationCount = 0;
        static int inputLength;

        static void Main(string[] args)
        {
            char[] input = Console.ReadLine().OrderBy(x => x).ToArray();
            inputLength = input.Length;


            if (IsValid(input))
            {
                validCombinationCount = CalculateFactorial(inputLength);
            }
            else
            {
                Permutate(input, 0, inputLength);
            }

            Console.WriteLine(validCombinationCount);
        }

        private static void Permutate(char[] input, int start, int end)
        {
            if (start < end)
            {
                for (int i = end - 2; i >= start; i--)
                {
                    for (int j = i + 1; j < end; j++)
                    {
                        if (input[i] != input[j])
                        {
                            Swap(ref input[i], ref input[j]);
                            IsValid(input);
                            Permutate(input, i + 1, end);
                        }
                    }

                    char tmp = input[i];
                    for (int k = i; k < end - 1;)
                    {
                        input[k] = input[++k];
                    }

                    input[end - 1] = tmp;
                }
            }
        }

        private static int CalculateFactorial(int count)
        {
            int factorial = 1;
            for (int i = 2; i <= count; i++)
            {
                factorial *= i;
            }

            return factorial;
        }

        private static void Swap(ref char i, ref char j)
        {
            if (i == j)
            {
                return;
            }

            i ^= j;
            j ^= i;
            i ^= j;
        }

        private static bool IsValid(char[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] == array[i + 1])
                {
                    return false;
                }
            }

            validCombinationCount++;
            //Console.WriteLine(string.Join("", array));
            return true;
        }
    }
}
