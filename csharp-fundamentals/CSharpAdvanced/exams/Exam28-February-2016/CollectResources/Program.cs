using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectResources
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> originalField = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            int pathCount = int.Parse(Console.ReadLine());
            long maxCollected = 0;
            for (int i = 0; i < pathCount; i++)
            {
                List<string> field = new List<string>(originalField);
                string[] tokens = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                int index = int.Parse(tokens[0]);
                int step = int.Parse(tokens[1]);
                step %= field.Count;
                long currentCollected = 0;
                while (true)
                {
                    if (field[index] == "")
                    {
                        if (currentCollected > maxCollected)
                        {
                            maxCollected = currentCollected;
                        }
                        break;
                    }
                    else
                    {
                        string[] resource = field[index].ToLower().Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                        if (resource[0] == "stone" ||
                            resource[0] == "gold" ||
                            resource[0] == "wood" ||
                            resource[0] == "food")
                        {
                            if (resource.Length == 1)
                            {
                                currentCollected++;
                            }
                            else
                            {
                                currentCollected += int.Parse(resource[1]);
                            }
                            field[index] = "";
                        }
                    }

                    index += step;
                    if (index >= field.Count)
                    {
                        index %= field.Count;
                    }
                }
            }
            Console.WriteLine(maxCollected);
        }
    }
}
