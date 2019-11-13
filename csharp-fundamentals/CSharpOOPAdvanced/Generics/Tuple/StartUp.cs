using System;
using System.Collections.Generic;

namespace TupleExercise
{
    public class StartUp
    {
        static void Main()
        {
            string[] args = Console.ReadLine().Split();
            var name = new MyTuple<string, string>(args[0] + " " + args[1], args[2]);
            args = Console.ReadLine().Split();
            var beerAmount = new MyTuple<string, int>(args[0], int.Parse(args[1]));
            args = Console.ReadLine().Split();
            var intDouble = new MyTuple<int, double>(int.Parse(args[0]), double.Parse(args[1]));

            Console.WriteLine(name);
            Console.WriteLine(beerAmount);
            Console.WriteLine(intDouble);
        }
    }
}
