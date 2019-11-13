using System;
using System.Collections.Generic;

namespace MostReliablePath
{
    class Program
    {
        private static int Nodes;
        private static int Edges;
        private static int[,] Graph;
        private static int[] Path;
        private static double[] Reliability;
        private static bool[] Visited;
        private static List<int>[] bestPathway;

        static void Main(string[] args)
        {
            ReadGraph();

            for (int i = 0; i < Nodes; i++)
            {
                if (Reliability[i] == -1)
                {
                    double nodeRealiability = CalculateReliability(i);
                    Reliability[i] = nodeRealiability;
                }
            }


            Console.WriteLine($"Most reliable path reliability: {Reliability[Path[1]] * 100:f2}%");

            bestPathway[Path[1]].Reverse();
            var road = bestPathway[Path[1]];
            Console.WriteLine(string.Join(" -> ", road));

        }

        private static double CalculateReliability(int node)
        {
            if (Reliability[node] != -1)
            {
                return Reliability[node];
            }

            int bestEdgeNode = FindBestEdge(node);
            if (node == bestEdgeNode)
            {
                return 0;
            }


            Visited[bestEdgeNode] = true;
            var temp = CalculateReliability(bestEdgeNode);
            Visited[bestEdgeNode] = false;
            Reliability[bestEdgeNode] = temp;

            bestPathway[node].AddRange(bestPathway[bestEdgeNode]);
            return temp * (Graph[node, bestEdgeNode] / 100.0);
        }

        private static int FindBestEdge(int node)
        {
            int maxReliability = -1;
            int bestNode = node;
            for (int i = 0; i < Nodes; i++)
            {
                if (Graph[node, i] > 0 && Graph[node, i] > maxReliability && !Visited[i])
                {
                    maxReliability = Graph[node, i];
                    bestNode = i;
                }
            }

            return bestNode;
        }

        private static void ReadGraph()
        {
            Nodes = int.Parse(Console.ReadLine().Split()[1]);

            var pathArgs = Console.ReadLine().Split();
            Path = new int[2];
            Path[0] = int.Parse(pathArgs[1]);
            Path[1] = int.Parse(pathArgs[3]);

            Edges = int.Parse(Console.ReadLine().Split()[1]);
            Graph = new int[Nodes, Nodes];

            Reliability = new double[Nodes];
            Visited = new bool[Nodes];
            bestPathway = new List<int>[Nodes];

            for (int i = 0; i < Nodes; i++)
            {
                Reliability[i] = -1;
                bestPathway[i] = new List<int> { i };
            }

            Reliability[Path[0]] = 1;

            for (int i = 0; i < Edges; i++)
            {
                var input = Console.ReadLine();
                var edge = new Edge(input);

                Graph[edge.StartNode, edge.EndNode] = edge.Weight;
                Graph[edge.EndNode, edge.StartNode] = edge.Weight;
            }
        }
    }

    class Edge
    {
        public int StartNode { get; set; }
        public int EndNode { get; set; }
        public int Weight { get; set; }

        public Edge(string inputString)
        {
            this.ReadInput(inputString);
        }

        private void ReadInput(string input)
        {
            var inpurArgs = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int startNode = int.Parse(inpurArgs[0]);
            int endNode = int.Parse(inpurArgs[1]);
            int weight = int.Parse(inpurArgs[2]);

            this.StartNode = startNode;
            this.EndNode = endNode;
            this.Weight = weight;
        }

        public override string ToString()
        {
            return $"({this.StartNode} {this.EndNode}) -> {this.Weight}";
        }
    }
}
