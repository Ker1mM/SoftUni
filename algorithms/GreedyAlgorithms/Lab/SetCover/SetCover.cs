﻿using System;
using System.Collections.Generic;
using System.Linq;

public class SetCover
{
    public static void Main(string[] args)
    {
        var universe = new[] { 1, 3, 5, 7, 9, 11, 20, 30, 40 };
        var sets = new[]
        {
                new[] { 20 },
                new[] { 1, 5, 20, 30 },
                new[] { 3, 7, 20, 30, 40 },
                new[] { 9, 30 },
                new[] { 11, 20, 30, 40 },
                new[] { 3, 7, 40 }
            };

        var selectedSets = ChooseSets(sets.ToList(), universe.ToList());
        Console.WriteLine($"Sets to take ({selectedSets.Count}):");
        foreach (var set in selectedSets)
        {
            Console.WriteLine($"{{ {string.Join(", ", set)} }}");
        }
    }

    public static List<int[]> ChooseSets(IList<int[]> sets, IList<int> universe)
    {
        var selectedSets = new List<int[]>();

        while (universe.Count > 0)
        {
            int maxSet = FindLargestUniqueSet(sets, universe);

            selectedSets.Add(sets[maxSet]);
            RemoveSetElementsFromUniverse(sets[maxSet], universe);
            sets.RemoveAt(maxSet);
        }

        return selectedSets;
    }

    private static void RemoveSetElementsFromUniverse(int[] set, IList<int> universe)
    {
        foreach (var element in set)
        {
            universe.Remove(element);
        }
    }

    private static int FindLargestUniqueSet(IList<int[]> sets, IList<int> universe)
    {

        int maxElemetsFromUniverseCount = 0;
        int maxSetId = -1;

        for (int i = 0; i < sets.Count; i++)
        {

            int elemetsFromUniverseCount = 0;
            foreach (var element in sets[i].Distinct())
            {
                if (universe.Contains(element))
                {
                    elemetsFromUniverseCount++;
                }
            }

            if (elemetsFromUniverseCount > maxElemetsFromUniverseCount)
            {
                maxElemetsFromUniverseCount = elemetsFromUniverseCount;
                maxSetId = i;
            }
        }

        return maxSetId;
    }
}
