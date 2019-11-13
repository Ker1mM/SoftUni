using System;

namespace ThreeupleExercise
{
    public class StartUp
    {
        static void Main()
        {
            string[] args = Console.ReadLine().Split();
            var address = new MyThreeuple<string, string, string>(args[0] + " " + args[1], args[2], args[3]);

            args = Console.ReadLine().Split();
            bool drunk = args[2] == "drunk";
            var beerAmount = new MyThreeuple<string, int, bool>(args[0], int.Parse(args[1]), drunk);

            args = Console.ReadLine().Split();
            var bankAccount = new MyThreeuple<string, double, string>(args[0], double.Parse(args[1]), args[2]);


            Console.WriteLine(address);
            Console.WriteLine(beerAmount);
            Console.WriteLine(bankAccount);
        }
    }
}
