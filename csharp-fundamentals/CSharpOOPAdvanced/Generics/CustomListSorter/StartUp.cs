using System;

namespace CustomListSorter
{
    public class StartUp
    {
        static void Main()
        {
            AnyType<string> myList = new AnyType<string>();
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] args = input.Split();
                string command = args[0];

                switch (command)
                {
                    case "Add":
                        string element = args[1];
                        myList.Add(element);
                        break;
                    case "Remove":
                        int index = int.Parse(args[1]);
                        myList.Remove(index);
                        break;
                    case "Contains":
                        string elementToFind = args[1];
                        Console.WriteLine(myList.Contains(elementToFind));
                        break;
                    case "Swap":
                        int index1 = int.Parse(args[1]);
                        int index2 = int.Parse(args[2]);
                        myList.Swap(index1, index2);
                        break;
                    case "Greater":
                        string elementToCompare = args[1];
                        Console.WriteLine(myList.CountGreaterThan(elementToCompare));
                        break;
                    case "Max":
                        Console.WriteLine(myList.Max());
                        break;
                    case "Min":
                        Console.WriteLine(myList.Min());
                        break;
                    case "Print":
                        Console.WriteLine(string.Join(Environment.NewLine, myList));
                        break;
                    case "Sort":
                        myList.Sort();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
