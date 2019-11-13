using System;
using System.Collections.Generic;
using System.Linq;

namespace ModifiedKruskalAlgorithm
{
    class Program
    {
        private static int Nodes;
        private static int Edges;
        private static List<Edge> Graph;
        private static int[] Roots;


        static void Main(string[] args)
        {
            ReadGraph();
            var resultGraph = new List<Edge>();


            for (int i = 0; i < Graph.Count; i++)
            {
                var currentEdge = Graph[i];
                if (Roots[currentEdge.StartNode] != Roots[currentEdge.EndNode])
                {
                    resultGraph.Add(currentEdge);
                    SetRoots(currentEdge);
                }
            }

            Console.WriteLine($"Minimum spanning forest weight: {resultGraph.Sum(x => x.Weight)}");
            //Console.WriteLine(string.Join(Environment.NewLine, resultGraph));
        }

        private static void SetRoots(Edge edge)
        {
            int rootsToChange = Roots[edge.EndNode];
            int root = Roots[edge.StartNode];

            for (int i = 0; i < Roots.Length; i++)
            {
                if (Roots[i] == rootsToChange)
                {
                    Roots[i] = root;
                }
            }
        }

        private static void ReadGraph()
        {
            Nodes = int.Parse(Console.ReadLine().Split()[1]);
            Edges = int.Parse(Console.ReadLine().Split()[1]);
            Graph = new List<Edge>();
            Roots = new int[Nodes];

            for (int i = 0; i < Nodes; i++)
            {
                Roots[i] = i;
            }

            for (int i = 0; i < Edges; i++)
            {
                var input = Console.ReadLine();
                var edge = new Edge(input);

                Graph.Add(edge);
            }

            Graph = Graph
                .OrderBy(x => x.Weight)
                .ToList();
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
