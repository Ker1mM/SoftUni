using System;
using System.Collections.Generic;
using System.Linq;

namespace DistanceBetweenVertices
{
    class Program
    {
        private static Dictionary<int, List<int>> graph;

        static void Main(string[] args)
        {
            int nodeCount = int.Parse(Console.ReadLine());
            int pairCount = int.Parse(Console.ReadLine());

            graph = new Dictionary<int, List<int>>();

            for (int i = 1; i <= nodeCount; i++)
            {
                string[] edgeInput = Console.ReadLine().Split(':').ToArray();

                var node = int.Parse(edgeInput[0]);
                var connectedNodes = edgeInput[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                if (!graph.ContainsKey(node))
                {
                    graph.Add(node, new List<int>());
                }

                graph[node] = connectedNodes;
            }

            var decoder = new List<int> { -1 };

            foreach (var key in graph.Keys)
            {
                decoder.Add(key);
            }

            for (int i = 0; i < pairCount; i++)
            {
                int[] pair = Console.ReadLine().Split('-').Select(int.Parse).ToArray();
                int source = decoder.IndexOf(pair[0]);
                int destination = decoder.IndexOf(pair[1]);
                int distance = FindShortestDistance(source, destination, decoder);

                Console.WriteLine($"{{{pair[0]}, {pair[1]}}} -> {distance}");
            }
        }

        private static int FindShortestDistance(int source, int destination, List<int> decoder)
        {
            int nodeCount = graph.Count;
            bool[] visited = new bool[nodeCount + 1];
            int[] distance = new int[nodeCount + 1];
            for (int i = 0; i < distance.Length; i++)
            {
                distance[i] = -1;
            }
            int[] predecessors = new int[nodeCount + 1];
            predecessors[source] = source;

            Queue<int> priorityQueue = new Queue<int>();
            priorityQueue.Enqueue(source);

            while (priorityQueue.Count > 0)
            {
                int currentNode = priorityQueue.Dequeue();
                foreach (var childDTO in graph[decoder[currentNode]])
                {
                    int child = decoder.IndexOf(childDTO);
                    if (!visited[child])
                    {
                        predecessors[child] = currentNode;
                        priorityQueue.Enqueue(child);
                        visited[child] = true;
                    }
                }
                var newDistance = distance[predecessors[currentNode]] + 1;
                if (distance[currentNode] == -1 || distance[currentNode] > newDistance)
                {
                    distance[currentNode] = newDistance;
                }

                if (currentNode == destination)
                {
                    break;
                }
            }

            return distance[destination];
        }

    }
}
