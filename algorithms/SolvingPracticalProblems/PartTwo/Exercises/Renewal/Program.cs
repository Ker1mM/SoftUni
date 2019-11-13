using System;
using System.Collections.Generic;
using System.Linq;

namespace Renewal
{
    class Program
    {
        static List<int>[] roads;
        static int[][] buildingCosts;
        static int[][] destroyingCosts;
        static int cityCount;
        static int totalCost;

        static void Main(string[] args)
        {
            cityCount = int.Parse(Console.ReadLine());
            roads = new List<int>[cityCount];

            for (int i = 0; i < cityCount; i++)
            {
                var edges = Console.ReadLine().ToCharArray();
                roads[i] = new List<int>();
                for (int j = 0; j < edges.Length; j++)
                {
                    if (edges[j] == '1')
                    {
                        roads[i].Add(j);
                    }
                }
            }

            buildingCosts = GetCosts();
            destroyingCosts = GetCosts();
            ;
            var comp = StronglyConnectedComponents.FindStronglyConnectedComponents(roads);
            var notDestroyedRoads = new int[cityCount, cityCount];
            foreach (var city in comp)
            {
                ConnectedCities(city, notDestroyedRoads);
            }

            totalCost = GetDestryingTotalCost(notDestroyedRoads);
            ConnectCities(comp);
            Console.WriteLine(totalCost);
            ;

        }

        private static void ConnectCities(List<List<int>> comps)
        {
            List<int> connectedCities = comps[0];
            comps.RemoveAt(0);

            while (comps.Count > 0)
            {
                var connection = BestConnection(connectedCities);
                totalCost += buildingCosts[connection[0]][connection[1]];

                for (int i = 0; i < comps.Count; i++)
                {
                    if (comps[i].Contains(connection[1]))
                    {
                        connectedCities.AddRange(comps[i]);
                        comps.RemoveAt(i);
                        break;
                    }
                }

            }
        }

        private static int[] BestConnection(List<int> addedCities)
        {
            int minCost = int.MaxValue;
            int minStartNode = -1;
            int minEndNode = -1;

            for (int i = 0; i < addedCities.Count; i++)
            {
                for (int j = 0; j < cityCount; j++)
                {
                    if (!addedCities.Contains(j) && buildingCosts[addedCities[i]][j] < minCost)
                    {
                        minCost = buildingCosts[addedCities[i]][j];
                        minStartNode = addedCities[i];
                        minEndNode = j;
                    }
                }
            }

            return new int[] { minStartNode, minEndNode };
        }

        private static int GetDestryingTotalCost(int[,] notDestroyedRoads)
        {
            int totalCosts = 0;
            for (int i = 0; i < cityCount; i++)
            {
                foreach (var road in roads[i])
                {
                    if (notDestroyedRoads[i, road] == 0)
                    {
                        totalCosts += destroyingCosts[i][road];
                        notDestroyedRoads[road, i] = -1;
                    }
                }
            }

            return totalCosts;
        }

        private static void ConnectedCities(List<int> cities, int[,] notDestroyedRoads)
        {
            var addedCities = new List<int>();
            addedCities.Add(cities[0]);

            var nextNode = BestPath(addedCities, cities);
            while (nextNode[0] != -1)
            {
                notDestroyedRoads[nextNode[0], nextNode[1]] = 1;
                notDestroyedRoads[nextNode[1], nextNode[0]] = 1;
                addedCities.Add(nextNode[1]);
                nextNode = BestPath(addedCities, cities);
            }
        }

        private static int[] BestPath(List<int> added, List<int> cities)
        {
            int maxValue = -1;
            int maxStartNode = -1;
            int maxEndNode = -1;
            foreach (var city in added)
            {
                foreach (var i in cities)
                {
                    if (!added.Contains(i) && roads[city].Contains(i))
                    {
                        if (destroyingCosts[city][i] > maxValue)
                        {
                            maxEndNode = i;
                            maxStartNode = city;
                            maxValue = destroyingCosts[city][i];
                        }
                    }
                }
            }

            return new int[] { maxStartNode, maxEndNode };
        }

        private static int[][] GetCosts()
        {
            int[][] costs = new int[cityCount][];


            for (int i = 0; i < cityCount; i++)
            {
                costs[i] = new int[cityCount];
                var inputArgs = Console.ReadLine()
                    .ToCharArray();

                for (int j = 0; j < cityCount; j++)
                {
                    int capitalLetterConst = 0;
                    if (char.IsLower(inputArgs[j]))
                    {
                        capitalLetterConst = -6;
                    }

                    costs[i][j] = (inputArgs[j] - 'A') + capitalLetterConst;
                }
            }

            return costs;
        }
    }

    class StronglyConnectedComponents
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
}
