using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClusterBorder
{
    class Program
    {
        static int[] singleShipTimes;
        static int[] combinedShipTimes;
        static int minTime;
        static string bestOrder;
        static void Main(string[] args)
        {
            singleShipTimes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            combinedShipTimes = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int?[] values = new int?[combinedShipTimes.Length];

            for (int i = 0; i < combinedShipTimes.Length; i++)
            {
                int singleTime = singleShipTimes[i] + singleShipTimes[i + 1];

                if (combinedShipTimes[i] < singleTime)
                {
                    values[i] = singleTime - combinedShipTimes[i];
                }
            }

            int maxValueIndex = FindMaxValueIndex(values);
            bool[] visited = new bool[singleShipTimes.Length];

            while (maxValueIndex != -1)
            {
                values[maxValueIndex] = null;

                if (visited[maxValueIndex] == false && visited[maxValueIndex + 1] == false)
                {
                    visited[maxValueIndex] = true;
                    visited[maxValueIndex + 1] = true;
                }

                maxValueIndex = FindMaxValueIndex(values);
            }

            var sb = new StringBuilder();

            for (int i = 0; i < visited.Length; i++)
            {
                if (visited[i])
                {
                    sb.AppendLine($"Pair of {i + 1} and {i + 2}");
                    minTime += combinedShipTimes[i];
                    i++;
                }
                else
                {
                    sb.AppendLine($"Single {i + 1}");
                    minTime += singleShipTimes[i];
                }
            }

            Console.WriteLine($"Optimal Time: {minTime}");
            Console.WriteLine(sb.ToString().TrimEnd());
            ;
        }

        private static int FindMaxValueIndex(int?[] values)
        {
            int maxIndex = -1;

            int maxValue = 0;

            for (int i = 0; i < values.Length; i++)
            {
                int? item = values[i];

                if (item != null && maxValue < item)
                {
                    maxValue = (int)item;
                    maxIndex = i;

                }
            }

            return maxIndex;
        }


    }
}
