using System;
using System.Linq;

namespace AddVAT
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<double> printWithVAT = n => Console.WriteLine("{0:f2}", n *= 1.20);
            Console.ReadLine()
                .Split(", ")
                .Select(double.Parse)
                .ToList()
                .ForEach(x => printWithVAT(x));
        }
    }
}
