using System;

namespace P4_Telephony
{
    public class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split();
            var urls = Console.ReadLine().Split();
            var phone = new Smartphone("NOKIA");

            foreach (var number in numbers)
            {
                try
                {
                    phone.Call(number);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (var url in urls)
            {
                try
                {
                    phone.Browse(url);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
