using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ArraySlider
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BigInteger> line = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(BigInteger.Parse)
                .ToList();

            string input;
            int index = 0;
            while ((input = Console.ReadLine().Trim()) != "stop")
            {
                string[] tokens = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                long offset = long.Parse(tokens[0]);
                char operation = tokens[1][0];
                int operand = int.Parse(tokens[2]);

                index = GetIndex(line.Count, offset, index);
                BigInteger result = 0;
                switch (operation)
                {
                    case '&':
                        result = line[index] & operand;
                        break;
                    case '|':
                        result = line[index] | operand;
                        break;
                    case '^':
                        result = line[index] ^ operand;
                        break;
                    case '+':
                        result = line[index] + operand;
                        break;
                    case '-':
                        result = line[index] < operand ? 0 : line[index] - operand;
                        break;
                    case '*':
                        result = line[index] * operand;
                        break;
                    case '/':
                        result = line[index] / operand;
                        break;
                    default:
                        break;
                }

                line[index] = result;
            }
            Console.WriteLine("[{0}]", String.Join(", ", line));
        }

        public static int GetIndex(int count, long offset, int index)
        {
            int result = index + (int)(offset % count);
            if (result < 0)
            {
                result = count + result;
            }
            else if (result >= count)
            {
                result = result % count;
            }

            return result;
        }
    }
}
