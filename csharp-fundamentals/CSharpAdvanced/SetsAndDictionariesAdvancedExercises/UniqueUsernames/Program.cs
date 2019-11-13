using System;
using System.Collections.Generic;

namespace UniqueUsernames
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputCount = int.Parse(Console.ReadLine());
            HashSet<string> names = new HashSet<string>();

            while (inputCount-- > 0)
            {
                string name = Console.ReadLine();
                names.Add(name);
            }

            if (names.Count > 0)
            {
                Console.WriteLine(String.Join("\n", names));
            }
        }
    }
}
