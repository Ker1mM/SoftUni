using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShortestPathInMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            ShortestPath.Print();
        }
    }

    class Edge
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Value { get; set; }

        public Edge(int start, int end, int value)
        {
            this.Start = start;
            this.End = end;
            this.Value = value;
        }

        public override string ToString()
        {
            return $"({this.Start}:{this.End}) -> {this.Value}";
        }
    }

    static class ShortestPath
    {
        private static int rowCount;
        private static int colCount;
        private static int[,] matrix;
        private static List<Edge> graph;
        private static List<Edge> shortestPath;
        private static bool[] visited;
        private static int[] distance;


        private static void ReadMatrix()
        {
            rowCount = int.Parse(Console.ReadLine());
            colCount = int.Parse(Console.ReadLine());

            matrix = new int[rowCount, colCount];
            graph = new List<Edge>();
            shortestPath = new List<Edge>();
            visited = new bool[rowCount * colCount];
            distance = new int[rowCount * colCount];

            for (int i = 0; i < rowCount * colCount; i++)
            {
                distance[i] = int.MaxValue;
            }

            for (int row = 0; row < rowCount; row++)
            {
                var inputArgs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                for (int col = 0; col < colCount; col++)
                {
                    matrix[row, col] = inputArgs[col];
                }
            }
        }

        private static void MatrixToGraph()
        {
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount - 1; col++)
                {
                    int start = colCount * row + col;
                    int end = start + 1;

                    var leftToRight = new Edge(start, end, matrix[row, col + 1]);
                    var rightToLeft = new Edge(end, start, matrix[row, col]);

                    graph.Add(leftToRight);
                    graph.Add(rightToLeft);
                }
            }

            for (int col = 0; col < colCount; col++)
            {
                for (int row = 0; row < rowCount - 1; row++)
                {
                    int start = colCount * row + col;
                    int end = start + colCount;

                    var topToBottom = new Edge(start, end, matrix[row + 1, col]);
                    var bottomToTop = new Edge(end, start, matrix[row, col]);

                    graph.Add(topToBottom);
                    graph.Add(bottomToTop);
                }
            }

            graph = graph
                .OrderBy(x => x.Value)
                .ThenBy(x => x.Start)
                .ThenBy(x => x.End)
                .ToList();
        }

        private static void FindPath()
        {
            distance[0] = 0;

            while (true)
            {
                int minDistance = int.MaxValue;
                int minNode = 0;
                for (int i = 0; i < distance.Length; i++)
                {
                    if (!visited[i] && distance[i] < minDistance)
                    {
                        minNode = i;
                        minDistance = distance[i];
                    }
                }

                if (minDistance == int.MaxValue)
                {
                    break;
                }

                visited[minNode] = true;

                var edges = graph.Where(x => x.Start == minNode && !visited[x.End]).ToList();
                foreach (var edge in edges)
                {
                    var newDistance = minDistance + edge.Value;
                    if (newDistance < distance[edge.End])
                    {
                        distance[edge.End] = newDistance;
                        shortestPath.Add(edge);
                    }
                }
            }
        }

        public static void Print()
        {
            ReadMatrix();
            MatrixToGraph();
            FindPath();
            ;

            var result = GetPath();
            Console.WriteLine($"Length: {result.Sum()}");
            Console.WriteLine($"Path: {string.Join(" ", result)}");
        }

        public static List<int> GetPath()
        {
            int child = rowCount * colCount - 1;
            var path = new List<int>();

            while (child != 0)
            {
                var edge = shortestPath.FirstOrDefault(x => x.End == child);
                path.Add(edge.Value);
                child = edge.Start;
            }

            path.Add(matrix[0, 0]);
            path.Reverse();

            return path;
        }
    }
}
