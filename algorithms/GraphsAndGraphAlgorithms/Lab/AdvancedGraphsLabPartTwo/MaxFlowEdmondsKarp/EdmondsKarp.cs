using System;
using System.Collections.Generic;

public class EdmondsKarp
{
    private static int[][] graph;
    private static int[] parents;
    private static bool[] visited;
    private static int source;
    private static int sink;

    public static int FindMaxFlow(int[][] targetGraph)
    {
        graph = targetGraph;

        int maxFlow = 0;



        parents = new int[graph.GetLength(0)];
        visited = new bool[graph.GetLength(0)];

        source = 0;
        sink = graph.GetLength(0) - 1;


        var road = BFS(source, sink);
        while (road != null)
        {
            int flow = GetMinFlow(road);
            maxFlow += flow;
            AdjustFlowCapacity(flow, road);

            parents = new int[graph.GetLength(0)];
            visited = new bool[graph.GetLength(0)];
            road = BFS(source, sink);
        }

        return maxFlow;
    }

    private static void AdjustFlowCapacity(int flow, List<int> road)
    {
        for (int i = 0; i < road.Count - 1; i++)
        {
            graph[road[i]][road[i + 1]] -= flow;
            //graph[road[i + 1]][road[i]] += flow;
        }
    }

    private static int GetMinFlow(List<int> road)
    {
        int minFlow = int.MaxValue;
        for (int i = 0; i < road.Count - 1; i++)
        {
            int edgeFlow = graph[road[i]][road[i + 1]];
            if (edgeFlow < minFlow)
            {
                minFlow = edgeFlow;
            }
        }

        return minFlow;
    }

    private static List<int> BFS(int node, int sink)
    {
        var queue = new Queue<int>();

        List<int> result = null;

        queue.Enqueue(node);
        while (queue.Count > 0)
        {
            var currentNode = queue.Dequeue();
            visited[currentNode] = true;
            for (int i = 0; i < graph[node].Length; i++)
            {
                if (graph[currentNode][i] > 0 && !visited[i])
                {
                    queue.Enqueue(i);
                    parents[i] = currentNode;
                    if (i == sink)
                    {
                        result = GetRoad();
                        return result;
                    }
                }
            }
        }

        return result;
    }

    private static List<int> GetRoad()
    {
        var result = new List<int> { sink };
        var parent = parents[sink];

        while (parent != source)
        {
            result.Add(parent);
            parent = parents[parent];
        }

        result.Add(parent);
        result.Reverse();
        return result;
    }
}
