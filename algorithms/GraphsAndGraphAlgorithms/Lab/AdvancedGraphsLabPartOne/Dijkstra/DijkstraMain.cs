//namespace Dijkstra
//{
using System;

public class DijkstraMain
{
    public static void Main()
    {
        var graph = new[,]
        {
                // 0   1   2   3   4  5  6
                { 0,  1,  0,  0,  0,  0, 2}, // 0
                { 1,  0,  1,  0,  0,  0, 0}, // 1
                { 0,  1,  0,  1,  0,  0, 0}, // 2
                { 0,  0,  1,  0,  1,  0, 0}, // 3
                { 0,  0,  0,  1,  0,  1, 0}, // 4
                { 0,  0,  0,  0,  1,  0, 1}, // 5
                { 2,  0,  0,  0,  0,  1, 0}, // 6
            };

        PrintPath(graph, 0, 5);
    }

    public static void PrintPath(int[,] graph, int sourceNode, int destinationNode)
    {
        Console.Write(
            "Shortest path [{0} -> {1}]: ",
            sourceNode,
            destinationNode);

        var path = DijkstraWithoutQueue.DijkstraAlgorithm(graph, sourceNode, destinationNode);

        if (path == null)
        {
            Console.WriteLine("no path");
        }
        else
        {
            int pathLength = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                pathLength += graph[path[i], path[i + 1]];
            }

            var formattedPath = string.Join("->", path);
            Console.WriteLine("{0} (length {1})", formattedPath, pathLength);
        }
    }
}
//}