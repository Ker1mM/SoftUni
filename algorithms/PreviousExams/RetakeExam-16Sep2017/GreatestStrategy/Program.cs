using System;
using System.Collections.Generic;
using System.Linq;

namespace GreatestStrategy
{
    class Program
    {
        static int[] componentCount;
        static int[] componentSum;
        static int[] parents;
        static List<int>[] graph;
        static bool[] visited;
        static int maxSum;

        static void Main(string[] args)
        {
            var inputArgs = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int areaCount = inputArgs[0];
            int connectionCount = inputArgs[1];
            int startArea = inputArgs[2];

            graph = new List<int>[areaCount + 1];
            for (int i = 0; i <= areaCount; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 1; i <= connectionCount; i++)
            {
                inputArgs = Console.ReadLine().Split().Select(int.Parse).ToArray();
                graph[inputArgs[0]].Add(inputArgs[1]);
                graph[inputArgs[1]].Add(inputArgs[0]);
            }

            componentCount = new int[areaCount + 1];
            componentSum = new int[areaCount + 1];
            parents = new int[areaCount + 1];
            visited = new bool[areaCount + 1];

            parents[startArea] = startArea;
            ;
            DFS(startArea);
            visited = new bool[areaCount + 1];
            CutConnections(startArea);

            Console.WriteLine(maxSum);
            ;
        }

        private static void CutConnections(int index)
        {
            visited[index] = true;
            for (int i = 0; i < graph[index].Count; i++)
            {
                var child = graph[index][i];
                if (visited[child] == false)
                {
                    CutConnections(child);
                }
            }

            if (componentCount[index] % 2 == 0)
            {
                maxSum = maxSum < componentSum[index] ? componentSum[index] : maxSum;

                Subtract(index);
            }
        }

        private static void Subtract(int index)
        {
            int parent = parents[index];
            int sum = componentSum[index];
            int count = componentCount[index];

            while (true)
            {
                componentSum[parent] -= sum;
                componentCount[parent] -= count;

                if (parent == parents[parent])
                {
                    break;
                }
                parent = parents[parent];
            }

        }

        private static void DFS(int index)
        {
            int count = 1;
            int sum = index;
            visited[index] = true;
            foreach (var child in graph[index])
            {
                if (visited[child] == false)
                {
                    DFS(child);
                    parents[child] = index;
                    count += componentCount[child];
                    sum += componentSum[child];
                }
            }
            componentSum[index] = sum;
            componentCount[index] = count;
        }
    }
}
