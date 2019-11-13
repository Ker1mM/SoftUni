using System.Collections.Generic;
using System.Linq;

namespace P01_Sorting
{
    public static class ShellSort
    {

        public static int[] Sort(ICollection<int> array)
        {
            var result = array.ToArray();

            var gaps = FindGaps(array.Count);

            foreach (var gap in gaps)
            {
                var startIndex = 0;
                var tempArray = new List<int>();

                for (int i = startIndex; i < array.Count; i += gap)
                {
                    tempArray.Add(result[i]);
                }

                tempArray = InsertionsSort.Sort(tempArray).ToList();

                int index = 0;
                for (int i = startIndex; i < array.Count; i += gap)
                {
                    result[i] = tempArray[index];
                    index++;
                }

                startIndex++;
            }

            return result;
        }


        private static int[] FindGaps(int arraySize)
        {
            var result = new List<int>();
            int interval = 1;
            while (interval <= arraySize / 2)
            {
                result.Add(interval);

                interval = (interval * 3) + 1;
            }

            result.Reverse();

            return result.ToArray();
        }
    }
}
