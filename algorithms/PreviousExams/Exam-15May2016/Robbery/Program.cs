using System;
using System.Collections.Generic;
using System.Linq;

namespace Robbery
{
    public class Node : IComparable<Node>
    {
        // set default value for the distance equal to positive infinity
        public Node(int id, double distance = double.PositiveInfinity)
        {
            this.Id = id;
            this.DistanceFromStart = distance;
        }

        public int Id { get; set; }

        public double DistanceFromStart { get; set; }

        public int CompareTo(Node other)
        {
            return this.DistanceFromStart.CompareTo(other.DistanceFromStart);
        }
    }

    class Program
    {
        static Dictionary<Node, Dictionary<Node, int>> graph;
        static Dictionary<int, Node> nodes;
        static void Main(string[] args)
        {
            var cameraStatus = GetCameraPositions();
            int nodeCount = cameraStatus.Length;
            int startingEnergy = int.Parse(Console.ReadLine());
            int waitingCost = int.Parse(Console.ReadLine());

            int start = int.Parse(Console.ReadLine());
            int end = int.Parse(Console.ReadLine());
            var edgeCount = int.Parse(Console.ReadLine());


            for (int i = 0; i < edgeCount; i++)
            {
                var inpurArgs = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int from = inpurArgs[0];
                int to = inpurArgs[1];
                int cost = inpurArgs[2];

                if (!graph[nodes[from]].ContainsKey(nodes[to]))
                {

                    graph[nodes[from]].Add(nodes[to], cost);
                }
            }

            ;
            DijkstraWithPriorityQueue.DijkstraAlgorithm(graph, nodes[start], nodes[end], waitingCost, cameraStatus);
            ;
            var finalEnergy = startingEnergy - nodes[end].DistanceFromStart;


            if (finalEnergy < 0)
            {
                Console.WriteLine($"Busted - need {finalEnergy * -1} more energy");
            }
            else
            {
                Console.WriteLine(finalEnergy);
            }

        }

        private static int[] GetCameraPositions()
        {
            var input = Console.ReadLine().Split().ToArray();
            var result = new int[input.Length];
            nodes = new Dictionary<int, Node>();
            graph = new Dictionary<Node, Dictionary<Node, int>>();


            for (int i = 0; i < input.Length; i++)
            {
                var temp = new Node(i);
                nodes.Add(i, temp);
                graph.Add(temp, new Dictionary<Node, int>());
                var node = input[i];
                var status = node[node.Length - 1];

                if (status == 'b')
                {
                    result[i] = 1;
                }
                else
                {
                    result[i] = 0;
                }
            }

            return result;
        }
    }

    public static class DijkstraWithPriorityQueue
    {
        static int[] CameraStatus { get; set; }
        static int[] roundCounter;
        static int WaitingCost;

        public static void DijkstraAlgorithm(Dictionary<Node, Dictionary<Node, int>> graph, Node sourceNode, Node destinationNode, int waitingCost, int[] cameraPositions)
        {
            CameraStatus = cameraPositions;
            roundCounter = new int[cameraPositions.Length];
            WaitingCost = waitingCost;
            int?[] previous = new int?[cameraPositions.Length];
            bool[] visited = new bool[cameraPositions.Length];

            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();

            foreach (var pair in graph)
            {
                pair.Key.DistanceFromStart = double.PositiveInfinity;
            }

            sourceNode.DistanceFromStart = 0;
            priorityQueue.Enqueue(sourceNode);

            while (priorityQueue.Count > 0)
            {
                var currentNode = priorityQueue.ExtractMin();

                if (currentNode == destinationNode)
                {
                    break;
                }

                foreach (var edge in graph[currentNode])
                {
                    if (!visited[edge.Key.Id])
                    {
                        priorityQueue.Enqueue(edge.Key);
                        visited[edge.Key.Id] = true;
                    }

                    int currentWaitingCost = WaitingCost * ((CameraStatus[edge.Key.Id] + roundCounter[currentNode.Id]) % 2);

                    int cost = 0;
                    if (currentWaitingCost > 0)
                    {
                        cost = 1;
                    }

                    double distance = currentNode.DistanceFromStart + edge.Value + currentWaitingCost;

                    if (distance < edge.Key.DistanceFromStart)
                    {
                        edge.Key.DistanceFromStart = distance;
                        previous[edge.Key.Id] = currentNode.Id;
                        priorityQueue.DecreaseKey(edge.Key);

                        roundCounter[edge.Key.Id] = roundCounter[currentNode.Id] + 1 + cost;
                    }
                }
            }

            if (double.IsInfinity(destinationNode.DistanceFromStart))
            {
                return;
            }
        }
    }

    public class PriorityQueue<T> where T : IComparable<T>
    {
        private Dictionary<T, int> searchCollection;
        private List<T> heap;

        public PriorityQueue()
        {
            this.heap = new List<T>();
            this.searchCollection = new Dictionary<T, int>();
        }

        public int Count
        {
            get
            {
                return this.heap.Count;
            }
        }

        public T ExtractMin()
        {
            var min = this.heap[0];
            var last = this.heap[this.heap.Count - 1];
            this.searchCollection[last] = 0;
            this.heap[0] = last;
            this.heap.RemoveAt(this.heap.Count - 1);
            if (this.heap.Count > 0)
            {
                this.HeapifyDown(0);
            }

            this.searchCollection.Remove(min);

            return min;
        }

        public T PeekMin()
        {
            return this.heap[0];
        }

        public void Enqueue(T element)
        {
            this.searchCollection.Add(element, this.heap.Count);
            this.heap.Add(element);
            this.HeapifyUp(this.heap.Count - 1);
        }

        private void HeapifyDown(int i)
        {
            var left = (2 * i) + 1;
            var right = (2 * i) + 2;
            var smallest = i;

            if (left < this.heap.Count && this.heap[left].CompareTo(this.heap[smallest]) < 0)
            {
                smallest = left;
            }

            if (right < this.heap.Count && this.heap[right].CompareTo(this.heap[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != i)
            {
                T old = this.heap[i];
                this.searchCollection[old] = smallest;
                this.searchCollection[this.heap[smallest]] = i;
                this.heap[i] = this.heap[smallest];
                this.heap[smallest] = old;
                this.HeapifyDown(smallest);
            }
        }

        private void HeapifyUp(int i)
        {
            var parent = (i - 1) / 2;
            while (i > 0 && this.heap[i].CompareTo(this.heap[parent]) < 0)
            {
                T old = this.heap[i];
                this.searchCollection[old] = parent;
                this.searchCollection[this.heap[parent]] = i;
                this.heap[i] = this.heap[parent];
                this.heap[parent] = old;

                i = parent;
                parent = (i - 1) / 2;
            }
        }

        public void DecreaseKey(T element)
        {
            int index = this.searchCollection[element];
            this.HeapifyUp(index);
        }
    }
}
