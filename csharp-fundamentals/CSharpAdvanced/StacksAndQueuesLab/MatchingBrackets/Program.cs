using System;
using System.Collections.Generic;

namespace MatchingBrackets
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            Stack<int> bracketIndexes = new Stack<int>();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    bracketIndexes.Push(i);
                }
                else if (input[i] == ')')
                {
                    string result = input.Substring(bracketIndexes.Peek(), i - bracketIndexes.Pop() + 1);
                    Console.WriteLine(result);
                }
            }
        }
    }
}
