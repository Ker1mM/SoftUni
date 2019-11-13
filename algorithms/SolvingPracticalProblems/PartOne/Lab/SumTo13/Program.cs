using System;
using System.Linq;

namespace SumTo13
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputArgs = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int a = inputArgs[0];
            int b = inputArgs[1];
            int c = inputArgs[2];

            if (CanSumTo13(a, b, c))
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
        }

        private static bool CanSumTo13(int a, int b, int c)
        {
            int[] possibilities = new int[] { -1, 1 };
            for (int one = 0; one < 2; one++)
            {
                for (int two = 0; two < 2; two++)
                {
                    for (int three = 0; three < 2; three++)
                    {
                        int result = (a * possibilities[one]) + (b * possibilities[two]) + (c * possibilities[three]);
                        if (result == 13)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
