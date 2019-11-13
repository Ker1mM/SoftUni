using System;
using System.Collections.Generic;
using System.Linq;

namespace Sticks
{
    class Program
    {
        static List<int>[] graph;
        static List<int>[] connections;
        static bool[] removed;
        static void Main(string[] args)
        {
            int stickCount = int.Parse(Console.ReadLine());
            int edgeCount = int.Parse(Console.ReadLine());

            graph = new List<int>[stickCount];
            connections = new List<int>[stickCount];
            for (int i = 0; i < stickCount; i++)
            {
                graph[i] = new List<int>();
                connections[i] = new List<int>();
            }

            for (int i = 0; i < edgeCount; i++)
            {
                var nodes = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var from = nodes[1];
                var to = nodes[0];

                connections[to].Add(from);
                graph[from].Add(to);
            }

            removed = new bool[stickCount];

            var removedOrder = new List<int>();

            for (int node = stickCount - 1; node >= 0; node--)
            {
                if (!removed[node])
                {
                    var result = DFS(node);

                    if (result)
                    {
                        removedOrder.Add(node);
                        node = stickCount;
                    }
                }
            }

            if (removedOrder.Count != stickCount)
            {
                Console.WriteLine("Cannot lift all sticks");
            }
            Console.WriteLine(string.Join(" ", removedOrder));
        }

        private static bool DFS(int index)
        {
            foreach (var node in graph[index])
            {
                if (!removed[node])
                {
                    return false;
                }
            }

            removed[index] = true;
            return true;
        }
    }

    class Stick : IComparable<Stick>
    {
        public List<int> SticksOnTop { get; set; }
        public List<int> SticksOnBottom { get; set; }

        public int Number { get; set; }

        public Stick(int number)
        {
            this.Number = number;
            this.SticksOnBottom = new List<int>();
            this.SticksOnTop = new List<int>();
        }

        public int CompareTo(Stick other)
        {
            return -(this.Number.CompareTo(other.Number));
        }
    }
}
