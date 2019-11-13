using System;
using System.Collections.Generic;
using System.Linq;

namespace CyclesInAGraph
{
    class Program
    {
        private static Dictionary<string, List<string>> graph;
        private static Dictionary<string, bool> visited;

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            graph = new Dictionary<string, List<string>>();
            visited = new Dictionary<string, bool>();

            while (!string.IsNullOrEmpty(input))
            {
                string[] edge = input.Split(new char[] { '–' }, StringSplitOptions.RemoveEmptyEntries);

                if (!graph.ContainsKey(edge[0]))
                {
                    graph.Add(edge[0], new List<string>());
                }
                if (!graph.ContainsKey(edge[1]))
                {
                    graph.Add(edge[1], new List<string>());
                }

                if (!graph[edge[0]].Contains(edge[1]))
                {
                    graph[edge[0]].Add(edge[1]);
                }

                if (!graph[edge[1]].Contains(edge[0]))
                {
                    graph[edge[1]].Add(edge[0]);
                }


                if (!visited.ContainsKey(edge[0]))
                {
                    visited.Add(edge[0], false);
                }

                if (!visited.ContainsKey(edge[1]))
                {
                    visited.Add(edge[1], false);
                }

                input = Console.ReadLine();
            }

            var node = graph.Keys.FirstOrDefault();
            bool hasCycle = HasCycle(node, node);

            string result = hasCycle ? "No" : "Yes";
            Console.WriteLine($"Acyclic: {result}");
        }

        private static bool HasCycle(string node, string pred)
        {
            bool cycleFound = false;

            if (visited[node])
            {
                return true;
            }
            else
            {
                visited[node] = true;
                foreach (var childNode in graph[node])
                {
                    if (childNode != pred)
                    {
                        cycleFound = HasCycle(childNode, node);
                        if (cycleFound)
                        {
                            break;
                        }
                    }
                }
            }

            return cycleFound;
        }
    }
}
