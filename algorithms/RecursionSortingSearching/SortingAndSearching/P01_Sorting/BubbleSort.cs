using System.Collections.Generic;
using System.Linq;

namespace P01_Sorting
{
    public static class BubbleSort
    {
        public static int[] Sort(ICollection<int> array)
        {
            var result = array.ToArray();

            Compare(result);

            return result;
        }

        private static void Compare(int[] array)
        {
            bool isOredered = false;

            while (!isOredered)
            {
                isOredered = true;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        isOredered = false;
                    }
                }
            }
        }
    }
}
