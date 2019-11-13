using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ShmoogleCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            string doublePattern = @"(?<=\Wdouble\s*)([a-z]+[a-zA-Z]*)";
            string intPattern = @"(?<=\Wint\s*)([a-z]+[a-zA-Z]*)";


            List<string> doubles = new List<string>();
            List<string> ints = new List<string>();
            string input;
            while ((input = Console.ReadLine()) != "//END_OF_CODE")
            {
                MatchCollection intMatches = Regex.Matches(input, intPattern);
                MatchCollection doubleMatches = Regex.Matches(input, doublePattern);

                foreach (Match intM in intMatches)
                {
                    ints.Add(intM.ToString());
                }
                foreach (Match doubleM in doubleMatches)
                {
                    doubles.Add(doubleM.ToString());
                }
            }

            if (doubles.Count > 0)
            {
                Console.WriteLine("Doubles: {0}", String.Join(", ", doubles.OrderBy(x => x)));
            }
            else
            {
                Console.WriteLine("Doubles: None");
            }

            if (ints.Count > 0)
            {
                Console.WriteLine("Ints: {0}", String.Join(", ", ints.OrderBy(x => x)));
            }
            else
            {
                Console.WriteLine("Ints: None");
            }
        }
    }
}
