using System;
using System.Collections.Generic;
using System.Linq;

public class StronglyConnectedComponents
{
    private static List<int>[] graph;
    private static List<List<int>> stronglyConnectedComponents;
    private static bool[] visited;

    public static List<List<int>> FindStronglyConnectedComponents(List<int>[] targetGraph)
    {
        graph = targetGraph;
        visited = new bool[graph.Length];

        var orderedNodes = new Stack<int>();
        for (int i = 0; i < graph.Length; i++)
        {
            DFS(i, orderedNodes, graph);
        }

        var reversedGraph = GetReversedGraph();

        visited = new bool[graph.Length];
        stronglyConnectedComponents = new List<List<int>>();
        while (orderedNodes.Count > 0)
        {
            var nextNode = orderedNodes.Pop();
            if (!visited[nextNode])
            {
                var connectedComponent = new Stack<int>();
                DFS(nextNode, connectedComponent, reversedGraph);
                stronglyConnectedComponents.Add(connectedComponent.ToList());
            }
        }

        return stronglyConnectedComponents;
    }

    private static List<int>[] GetReversedGraph()
    {
        var result = new List<int>[graph.Length];

        for (int i = 0; i < graph.Length; i++)
        {
            result[i] = new List<int>();
        }

        for (int i = 0; i < graph.Length; i++)
        {
            foreach (var node in graph[i])
            {
                result[node].Add(i);
            }
        }

        return result;
    }

    private static void DFS(int node, Stack<int> orderedNodes, List<int>[] currentGraph)
    {
        if (visited[node])
        {
            return;
        }

        visited[node] = true;
        foreach (var child in currentGraph[node])
        {
            DFS(child, orderedNodes, currentGraph);
        }
        orderedNodes.Push(node);
    }
}
