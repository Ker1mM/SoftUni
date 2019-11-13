using System;
using System.Linq;

namespace Froggy
{
    class StartUp
    {
        static void Main()
        {
            var stones = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var lake = new Lake(stones);

            Console.WriteLine(string.Join(", ", lake));
        }
    }
}
