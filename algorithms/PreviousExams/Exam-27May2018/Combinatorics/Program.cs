using System;
using System.Collections.Generic;
using System.Linq;

namespace Combinatorics
{
    class Program
    {
        static bool[] visited;

        private static void DFS(int vertex, List<int>[] graph, int ap)
        {
            if (!visited[vertex] && vertex != ap)
            {
                visited[vertex] = true;
                foreach (var child in graph[vertex])
                {
                    DFS(child, graph, ap);
                }
            }
        }

        private static int GetConnectedcomponentCount(List<int>[] graph, int ap)
        {
            int count = 0;
            visited = new bool[graph.Length];

            for (int i = 0; i < graph.Length; i++)
            {
                if (!visited[i] && i != ap)
                {
                    count++;
                    DFS(i, graph, ap);
                }
            }

            return count;
        }
        static void Main(string[] args)
        {
            int connectiongParts = int.Parse(Console.ReadLine());
            int toPart = int.Parse(Console.ReadLine());

            var graph = new List<int>[connectiongParts];

            for (int i = 0; i < connectiongParts; i++)
            {
                var input = Console.ReadLine().Split().Select(int.Parse).Select(x => x -= 1).ToList();

                graph[i] = input;
            }

            var initial = GetConnectedcomponentCount(graph, -1);
            var res = ArticulationPoints.FindArticulationPoints(graph);

            if (initial != 1)
            {
                Console.WriteLine(-2);
            }
            else if (res.Count == 0)
            {
                Console.WriteLine(-1);
            }
            else
            {
                bool found = false;
                foreach (var item in res)
                {
                    int curRes = GetConnectedcomponentCount(graph, item);

                    if (curRes == toPart)
                    {
                        Console.WriteLine(item + 1);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Console.WriteLine(0);
                }

            }

        }
    }

    public class ArticulationPoints
    {
        private static List<int>[] graph;
        private static bool[] visited;
        private static int?[] parent;
        private static int[] depths;
        private static int[] lowpoint;
        private static List<int> articulationPoints;

        public static List<int> FindArticulationPoints(List<int>[] targetGraph)
        {
            graph = targetGraph;
            visited = new bool[graph.Length];
            parent = new int?[graph.Length];
            depths = new int[graph.Length];
            lowpoint = new int[graph.Length];
            articulationPoints = new List<int>();

            FindArticulationPoints(0, 0);

            return articulationPoints;
        }

        private static void FindArticulationPoints(int node, int depth)
        {
            visited[node] = true;
            depths[node] = depth;
            lowpoint[node] = depth;

            int childCount = 0;
            bool isArticulationPoint = false;

            foreach (var childNode in graph[node])
            {
                if (!visited[childNode])
                {
                    parent[childNode] = node;
                    FindArticulationPoints(childNode, depth + 1);
                    childCount++;

                    if (lowpoint[childNode] >= depths[node])
                    {
                        isArticulationPoint = true;
                    }

                    lowpoint[node] = Math.Min(lowpoint[node], lowpoint[childNode]);
                }
                else if (childNode != parent[node])
                {
                    lowpoint[node] = Math.Min(lowpoint[node], depths[childNode]);
                }
            }

            if ((parent[node] != null && isArticulationPoint) || (parent[node] == null && childCount > 1))
            {
                articulationPoints.Add(node);
            }
        }
    }
}
