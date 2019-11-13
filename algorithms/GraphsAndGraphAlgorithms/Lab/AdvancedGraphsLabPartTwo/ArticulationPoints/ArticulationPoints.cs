using System;
using System.Collections.Generic;
using System.Linq;

public class ArticulationPoints
{
    private static List<int>[] graph;
    private static int?[] parents;
    private static int[] visitedTime;
    private static int[] lowPoint;
    private static bool[] visited;
    private static List<int> articulationPoints;
    private static int time;
    private static int root;

    public static List<int> FindArticulationPoints(List<int>[] targetGraph)
    {
        graph = targetGraph;
        visitedTime = new int[targetGraph.Length];
        lowPoint = new int[targetGraph.Length];

        parents = new int?[targetGraph.Length];
        visited = new bool[targetGraph.Length];

        articulationPoints = new List<int>();
        time = 0;
        ;

        for (int i = 0; i < graph.Length; i++)
        {
            if (!visited[i])
            {
                root = i;
                DFS(i);
                if (parents.Where(x => x == i).Count() > 1)
                {
                    articulationPoints.Add(i);
                }
            }
        }

        return articulationPoints.Distinct().ToList();
    }

    private static void DFS(int node)
    {
        if (visited[node])
        {
            return;
        }

        visited[node] = true;
        visitedTime[node] = time;
        lowPoint[node] = time;
        time++;

        foreach (var child in graph[node])
        {
            if (parents[node] == child)
            {
                continue;
            }

            if (!visited[child])
            {
                parents[child] = node;
                DFS(child);

                if (visitedTime[node] <= lowPoint[child] && node != root)
                {
                    articulationPoints.Add(node);
                }

                lowPoint[node] = Math.Min(lowPoint[node], lowPoint[child]);
            }
            else
            {

                lowPoint[node] = Math.Min(lowPoint[node], visitedTime[child]);
            }

        }


    }
}
