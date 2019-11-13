using System;
using System.Collections.Generic;
using System.Linq;

namespace P03_InversionCount
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int result = MergeSort.InversionCount(input);
            Console.WriteLine(result);
        }
    }

    public static class MergeSort
    {
        static int inversions = 0;

        public static int InversionCount(int[] array)
        {
            int[] temp = new int[array.Length];
            MergeSortFunc(array.ToList(), 0, array.Length - 1, temp);
            return inversions;
        }

        private static void MergeSortFunc(List<int> array, int start, int end, int[] temp)
        {
            if (end > start)
            {
                int middle = (start + end) / 2;

                MergeSortFunc(array, start, middle, temp);
                MergeSortFunc(array, middle + 1, end, temp);

                Merge(array, start, middle, end, temp);
            }
        }

        private static void Merge(List<int> array, int start, int middle, int end, int[] result)
        {
            int arrayOneIndex = start;
            int arrayTwoIndex = middle + 1;

            int k = 0;

            while (arrayOneIndex <= middle && arrayTwoIndex <= end)
            {
                if (array[arrayOneIndex] <= array[arrayTwoIndex])
                {
                    result[k++] = (array[arrayOneIndex++]);
                }
                else
                {
                    result[k++] = (array[arrayTwoIndex++]);
                    inversions += (middle + 1) - arrayOneIndex;
                }
            }

            while (arrayOneIndex <= middle)
            {
                result[k++] = (array[arrayOneIndex++]);
            }

            while (arrayTwoIndex <= end)
            {
                result[k++] = (array[arrayTwoIndex++]);
            }

            int counter = 0;
            for (int i = start; i <= end; i++)
            {
                array[i] = result[counter++];
            }
        }
    }
}
