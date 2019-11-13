using System;
using System.Collections.Generic;
using System.Linq;

namespace Greedy
{
    class Program
    {
        private static List<string> combinations;
        private static List<char> skirts;
        private static int shirtCount;

        private static Dictionary<char, int> GetSkirtCount(List<char> skirts)
        {
            var res = new Dictionary<char, int>();

            for (int i = 0; i < skirts.Count; i++)
            {
                if (!res.ContainsKey(skirts[i]))
                {
                    res.Add(skirts[i], 0);
                }

                res[skirts[i]]++;
            }

            return res;
        }

        private static void GetCombinations(string[] vector, int index, int skirtIndex, int shirtNumber, Dictionary<char, int> skirtsCount)
        {
            if (index >= vector.Length)
            {
                combinations.Add(string.Join("-", vector));
                return;
            }

            for (int i = shirtNumber; i < shirtCount; i++)
            {
                for (int j = 0; j < skirtsCount.Count; j++)
                {
                    if (skirtsCount[skirts[j]] > 0)
                    {
                        string resString = i.ToString() + skirts[j];

                        vector[index] = resString;

                        skirtsCount[skirts[j]]--;

                        int nextSkirIndex = j;
                        if (skirtsCount[skirts[j]] == 0)
                        {
                            nextSkirIndex++;
                        }

                        GetCombinations(vector, index + 1, nextSkirIndex, i + 1, skirtsCount);

                        if (skirtsCount[skirts[j]] == 0)
                        {
                            nextSkirIndex--;
                        }
                        skirtsCount[skirts[j]]++;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            combinations = new List<string>();
            shirtCount = int.Parse(Console.ReadLine());
            skirts = Console.ReadLine().ToCharArray().OrderBy(x => x).ToList();
            var girlCount = int.Parse(Console.ReadLine());
            var rr = GetSkirtCount(skirts);
            skirts = skirts.Distinct().ToList();

            string[] vector = new string[girlCount];

            GetCombinations(vector, 0, 0, 0, rr);

            Console.WriteLine(combinations.Count);
            Console.WriteLine(string.Join(Environment.NewLine, combinations));
        }
    }
}
