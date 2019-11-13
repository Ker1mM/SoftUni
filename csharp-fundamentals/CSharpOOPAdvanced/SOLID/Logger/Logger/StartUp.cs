using System;

namespace Logger
{
    public class StartUp
    {
        static void Main()
        {
            int count = int.Parse(Console.ReadLine());
            var engine = new Engine();
            engine.Run(count);
        }
    }
}
