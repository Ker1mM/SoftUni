using System;
using System.Linq;

namespace CountUppercaseWords
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                String.Join("\n",
            Console.ReadLine()
            .Split(" ")
            .Where(x => x != "" && x[0] == x.ToUpper()[0])
                ));
        }
    }
}
