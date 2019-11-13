using System;
using System.Collections.Generic;
using System.Linq;

namespace AppliedArithmetics
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse);

            Func<int, int> Add = n => n += 1;
            Func<int, int> Multiply = n => n *= 2;
            Func<int, int> Subtract = n => n -= 1;
            Action<List<int>> Print = x => Console.WriteLine(String.Join(" ", x));

            string input;
            while ((input = Console.ReadLine()) != "end")
            {
                switch (input)
                {
                    case "add":
                        numbers = numbers.Select(Add);
                        break;
                    case "multiply":
                        numbers = numbers.Select(Multiply);
                        break;
                    case "subtract":
                        numbers = numbers.Select(Subtract);
                        break;
                    case "print":
                        Print(numbers.ToList());
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
