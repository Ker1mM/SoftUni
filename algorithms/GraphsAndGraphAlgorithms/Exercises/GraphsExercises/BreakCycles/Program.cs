using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreakCycles
{
    class Program
    {
        private static Dictionary<string, List<string>> graph;

        static void Main(string[] args)
        {
            graph = new Dictionary<string, List<string>>();
            string input = Console.ReadLine();

            while (!string.IsNullOrWhiteSpace(input))
            {
                var inputArgs = input.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                var node = inputArgs[0];
                var connectedNodes = inputArgs[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (!graph.ContainsKey(node))
                {
                    graph.Add(node, new List<string>());
                }
                graph[node].AddRange(connectedNodes);

                input = Console.ReadLine();
            }

            graph = graph
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            var edgesToRemove = new List<string>();

            var startNodes = graph.Keys.ToList();

            foreach (var startNode in startNodes)
            {
                var endNodes = graph[startNode]
                    .Where(x => startNodes.IndexOf(x) >= startNodes.IndexOf(startNode))
                    .OrderBy(x => x)
                    .ToList();

                foreach (var endNode in endNodes)
                {

                    var visited = new List<string>();
                    RemoveEdge(startNode, endNode);
                    if (CanReach(startNode, endNode, visited))
                    {
                        edgesToRemove.Add($"{startNode} - {endNode}");
                    }
                    else
                    {
                        graph[startNode].Remove(endNode);
                        graph[endNode].Remove(startNode);
                    }
                }
            }

            Console.WriteLine($"Edges to remove: {edgesToRemove.Count()}");
            Console.WriteLine(string.Join(Environment.NewLine, edgesToRemove));
        }

        private static void RemoveEdge(string start, string end)
        {
            graph[start].Remove(end);
            graph[end].Remove(start);
        }

        private static bool CanReach(string source, string destination, List<string> visited)
        {
            if (source == destination)
            {
                return true;
            }

            visited.Add(source);
            foreach (var childNodes in graph[source])
            {
                if (!visited.Contains(childNodes))
                {
                    return CanReach(childNodes, destination, visited);
                }
            }

            return false;
        }
    }
}
