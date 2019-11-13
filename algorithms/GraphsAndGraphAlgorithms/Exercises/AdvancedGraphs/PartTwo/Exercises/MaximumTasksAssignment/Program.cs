using System;
using System.Collections.Generic;
using System.Linq;

namespace MaximumTasksAssignment
{
    class Program
    {
        private static int[][] graph;
        private static int graphLength;

        static void Main(string[] args)
        {
            var persons = int.Parse(Console.ReadLine().Split()[1]);
            var tasks = int.Parse(Console.ReadLine().Split()[1]);
            graphLength = persons + tasks + 2;

            ReadGraph(tasks, persons);

            FindMaxFlow();

            var result = FindAssignments();
            PrintResult(result, tasks);

        }

        private static void PrintResult(List<int[]> result, int tasks)
        {
            foreach (var couple in result.OrderBy(x => x[0]))
            {
                char person = (char)('A' - 1 + couple[0]);
                int task = couple[1] - tasks;
                Console.WriteLine($"{person}-{task}");
            }
        }

        private static List<int[]> FindAssignments()
        {
            var result = new List<int[]>();

            int start = graphLength - 1;
            int end = 0;
            var parents = new int[graphLength];

            while (BFS(start, end, parents))
            {
                var road = new List<int>();
                var child = end;
                while (child != start)
                {
                    var parent = parents[child];

                    road.Add(parent);
                    graph[parent][child] = 0;
                    graph[child][parent] = 1;

                    child = parent;
                }

                result.Add(road.Take(2).ToArray());
            }

            return result;
        }

        private static void FindMaxFlow()
        {
            int start = 0;
            int end = graphLength - 1;
            var parents = new int[graphLength];


            while (BFS(start, end, parents))
            {
                var child = end;

                while (child != start)
                {
                    var parent = parents[child];

                    graph[parent][child] = 0;
                    graph[child][parent] = 1;

                    child = parent;
                }
            }
        }

        private static bool BFS(int start, int end, int[] parents)
        {
            var queue = new Queue<int>();
            queue.Enqueue(start);
            var visited = new bool[graphLength];

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                visited[node] = true;

                for (int i = graphLength - 1; i >= 0; i--)
                {
                    if (graph[node][i] > 0 && !visited[i])
                    {
                        visited[i] = true;
                        parents[i] = node;
                        queue.Enqueue(i);

                        if (i == end)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        private static void ReadGraph(int tasks, int persons)
        {

            graph = new int[graphLength][];

            for (int i = 1; i <= tasks; i++)
            {
                graph[i] = new int[graphLength];

                var input = Console.ReadLine().ToCharArray().ToList();
                var indefOfY = input.IndexOf('Y');

                while (indefOfY != -1)
                {
                    input[indefOfY] = 'N';
                    graph[i][indefOfY + tasks + 1] = 1;
                    indefOfY = input.IndexOf('Y');
                }
            }

            graph[0] = new int[graphLength];
            for (int i = 1; i <= tasks; i++)
            {
                graph[0][i] = 1;
            }

            graph[graphLength - 1] = new int[graphLength];
            for (int i = tasks + 1; i < graphLength - 1; i++)
            {
                graph[i] = new int[graphLength];
                graph[i][graphLength - 1] = 1;
            }
        }

    }
}
