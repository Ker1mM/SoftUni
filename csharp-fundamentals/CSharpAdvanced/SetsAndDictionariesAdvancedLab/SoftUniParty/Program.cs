using System;
using System.Collections.Generic;

namespace SoftUniParty
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> regulars = new HashSet<string>();
            HashSet<string> vips = new HashSet<string>();
            string input;
            while ((input = Console.ReadLine()) != "PARTY")
            {
                if (Char.IsDigit(input[0]))
                {
                    vips.Add(input);
                }
                else
                {
                    regulars.Add(input);
                }
            }

            while ((input = Console.ReadLine()) != "END")
            {
                vips.Remove(input);
                regulars.Remove(input);
            }

            int count = vips.Count + regulars.Count;
            Console.WriteLine(count);
            if (vips.Count > 0)
            {
                Console.WriteLine(String.Join("\n", vips));
            }
            if (regulars.Count > 0)
            {
                Console.WriteLine(String.Join("\n", regulars));
            }
        }
    }
}
