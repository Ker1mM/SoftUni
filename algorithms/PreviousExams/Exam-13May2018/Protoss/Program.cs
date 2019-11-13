using System;
using System.Collections.Generic;
using System.Linq;

namespace Protoss
{
    class Program
    {
        private static int GetConnections(int index, List<int>[] graph)
        {
            var q = new List<int>();
            var visited = new bool[graph.Length];
            visited[index] = true;

            int connections = 0;

            foreach (var child in graph[index])
            {
                if (!visited[child])
                {
                    visited[child] = true;
                    q.Add(child);
                    connections++;
                }
            }

            foreach (var parent in q)
            {
                foreach (var child in graph[parent])
                {
                    if (!visited[child])
                    {
                        visited[child] = true;
                        connections++;
                    }
                }
            }

            return connections;
        }

        static void Main(string[] args)
        {
            int warriorCount = int.Parse(Console.ReadLine());
            List<int>[] graph = new List<int>[warriorCount];

            for (int i = 0; i < warriorCount; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < warriorCount; i++)
            {
                var input = Console.ReadLine().ToCharArray().ToList(); ;

                var index = input.IndexOf('Y');
                while (index != -1)
                {
                    graph[i].Add(index);
                    input[index] = 'N';
                    index = input.IndexOf('Y');
                }
            }

            int maxConnections = 0;

            for (int i = 0; i < warriorCount; i++)
            {
                int connections = GetConnections(i, graph);

                if (maxConnections < connections)
                {
                    maxConnections = connections;
                }
            }

            Console.WriteLine(maxConnections);
        }
    }
}
