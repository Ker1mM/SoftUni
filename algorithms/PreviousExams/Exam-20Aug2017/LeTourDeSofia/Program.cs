using System;
using System.Collections.Generic;
using System.Linq;

namespace LeTourDeSofia
{
    class Program
    {
        static int visitedJunctionsCount;
        static int startJunction;
        static void Main(string[] args)
        {
            int junctionCount = int.Parse(Console.ReadLine());
            int streetCount = int.Parse(Console.ReadLine());
            startJunction = int.Parse(Console.ReadLine());

            var road = new List<int>[junctionCount];

            int[] parents = new int[junctionCount];
            for (int i = 0; i < junctionCount; i++)
            {
                road[i] = new List<int>();
                parents[i] = -1;
            }

            for (int i = 0; i < streetCount; i++)
            {
                var junctions = Console.ReadLine().Split().Select(int.Parse).ToArray();
                road[junctions[0]].Add(junctions[1]);
            }

            bool[] visited = new bool[junctionCount];
            DFS(road, parents, startJunction, visited);

            if (parents[startJunction] >= 0)
            {
                int counter = 1;

                int parent = parents[startJunction];

                while (parent != startJunction)
                {
                    counter++;
                    parent = parents[parent];
                }

                visitedJunctionsCount = counter;
            }

            Console.WriteLine(visitedJunctionsCount);
        }

        private static void DFS(List<int>[] road, int[] parents, int start, bool[] visited)
        {
            var queue = new Queue<int>();
            queue.Enqueue(start);
            visited[start] = true;

            while (queue.Count > 0)
            {
                int next = queue.Dequeue();
                visitedJunctionsCount++;

                foreach (var street in road[next])
                {

                    if (visited[street] == false)
                    {
                        queue.Enqueue(street);
                        visited[street] = true;
                        parents[street] = next;
                    }

                    if (street == startJunction)
                    {
                        parents[street] = next;
                        return;
                    }
                }
            }
        }
    }
}
