using System;

namespace ActionPoint
{
    class Program
    {
        static void Main(string[] args)
        {
            //If it works, it ain't stupid!
            Action<string> PrintOnNewLine = n => Console.WriteLine(n.Replace(" ", "\n"));
            PrintOnNewLine(Console.ReadLine());
        }
    }
}
