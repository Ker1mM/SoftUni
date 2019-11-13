using System;
using System.Collections.Generic;
using System.Linq;

public class GraphConnectedComponents
{
    static List<int>[] graph = new List<int>[]
    {
        new List<int>() { 3, 6 },
        new List<int>() { 3, 4, 5, 6 },
        new List<int>() { 8 },
        new List<int>() { 0, 1, 5 },
        new List<int>() { 1, 6 },
        new List<int>() { 1, 3 },
        new List<int>() { 0, 1, 4 },
        new List<int>() { },
        new List<int>() { 2 }
    };

    public static void Main()
    {
        graph = ReadGraph();
        FindGraphConnectedComponents();
    }

    private static List<int>[] ReadGraph()
    {
        int n = int.Parse(Console.ReadLine());
        var graph = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            graph[i] = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();
        }
        return graph;
    }

    private static void FindGraphConnectedComponents()
    {
        bool[] visited = new bool[graph.Count()];
        var result = new List<int>();


        for (int i = 0; i < graph.Count(); i++)
        {
            if (!visited[i])
            {
                Console.Write("Connected component:");
                DFS(i, visited);
                Console.WriteLine();
            }
        }
    }

    private static void DFS(int node, bool[] visited)
    {
        if (!visited[node])
        {
            visited[node] = true;
            foreach (var childNode in graph[node])
            {
                DFS(childNode, visited);
            }

            Console.Write($" {node}");
        }
    }
}
