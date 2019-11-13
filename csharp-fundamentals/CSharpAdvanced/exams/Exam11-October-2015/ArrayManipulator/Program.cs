using System;
using System.Collections.Generic;
using System.Linq;

namespace ArrayManipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> array = Console.ReadLine().Split().Select(int.Parse).ToList();

            string command = Console.ReadLine();
            while (command.Equals("end") == false)
            {
                string[] tokens = command.Split();
                switch (tokens[0])
                {
                    case "exchange":
                        int index = int.Parse(tokens[1]);
                        Exchange(array, index);
                        break;
                    case "max":
                        PrintMax(array, tokens[1]);
                        break;
                    case "min":
                        PrintMin(array, tokens[1]);
                        break;
                    case "first":
                        int countFirst = int.Parse(tokens[1]);
                        PrintFirstOrLast(array, countFirst, tokens[2], tokens[0]);
                        break;
                    case "last":
                        int countLast = int.Parse(tokens[1]);
                        PrintFirstOrLast(array, countLast, tokens[2], tokens[0]);
                        break;
                    default:
                        break;
                }
                command = Console.ReadLine();
            }
            Console.WriteLine("[{0}]", string.Join(", ", array));
        }

        static void PrintFirstOrLast(List<int> array, int count, string oddEven, string firstOrLast)
        {
            if (count > array.Count)
            {
                Console.WriteLine("Invalid count");
            }
            else if (oddEven == "even")
            {
                var temp = array.Where(x => x % 2 == 0).ToList();
                if (firstOrLast == "first")
                {
                    Console.WriteLine("[{0}]", string.Join(", ", temp.Take(count)));
                }
                else if (firstOrLast == "last")
                {
                    count = temp.Count - count < 0 ? 0 : temp.Count - count;
                    Console.WriteLine("[{0}]", string.Join(", ", temp.Skip(count)));
                }
            }
            else if (oddEven == "odd")
            {
                var temp = array.Where(x => x % 2 != 0).ToList();
                if (firstOrLast == "first")
                {
                    Console.WriteLine("[{0}]", string.Join(", ", temp.Take(count)));
                }
                else if (firstOrLast == "last")
                {
                    count = temp.Count - count < 0 ? 0 : temp.Count - count;
                    Console.WriteLine("[{0}]", string.Join(", ", temp.Skip(count)));
                }
            }
        }

        static void PrintMin(List<int> array, string evenOdd)
        {
            int index = -1;
            int maxNumber = 1001;
            if (evenOdd == "even")
            {
                for (int i = 0; i < array.Count; i++)
                {
                    if (array[i] % 2 == 0)
                    {
                        if (array[i] <= maxNumber)
                        {
                            maxNumber = array[i];
                            index = i;
                        }
                    }
                }
            }
            else if (evenOdd == "odd")
            {
                for (int i = 0; i < array.Count; i++)
                {
                    if (array[i] % 2 != 0)
                    {
                        if (array[i] <= maxNumber)
                        {
                            maxNumber = array[i];
                            index = i;
                        }
                    }
                }
            }
            if (index < 0)
            {
                Console.WriteLine("No matches");
            }
            else
            {
                Console.WriteLine(index);
            }
        }

        static void PrintMax(List<int> array, string evenOdd)
        {
            int index = -1;
            int maxNumber = -1;
            if (evenOdd == "even")
            {
                for (int i = 0; i < array.Count; i++)
                {
                    if (array[i] % 2 == 0)
                    {
                        if (array[i] >= maxNumber)
                        {
                            maxNumber = array[i];
                            index = i;
                        }
                    }
                }
            }
            else if (evenOdd == "odd")
            {
                for (int i = 0; i < array.Count; i++)
                {
                    if (array[i] % 2 != 0)
                    {
                        if (array[i] >= maxNumber)
                        {
                            maxNumber = array[i];
                            index = i;
                        }
                    }
                }
            }
            if (index < 0)
            {
                Console.WriteLine("No matches");
            }
            else
            {
                Console.WriteLine(index);
            }
        }

        static void Exchange(List<int> array, int index)
        {
            if (index < 0 || index >= array.Count)
            {
                Console.WriteLine("Invalid index");
            }
            else if (index < array.Count - 1)
            {
                index++;
                var temp = array.GetRange(index, array.Count - index);
                array.RemoveRange(index, array.Count - index);
                array.InsertRange(0, temp);
            }
        }
    }
}
