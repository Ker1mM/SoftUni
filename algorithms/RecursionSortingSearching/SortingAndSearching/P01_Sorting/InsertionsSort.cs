using System.Collections.Generic;
using System.Linq;

namespace P01_Sorting
{
    public static class InsertionsSort
    {
        public static int[] Sort(ICollection<int> array)
        {
            var result = array.ToArray();

            for (int i = 1; i < array.Count; i++)
            {
                Swap(result, i);
            }

            return result;
        }

        private static void Swap(int[] array, int index)
        {
            if (index > 0)
            {
                if (array[index - 1] > array[index])
                {
                    int temp = array[index];
                    array[index] = array[index - 1];
                    array[index - 1] = temp;
                    Swap(array, index - 1);
                }

            }
        }
    }
}
