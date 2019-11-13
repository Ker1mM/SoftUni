using System;
using System.Collections.Generic;

namespace P06.XRemoval
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                lines.Add(input);
            }

            HashSet<int[]> indexes = new HashSet<int[]>();

            for (int i = 0; i < lines.Count - 2; i++)
            {
                for (int j = 0; j < lines[i].Length - 2; j++)
                {
                    string l1 = lines[i].ToLower();
                    string l2 = lines[i + 1].ToLower();
                    string l3 = lines[i + 2].ToLower();

                    if (l1[j] == l1[j + 2])
                    {
                        if (l2.Length > j + 1 && l2[j + 1] == l1[j])
                        {
                            if (l3.Length > j + 2 && l1[j] == l3[j] && l1[j] == l3[j + 2])
                            {
                                indexes.Add(new int[] { i, j });
                                indexes.Add(new int[] { i, j + 2 });
                                indexes.Add(new int[] { i + 1, j + 1 });
                                indexes.Add(new int[] { i + 2, j + 0 });
                                indexes.Add(new int[] { i + 2, j + 2 });
                            }
                        }
                    }
                }
            }

            foreach (var item in indexes)
            {
                char[] temp = lines[item[0]].ToCharArray();
                temp[item[1]] = ' ';
                lines[item[0]] = new string(temp);
            }

            foreach (var item in lines)
            {
                Console.WriteLine(item.Replace(" ", ""));
            }
        }
    }
}
