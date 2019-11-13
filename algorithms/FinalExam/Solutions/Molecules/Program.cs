using System;
using System.Collections.Generic;
using System.Linq;

namespace Molecules
{

    public static class ShortestPath
    {
        static bool[] visited;
        static int[,] graph;

        public static List<int> FindNotConnectedMolecules(int[,] graph, int source)
        {
            visited = new bool[graph.GetLength(0)];
            DFS(source, graph);

            var result = new List<int>();
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                if (visited[i] == false)
                {
                    result.Add(i + 1);
                }
            }

            return result;

        }
        private static void DFS(int vertex, int[,] graph)
        {
            if (!visited[vertex])
            {
                visited[vertex] = true;
                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    if (graph[vertex, i] > 0)
                    {
                        DFS(i, graph);
                    }
                }
            }
        }

        public static int FindShortestPath(int[,] graph, int sourceNode, int destinationNode)
        {
            int n = graph.GetLength(0);
            int[] distance = new int[n];

            for (int i = 0; i < n; i++)
            {
                distance[i] = int.MaxValue;
            }

            distance[sourceNode] = 0;
            var used = new bool[n];
            int?[] previous = new int?[n];

            while (true)
            {
                int minDistance = int.MaxValue;
                int minNode = 0;
                for (int node = 0; node < n; node++)
                {
                    if (!used[node] && distance[node] < minDistance)
                    {
                        minDistance = distance[node];
                        minNode = node;
                    }
                }

                if (minDistance == int.MaxValue)
                {
                    break;
                }

                used[minNode] = true;

                for (int i = 0; i < n; i++)
                {
                    if (graph[minNode, i] > 0)
                    {
                        int newDistance = minDistance + graph[minNode, i];
                        if (newDistance < distance[i])
                        {
                            distance[i] = newDistance;
                            previous[i] = minNode;
                        }
                    }
                }
            }

            if (distance[destinationNode] == int.MaxValue)
            {
                return 0;
            }

            var path = new List<int>();
            int? currentNode = destinationNode;
            while (currentNode != null)
            {
                path.Add(currentNode.Value);
                currentNode = previous[currentNode.Value];
            }

            path.Reverse();

            int result = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                result += graph[path[i], path[i + 1]];
            }

            return result;
        }
    }

    class Program
    {
        static int[,] graph;
        static void Main(string[] args)
        {
            int moleculeCount = int.Parse(Console.ReadLine());
            int connectionCount = int.Parse(Console.ReadLine());
            graph = new int[moleculeCount, moleculeCount];


            for (int i = 0; i < connectionCount; i++)
            {
                var inputArgs = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int start = inputArgs[0] - 1;
                int end = inputArgs[1] - 1;
                int weight = inputArgs[2];

                graph[start, end] = weight;
            }

            var sourceDestinationArgs = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int startNode = sourceDestinationArgs[0] - 1;
            int endNode = sourceDestinationArgs[1] - 1;

            var notConnectedMolecules = ShortestPath.FindNotConnectedMolecules(graph, startNode);
            var minCost = ShortestPath.FindShortestPath(graph, startNode, endNode);


            Console.WriteLine(minCost);
            Console.WriteLine(string.Join(" ", notConnectedMolecules));

        }
    }
}
