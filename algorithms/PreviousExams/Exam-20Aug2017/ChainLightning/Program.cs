using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainLightning
{
    class Program
    {
        static int nodeCount;
        static List<Edge> graph;
        static int[] damages;
        static void Main(string[] args)
        {
            nodeCount = int.Parse(Console.ReadLine());
            int edgeCount = int.Parse(Console.ReadLine());
            int lightningCount = int.Parse(Console.ReadLine());

            graph = new List<Edge>();

            for (int i = 0; i < edgeCount; i++)
            {
                var inputArgs = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var edge = new Edge(inputArgs[0], inputArgs[1], inputArgs[2]);
                graph.Add(edge);
            }

            damages = new int[nodeCount];
            var path = KruskalAlgorithm.Kruskal(nodeCount, graph, nodeCount);
            for (int i = 0; i < lightningCount; i++)
            {
                var inputArgs = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var visited = new bool[nodeCount];
                DFS(inputArgs[0], inputArgs[1], path, visited);
            }

            Console.WriteLine(damages.Max());
        }

        private static void DFS(int node, int damage, List<int>[] city, bool[] visited)
        {
            if (damage == 0)
            {
                return;
            }

            visited[node] = true;
            damages[node] += damage;

            foreach (var child in city[node])
            {
                if (!visited[child])
                {
                    DFS(child, damage / 2, city, visited);
                }
            }
        }
    }

    public class Edge : IComparable<Edge>
    {
        public Edge(int startNode, int endNode, int weight)
        {
            this.StartNode = startNode;
            this.EndNode = endNode;
            this.Weight = weight;
        }

        public int StartNode { get; set; }

        public int EndNode { get; set; }

        public int Weight { get; set; }

        public int CompareTo(Edge other)
        {
            int weightCompared = this.Weight.CompareTo(other.Weight);
            return weightCompared;
        }

        public override string ToString()
        {
            return $"({this.StartNode} {this.EndNode}) -> {this.Weight}";
        }
    }

    public class KruskalAlgorithm
    {
        public static List<int>[] Kruskal(int numberOfVertices, List<Edge> edges, int nodeCount)
        {
            var spannigTree = new List<int>[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                spannigTree[i] = new List<int>();
            }
            edges.Sort();

            var parent = new int[numberOfVertices];
            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = i;
            }

            foreach (var edge in edges)
            {
                int rootStartNode = FindRoot(edge.StartNode, parent);
                int rootEndNode = FindRoot(edge.EndNode, parent);

                if (rootStartNode != rootEndNode)
                {
                    spannigTree[edge.StartNode].Add(edge.EndNode);
                    spannigTree[edge.EndNode].Add(edge.StartNode);
                    parent[rootEndNode] = rootStartNode;
                }
            }

            return spannigTree;
        }

        public static int FindRoot(int node, int[] parent)
        {
            int root = node;
            while (parent[root] != root)
            {
                root = parent[root];
            }

            while (node != root)
            {
                int previousParent = parent[node];
                parent[node] = root;
                node = previousParent;
            }

            return root;
        }
    }
}
