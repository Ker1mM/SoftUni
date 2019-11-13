using System;

namespace CustomList
{
    public class StartUp
    {
        static void Main()
        {
            //•	Add < element > -Adds the given element to the end of the list
            //•	Remove < index > -Removes the element at the given index
            //•	Contains < element > -Prints if the list contains the given element(True or False)
            //•	Swap<index> < index > -Swaps the elements at the given indexes
            //•	Greater < element > -Counts the elements that are greater than the given element and prints their count
            //•	Max - Prints the maximum element in the list
            //•	Min - Prints the minimum element in the list
            //•	Print - Prints all of the elements in the list, each on a separate line
            //•	END - stops the reading of commands

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
                    default:
                        break;
                }
            }
        }
    }
}
