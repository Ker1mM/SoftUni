using System;
using System.Text;

namespace LinkedListTraversal
{
    public class StartUp
    {
        static void Main()
        {
            int count = int.Parse(Console.ReadLine());
            var myList = new MyLinkedList<long>();
            while (count-- > 0)
            {
                string[] args = Console.ReadLine().Split();
                long number = long.Parse(args[1]);
                switch (args[0])
                {
                    case "Add":
                        myList.Add(number);
                        break;
                    case "Remove":
                        myList.Remove(number);
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine(myList.Count);
            StringBuilder sb = new StringBuilder();
            foreach (var item in myList)
            {
                sb.Append($"{item} ");
            }
            Console.WriteLine(sb.ToString().TrimEnd());
        }
    }
}
