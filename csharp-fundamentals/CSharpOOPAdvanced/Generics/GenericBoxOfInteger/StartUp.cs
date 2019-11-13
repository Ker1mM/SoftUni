using System;

namespace GenericBox
{
    public class StartUp
    {
        static void Main()
        {
            int count = int.Parse(Console.ReadLine());

            while (count-- > 0)
            {
                string input = Console.ReadLine();
                Box<int> generic = new Box<int>(int.Parse(input));

                Console.WriteLine(generic);
            }
        }
    }
}
