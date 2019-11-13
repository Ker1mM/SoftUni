using System;
using System.Collections.Generic;

namespace P01.PlusRemove
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

            HashSet<int[]> removeIndexes = new HashSet<int[]>();
            for (int i = 1; i < lines.Count - 1; i++)
            {
                for (int j = 0; j < lines[i].Length - 2; j++)
                {
                    string curLine = lines[i].ToLower();
                    if (curLine[j] == curLine[j + 1] && curLine[j + 1] == curLine[j + 2])
                    {
                        string topLine = lines[i - 1].ToLower();
                        string botLine = lines[i + 1].ToLower();
                        if (j + 1 < topLine.Length && j + 1 < botLine.Length)
                        {
                            if (topLine[j + 1] == curLine[j + 1] && (botLine[j + 1] == curLine[j + 1]))
                            {
                                removeIndexes.Add(new int[] { i - 1, j + 1 });
                                removeIndexes.Add(new int[] { i, j });
                                removeIndexes.Add(new int[] { i, j + 1 });
                                removeIndexes.Add(new int[] { i, j + 2 });
                                removeIndexes.Add(new int[] { i + 1, j + 1 });
                            }
                        }
                    }
                }
            }

            foreach (var index in removeIndexes)
            {
                int row = index[0];
                int col = index[1];
                char[] temp = lines[row].ToCharArray();
                temp[col] = ' ';
                lines[row] = new string(temp);
            }

            foreach (var item in lines)
            {
                Console.WriteLine(item.Replace(" ", ""));
            }
        }
    }
}
