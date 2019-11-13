using System;
using System.Collections.Generic;
using System.Linq;

namespace EgyptianFractions
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split('/');
            long numerator = int.Parse(input[0]);
            long denominator = int.Parse(input[1]);


            List<string> result = new List<string>();
            if (numerator >= denominator)
            {
                result.Add("Error (fraction is equal to or greater than 1)");
            }
            else
            {
                result.Add($"{numerator}/{denominator} = ");


                long nextDenom = 2;

                while (true)
                {
                    if (denominator < nextDenom * numerator)
                    {
                        result.Add($"1/{nextDenom}");
                        numerator = (numerator * nextDenom) - denominator;
                        denominator *= nextDenom;
                    }
                    else if (denominator == nextDenom * numerator)
                    {
                        result.Add($"1/{nextDenom}");
                        break;
                    }
                    nextDenom++;
                }

            }

            Console.Write(result[0]);
            Console.WriteLine(string.Join($" + ", result.Skip(1)));
        }
    }
}
