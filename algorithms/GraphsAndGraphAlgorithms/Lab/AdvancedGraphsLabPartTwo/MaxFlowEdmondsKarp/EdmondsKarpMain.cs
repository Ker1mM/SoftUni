using System;

public class EdmondsKarpMain
{
    static void Main(string[] args)
    {
        var graph = new int[][]
        {
                new int[] { 0, 1, 1, 1, 0, 0, 0, 0},
                new int[] { 0, 0, 0, 0, 1, 0, 1, 0},
                new int[] { 0, 0, 0, 0, 0, 1, 1, 0},
                new int[] { 0, 0, 0, 0, 1, 1, 0, 0},
                new int[] { 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] { 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] { 0, 0, 0, 0, 0, 0, 0, 1},
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0},
        };

        var maxFlow = EdmondsKarp.FindMaxFlow(graph);
        Console.WriteLine($"Max flow = {maxFlow}");
    }
}
