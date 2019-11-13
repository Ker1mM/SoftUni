using System;
using System.Collections.Generic;
using System.Linq;

namespace CableNetwork
{
    class Program
    {

        private static long Budget;
        private static int Nodes;
        private static int Edges;
        private static List<Edge> ConnectedClients;
        private static List<Edge> NotConnectedClients;

        static void Main(string[] args)
        {
            ReadClients();

            long currentCost = 0;
            while (NotConnectedClients.Count > 0)
            {
                bool newClientFound = false;
                for (int i = 0; i < NotConnectedClients.Count; i++)
                {
                    var nextClient = NotConnectedClients[i];
                    if (HasOneConnectedNode(nextClient))
                    {
                        if (currentCost + nextClient.Cost <= Budget)
                        {
                            newClientFound = true;
                            nextClient.IsConnected = true;
                            NotConnectedClients.RemoveAt(i);
                            ConnectedClients.Add(nextClient);
                            currentCost += nextClient.Cost;
                        }
                        break;
                    }
                }

                if (!newClientFound)
                {
                    break;
                }
            }

            Console.WriteLine($"Budget used: {currentCost}");
        }

        private static bool HasOneConnectedNode(Edge edge)
        {
            bool startNodeConnected = ConnectedClients.Any(x => x.StartNode == edge.StartNode || x.EndNode == edge.StartNode);
            bool endNodeConnected = ConnectedClients.Any(x => x.StartNode == edge.EndNode || x.EndNode == edge.EndNode);

            return startNodeConnected ^ endNodeConnected;
        }


        private static void ReadClients()
        {
            Budget = long.Parse(Console.ReadLine().Split()[1]);
            Nodes = int.Parse(Console.ReadLine().Split()[1]);
            Edges = int.Parse(Console.ReadLine().Split()[1]);

            ConnectedClients = new List<Edge>();
            NotConnectedClients = new List<Edge>();

            for (int i = 0; i < Edges; i++)
            {
                var input = Console.ReadLine();
                var client = new Edge(input);

                if (client.IsConnected)
                {
                    ConnectedClients.Add(client);
                }
                else
                {
                    NotConnectedClients.Add(client);
                }
            }

            NotConnectedClients = NotConnectedClients
                .OrderBy(x => x.IsConnected)
                .ThenBy(x => x.Cost)
                .ToList();
        }
    }

    class Edge
    {
        public int StartNode { get; set; }
        public int EndNode { get; set; }
        public int Cost { get; set; }
        public bool IsConnected { get; set; }

        public Edge(string inputString)
        {
            this.ReadInput(inputString);
        }

        private void ReadInput(string input)
        {
            var inpurArgs = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int startNode = int.Parse(inpurArgs[0]);
            int endNode = int.Parse(inpurArgs[1]);
            int cost = int.Parse(inpurArgs[2]);

            this.StartNode = startNode;
            this.EndNode = endNode;
            this.Cost = cost;

            if (inpurArgs.Length == 4)
            {
                this.IsConnected = true;
            }
        }

        public override string ToString()
        {
            return $"[{this.StartNode} {this.EndNode}] -> {this.Cost}";
        }
    }
}
