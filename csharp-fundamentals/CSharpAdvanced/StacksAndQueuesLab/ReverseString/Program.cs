using System;
using System.Collections.Generic;
using System.Linq;

namespace ReverseString
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            Stack<char> inputString = new Stack<char>();

            char[] tempArray = input.ToCharArray();
            foreach (char current in tempArray)
            {
                inputString.Push(current);
            }

            while(inputString.TryPop( out char result))
            {
                Console.Write(result);
            }
            Console.WriteLine();
        }
    }
}
