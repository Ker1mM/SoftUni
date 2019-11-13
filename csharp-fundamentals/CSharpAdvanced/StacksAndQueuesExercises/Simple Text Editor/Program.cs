using System;
using System.Collections.Generic;

namespace Simple_Text_Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            int commandsCount = int.Parse(Console.ReadLine());

            Stack<string> text = new Stack<string>();

            for (int i = 0; i < commandsCount; i++)
            {
                string input = Console.ReadLine();
                string[] tokens = input.Split(" ");
                int command = int.Parse(tokens[0]);

                if (command == 1)
                {
                    if (text.Count == 0)
                    {
                        text.Push(tokens[1]);
                    }
                    else
                    {
                        text.Push(text.Peek() + tokens[1]);
                    }
                }
                else if (command == 2)
                {
                    int count = int.Parse(tokens[1]);
                    if (text.Peek().Length <= count)
                    {
                        text.Push("");
                    }
                    else
                    {
                        text.Push(text.Peek().Remove(text.Peek().Length - count, count));
                    }
                }
                else if (command == 3)
                {
                    int index = int.Parse(tokens[1]);
                    if (text.Count > 0)
                    {
                        Console.WriteLine(text.Peek()[index - 1]);
                    }
                }
                else if (command == 4)
                {
                    if (text.Count > 0)
                    {
                        text.Pop();
                    }
                }
            }
        }
    }
}
