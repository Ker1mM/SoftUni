using System;
using System.Collections.Generic;
using System.Linq;

namespace FindBiConnectedComponents
{
    class Program
    {
        private static List<int>[] graph;
        private static int nodes;
        private static int edges;

        static void Main(string[] args)
        {
            ReadGraph();
            var result = ArticulationPoints.FindArticulationPoints(graph);
            Console.WriteLine($"Number of bi-connected components: {result}");
        }

        private static void ReadGraph()
        {
            nodes = int.Parse(Console.ReadLine().Split()[1]);
            edges = int.Parse(Console.ReadLine().Split()[1]);

            graph = new List<int>[nodes];

            for (int i = 0; i < edges; i++)
            {
                var input = Console.ReadLine().Split().ToArray();
                var start = int.Parse(input[0]);
                var end = int.Parse(input[1]);

                if (graph[start] == null)
                {
                    graph[start] = new List<int>();
                }
                if (graph[end] == null)
                {
                    graph[end] = new List<int>();
                }

                graph[start].Add(end);
                graph[end].Add(start);
            }
        }
    }

    public static class ArticulationPoints
    {
        private static List<int>[] graph;
        private static int?[] parents;
        private static int[] visitedTime;
        private static int[] lowPoint;
        private static bool[] visited;
        private static List<int> articulationPoints;
        private static int time;
        private static int root;
        private static int counter;

        public static int FindArticulationPoints(List<int>[] targetGraph)
        {
            graph = targetGraph;
            counter = 0;
            visitedTime = new int[targetGraph.Length];
            lowPoint = new int[targetGraph.Length];

            parents = new int?[targetGraph.Length];
            visited = new bool[targetGraph.Length];

            articulationPoints = new List<int>();
            time = 0;
            ;

            for (int i = 0; i < graph.Length; i++)
            {
                if (!visited[i])
                {
                    root = i;
                    DFS(i);
                }
            }

            return counter;
        }

        private static void DFS(int node)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;
            visitedTime[node] = time;
            lowPoint[node] = time;
            time++;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    parents[child] = node;
                    DFS(child);

                    if (visitedTime[node] <= lowPoint[child])
                    {
                        counter++;
                        articulationPoints.Add(node);
                    }

                    lowPoint[node] = Math.Min(lowPoint[node], lowPoint[child]);
                }
                else
                {

                    lowPoint[node] = Math.Min(lowPoint[node], visitedTime[child]);
                }
            }
        }
    }

}
