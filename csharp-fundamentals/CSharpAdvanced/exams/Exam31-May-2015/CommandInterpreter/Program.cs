using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            List<string> collection = input.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            string command = Console.ReadLine();
            while (command != "end")
            {
                string[] tokens = command.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (tokens[0] == "rollLeft" || tokens[0] == "rollRight")
                {
                    int count = -1;
                    int.TryParse(tokens[1], out count);
                    if (count >= 0)
                    {
                        Roll(collection, count, tokens[0]);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input parameters.");
                    }
                }
                else
                {
                    int index = -1;
                    int count = -1;
                    int.TryParse(tokens[2], out index);
                    int.TryParse(tokens[4], out count);
                    if (count < 0 || index < 0 || index >= collection.Count || count + index > collection.Count)
                    {
                        Console.WriteLine("Invalid input parameters.");
                    }
                    else
                    {
                        List<string> workCollection = collection.GetRange(index, count);
                        if (tokens[0] == "reverse")
                        {
                            workCollection.Reverse();
                        }
                        else if (tokens[0] == "sort")
                        {
                            workCollection = workCollection.OrderBy(x => x).ToList();
                        }
                        collection.RemoveRange(index, count);
                        collection.InsertRange(index, workCollection);
                    }

                }
                command = Console.ReadLine();
            }
            Console.WriteLine("[{0}]", string.Join(", ", collection));
        }

        static void Roll(List<string> collection, int count, string direction)
        {
            int collecLength = collection.Count;
            count %= collecLength;
            if (direction.Equals("rollLeft"))
            {
                while (count-- > 0)
                {
                    collection.Add(collection[0]);
                    collection.RemoveAt(0);
                }
            }
            else if (direction.Equals("rollRight"))
            {
                while (count-- > 0)
                {
                    collection.Insert(0, collection[collection.Count - 1]);
                    collection.RemoveAt(collection.Count - 1);
                }
            }
        }
    }
}
