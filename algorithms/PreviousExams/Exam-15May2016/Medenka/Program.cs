using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medenka
{
    class Program
    {
        static List<string> medenka;
        static StringBuilder sb;
        static void Main(string[] args)
        {
            medenka = Console.ReadLine().Split().ToList();
            sb = new StringBuilder();
            SplitMedenka(0, 0);
            Console.WriteLine(sb.ToString().TrimEnd());
        }

        private static void SplitMedenka(int index, int nutCount)
        {
            if (index >= medenka.Count)
            {
                if (nutCount > 0)
                {
                    sb.AppendLine(string.Join("", medenka));
                }
                return;
            }

            if (nutCount == 1)
            {
                medenka.Insert(index, "|");
                SplitMedenka(index + 1, 0);
                medenka.RemoveAt(index);
            }

            nutCount += int.Parse(medenka[index]);

            if (nutCount <= 1)
            {
                SplitMedenka(index + 1, nutCount);
            }
        }
    }
}
