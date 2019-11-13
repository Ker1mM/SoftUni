using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blocks
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            var result = CalculateCubes.GetCubes(count);

            Console.WriteLine($"Number of blocks: {result.Count}");
            Console.WriteLine(string.Join(Environment.NewLine, result));
        }


    }

    static class CalculateCubes
    {
        private static int count;
        private static int[] cube;
        private static bool[] visited;
        private static List<string> result;
        private static HashSet<string> generatedCubes;


        public static List<string> GetCubes(int letterCount)
        {
            result = new List<string>();
            generatedCubes = new HashSet<string>();
            count = letterCount;
            visited = new bool[count];
            cube = new int[4];

            Generate(0);

            return result;
        }

        private static void SaveCube(string cube)
        {
            result.Add(cube);
            generatedCubes.Add(cube);
            generatedCubes.Add(new string(new[] { cube[3], cube[0], cube[1], cube[2] }));
            generatedCubes.Add(new string(new[] { cube[2], cube[3], cube[0], cube[1] }));
            generatedCubes.Add(new string(new[] { cube[1], cube[2], cube[3], cube[0] }));
        }

        private static string NumberToLetterCube(int[] cube)
        {
            var result = new char[4];
            for (int i = 0; i < 4; i++)
            {
                result[i] = (char)('A' + cube[i]);
            }

            return string.Join("", result);
        }

        private static void Generate(int index)
        {

            if (index >= 4)
            {
                var generatedCube = NumberToLetterCube(cube);
                if (!generatedCubes.Contains(generatedCube))
                {
                    SaveCube(generatedCube);
                }
                return;
            }

            for (int i = 0; i < visited.Length; i++)
            {
                if (!visited[i])
                {
                    cube[index] = i;
                    visited[i] = true;
                    Generate(index + 1);
                    visited[i] = false;
                }
            }

        }
    }
}
