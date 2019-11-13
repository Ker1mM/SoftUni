using System;

namespace P3_Ferrari
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string name = Console.ReadLine();
            ICar car = new Ferrari(name);

            Console.WriteLine(car.ToString());
        }
    }
}
