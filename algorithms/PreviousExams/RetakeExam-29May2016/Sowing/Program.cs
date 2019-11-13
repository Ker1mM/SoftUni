using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sowing
{
    class Program
    {
        static char[] field;
        static StringBuilder sb;
        static void Main(string[] args)
        {
            int seedCount = int.Parse(Console.ReadLine());
            field = Console.ReadLine().Replace(" ", "").ToCharArray();
            var vector = new char[field.Length];
            sb = new StringBuilder();
            PlantSeeds(0, seedCount, vector);
            //Console.WriteLine(sb.ToString().TrimEnd());
        }

        static void PlantSeeds(int index, int seedLeft, char[] vector)
        {
            if (index >= field.Length)
            {
                if (seedLeft == 0)
                {
                    Console.WriteLine(new string(vector));
                }
                return;
            }

            vector[index] = field[index];
            PlantSeeds(index + 1, seedLeft, vector);
            if (seedLeft > 0 && vector[index] == '1' && (index == 0 || vector[index - 1] != '.'))
            {
                vector[index] = '.';
                PlantSeeds(index + 1, seedLeft - 1, vector);
            }
        }
    }
}
