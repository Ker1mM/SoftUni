using System;
using System.Collections.Generic;
using System.Text;

namespace Parentheses
{
    class Program
    {
        static List<string> sb;

        static void Main(string[] args)
        {
            sb = new List<string>();
            int n = int.Parse(Console.ReadLine());
            string[] vector = new string[2 * n];
            PrintParentheses(vector, 0, 0, 0, n);
            for (int i = sb.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(sb[i]);
            }
        }

        private static void PrintParentheses(string[] vector, int pos, int open, int close, int count)
        {
            if (close == count)
            {
                sb.Add(string.Join("", vector));
                return;
            }
            else
            {
                if (open > close)
                {
                    vector[pos] = ")";
                    PrintParentheses(vector, pos + 1, open, close + 1, count);
                }

                if (open < count)
                {
                    vector[pos] = "(";
                    PrintParentheses(vector, pos + 1, open + 1, close, count);
                }
            }
        }

    }
}
