using System.Collections.Generic;
using System.Linq;

namespace P01_Sorting
{
    public static class MergeSort
    {
        public static int[] Sort(ICollection<int> array)
        {
            int[] result = array.ToArray();

            MergeSortFunc(result, 0, result.Length - 1);

            return result;
        }

        private static void MergeSortFunc(int[] array, int start, int end)
        {
            if (end > start)
            {
                int middle = (start + end) / 2;

                MergeSortFunc(array, start, middle);
                MergeSortFunc(array, middle + 1, end);

                Merge(array, start, middle, end);
            }
        }

        private static void Merge(int[] array, int start, int middle, int end)
        {
            int arrayOneIndex = start;
            int arrayTwoIndex = middle + 1;
            List<int> result = new List<int>();
            while (arrayOneIndex <= middle && arrayTwoIndex <= end)
            {
                if (array[arrayOneIndex] > array[arrayTwoIndex])
                {
                    result.Add(array[arrayTwoIndex]);
                    arrayTwoIndex++;
                }
                else
                {
                    result.Add(array[arrayOneIndex]);
                    arrayOneIndex++;
                }
            }

            if (arrayOneIndex > middle)
            {
                for (int i = arrayTwoIndex; i <= end; i++)
                {
                    result.Add(array[i]);
                }
            }
            else if (arrayTwoIndex > middle)
            {
                for (int i = arrayOneIndex; i <= middle; i++)
                {
                    result.Add(array[i]);
                }
            }

            var merged = result.ToArray();
            int counter = 0;
            for (int i = start; i <= end; i++)
            {
                array[i] = merged[counter];
                counter++;
            }
        }
    }
}
