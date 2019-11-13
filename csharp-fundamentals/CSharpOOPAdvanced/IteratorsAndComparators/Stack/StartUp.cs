using System;
using System.Linq;

namespace IteratorsAndComparators
{
    public class StartUp
    {
        static void Main()
        {
            string input;
            var myStack = new Stack<long>();

            try
            {
                while ((input = Console.ReadLine()) != "END")
                {
                    string[] args = input.Replace(",", "").Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    switch (args[0])
                    {
                        case "Push":
                            myStack.Push(args.Skip(1).Select(long.Parse).ToArray());
                            break;
                        case "Pop":
                            myStack.Pop();
                            break;
                        default:
                            break;
                    }
                }

                foreach (var element in myStack)
                {
                    Console.WriteLine(element);
                }

                foreach (var element in myStack)
                {
                    Console.WriteLine(element);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("No elements");
            }
        }
    }
}
