using System;
using System.Collections.Generic;

namespace RecordUniqueNames
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputCount = int.Parse(Console.ReadLine());

            HashSet<string> names = new HashSet<string>();
            while (inputCount-- > 0)
            {
                string input = Console.ReadLine();
                names.Add(input);
            }

            Console.WriteLine(String.Join("\n", names));
        }
    }
}
