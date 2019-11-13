using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            Stack<string> content = new Stack<string>(input.Split(" ").Reverse());

            int result = 0;
            bool isPlus = true;
            while (content.TryPop(out string nextElement))
            {
                if (nextElement == "-")
                {
                    isPlus = false;
                }
                else if (nextElement == "+")
                {
                    isPlus = true;
                }
                else
                {
                    if (isPlus)
                    {
                        result += int.Parse(nextElement);
                    } else
                    {
                        result -= int.Parse(nextElement);
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}
