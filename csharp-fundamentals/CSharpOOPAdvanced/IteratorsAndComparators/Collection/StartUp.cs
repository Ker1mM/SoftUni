using System;
using System.Linq;
using System.Text;

namespace IteratorsAndComparators
{
    public class StartUp
    {
        static void Main()
        {
            string[] args = Console.ReadLine().Split().Skip(1).ToArray();
            var myListyIterator = new ListyIterator<string>(args);

            StringBuilder sb = new StringBuilder();
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                try
                {
                    switch (input)
                    {
                        case "HasNext":
                            sb.AppendLine(myListyIterator.HasNext().ToString());
                            break;
                        case "Print":
                            sb.AppendLine(myListyIterator.Print());
                            break;
                        case "Move":
                            sb.AppendLine(myListyIterator.Move().ToString());
                            break;
                        case "PrintAll":
                            sb.AppendLine(myListyIterator.PrintAll());
                            break;
                        default:
                            break;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    sb.AppendLine("Invalid Operation!");
                }
            }

            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}
