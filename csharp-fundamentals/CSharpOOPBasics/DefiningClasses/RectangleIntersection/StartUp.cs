using System;
using System.Collections.Generic;
using System.Linq;

namespace RectangleIntersection
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int[] inputParameters = Console.ReadLine().Split().Select(int.Parse).ToArray();
            List<Rectangle> rectangles = new List<Rectangle>();
            for (int i = 0; i < inputParameters[0]; i++)
            {
                string[] tokens = Console.ReadLine().Split();
                if (tokens.Length != 5)
                {
                    continue;
                }
                string id = tokens[0];
                double width = double.Parse(tokens[1]);
                double height = double.Parse(tokens[2]);
                double x = double.Parse(tokens[3]);
                double y = double.Parse(tokens[4]);

                rectangles.Add(new Rectangle(id, width, height, x, y));
            }

            for (int i = 0; i < inputParameters[1]; i++)
            {
                string[] tokens = Console.ReadLine().Split();

                Rectangle one = rectangles.FirstOrDefault(x => x.Id == tokens[0]);
                Rectangle two = rectangles.FirstOrDefault(x => x.Id == tokens[1]);

                Console.WriteLine(one.Intersect(two).ToString().ToLower());
            }

        }
    }
}
