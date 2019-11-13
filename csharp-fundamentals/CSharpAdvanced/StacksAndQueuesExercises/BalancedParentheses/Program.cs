using System;
using System.Collections.Generic;

namespace BalancedParentheses
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            Stack<char> parentheses = new Stack<char>();
            bool result = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(' || input[i] == '{' || input[i] == '[')
                {
                    parentheses.Push(input[i]);
                }
                else if (input[i] == ')' || input[i] == '}' || input[i] == ']')
                {
                    if (input[i] == ')')
                    {
                        if (parentheses.Count == 0 || parentheses.Pop() != '(')
                        {
                            result = false;
                            break;
                        }
                    }
                    else if (input[i] == '}')
                    {
                        if (parentheses.Count == 0 || parentheses.Pop() != '{')
                        {
                            result = false;
                            break;
                        }
                    }
                    else if (input[i] == ']')
                    {
                        if (parentheses.Count == 0 || parentheses.Pop() != '[')
                        {
                            result = false;
                            break;
                        }
                    }
                    if (result == false)
                    {
                        break;
                    }
                }
            }

            if (result)
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
        }
    }
}
