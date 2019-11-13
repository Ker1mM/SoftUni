using System.Collections.Generic;

namespace StudentSystem
{
    class StartUp
    {
        static void Main()
        {
            Dictionary<string, Student> studentSystem = new Dictionary<string, Student>();
            string input;
            while ((input = System.Console.ReadLine()) != "Exit")
            {
                if (StudentSystem.TryGetInfo(studentSystem, input, out string printInfo))
                {
                    System.Console.WriteLine(printInfo);
                }
            }
        }
    }
}
