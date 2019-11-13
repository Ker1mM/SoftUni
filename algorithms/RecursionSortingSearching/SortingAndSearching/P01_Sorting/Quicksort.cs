using System.Collections.Generic;
using System.Linq;

namespace P01_Sorting
{
    public static class Quicksort
    {
        public static int[] Sort(ICollection<int> array)
        {
            var result = array.ToArray();

            Partition(result, 0, result.Length - 1);

            return result;
        }

        private static void Partition(int[] array, int start, int end)
        {
            if (start < end)
            {

                int pivot = (start + end) / 2;
                int pivotElement = array[pivot];
                int initialPivot = pivot;

                for (int i = start; i < pivot; i++)
                {
                    if (array[i] > pivotElement)
                    {
                        array[pivot] = array[i];
                        array[i] = array[pivot - 1];
                        array[pivot - 1] = pivotElement;
                        i--;
                        pivot--;
                    }
                }

                for (int i = initialPivot + 1; i <= end; i++)
                {
                    if (array[i] < pivotElement)
                    {
                        array[pivot] = array[i];
                        array[i] = array[pivot + 1];
                        array[pivot + 1] = pivotElement;
                        pivot++;
                    }
                }
                Partition(array, start, pivot - 1);
                Partition(array, pivot + 1, end);
            }
        }
    }
}
