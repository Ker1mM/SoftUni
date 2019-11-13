using System;
using System.Collections.Generic;
using System.Linq;

namespace RectangleIntersection
{
    class Program
    {
        private static Dictionary<int, int> v;
        private static Dictionary<int, int> sum;
        private static long area;
        private static int minX;
        private static int maxX;
        private static int minY;
        private static int maxY;
        private static List<int[]> intersections;
        private static List<int[]> edges;

        static void Main(string[] args)
        {
            v = new Dictionary<int, int>();
            sum = new Dictionary<int, int>();
            //GetArea();
            //Console.WriteLine(area);
            GetEdges();
            GetArea();
            Console.WriteLine(area);
            ;
        }

        private static void GetEdges()
        {
            ReadRectangles();
            AdjustIntersectionCoordinates();

            edges = new List<int[]>();

            foreach (var intersection in intersections)
            {
                var leftEdge = new int[]
                {
                    intersection[0],
                    intersection[2],
                    intersection[3],
                    1
                };

                var rightEdge = new int[]
                {
                    intersection[1],
                    intersection[2],
                    intersection[3],
                    -1
                };

                edges.Add(leftEdge);
                edges.Add(rightEdge);
            }

            edges = edges.OrderBy(x => x[0]).ToList();
        }

        private static void ReadRectangles()
        {
            int count = int.Parse(Console.ReadLine());

            var rectangles = new List<int[]>();
            intersections = new List<int[]>();
            minY = 2000;
            maxY = -2000;

            for (int i = 0; i < count; i++)
            {
                int[] coordinates = Console.ReadLine().Split().Select(int.Parse).ToArray();
                AddIfIntersect(rectangles, coordinates);
            }
        }

        private static void AddIfIntersect(List<int[]> rectangles, int[] newRectangle)
        {
            foreach (var rectangle in rectangles)
            {
                if (rectangle[1] > newRectangle[0] &&
                    rectangle[0] < newRectangle[1] &&
                    rectangle[3] > newRectangle[2] &&
                    rectangle[2] < newRectangle[3])
                {
                    var intersection = new int[]
                    {
                        Math.Max(rectangle[0], newRectangle[0]),
                        Math.Min(rectangle[1], newRectangle[1]),
                        Math.Max(rectangle[2], newRectangle[2]),
                        Math.Min(rectangle[3], newRectangle[3]),
                    };

                    intersections.Add(intersection);

                    minX = minX > intersection[0] ? intersection[0] : minX;
                    minY = minY > intersection[2] ? intersection[2] : minY;

                    maxX = maxX < intersection[1] ? intersection[1] : maxX;
                    maxY = maxY < intersection[3] ? intersection[3] : maxY;
                }
            }

            rectangles.Add(newRectangle);
        }

        private static void AdjustIntersectionCoordinates()
        {
            int xAdjust = 0;
            int yAdjust = 0;

            if (minX < 0)
            {
                xAdjust = minX * -1;
                minX = 0;
                maxX += xAdjust;
            }

            if (minY < 0)
            {
                yAdjust = minY * -1;
                minY = 0;
                maxY += yAdjust;
            }

            if (xAdjust != 0 || yAdjust != 0)
            {
                foreach (var intersection in intersections)
                {
                    intersection[0] += xAdjust;
                    intersection[1] += xAdjust;
                    intersection[2] += yAdjust;
                    intersection[3] += yAdjust;
                }
            }
        }

        private static void GetArea()
        {
            int lastX = 0;
            sum.Add(1, 0);
            v.Add(1, 0);
            for (int i = 0; i < edges.Count; i++)
            {
                int[] args = edges[i];
                area = area + (sum[1] * (args[0] - lastX));


                lastX = args[0];

                if (args[3] == 1)
                {
                    ModifyInterval(1, args[1], args[2], minY, maxY, +1);
                }
                else
                {
                    ModifyInterval(1, args[1], args[2], minY, maxY, -1);
                }
            }
        }

        private static void ModifyInterval(int node, int y0, int y1, int intervalStart, int intervalEnd, int delta)
        {
            if (y0 <= intervalStart && y1 >= intervalEnd)
            {
                if (!v.ContainsKey(node))
                {
                    v.Add(node, 0);
                }

                v[node] += delta;
            }
            else
            {
                var middle = (intervalStart + intervalEnd) / 2;

                int middleY0 = y0;
                int middleY1 = y1;

                if (middle > y0 && middle < y1)
                {
                    middleY1 = middle;
                    middleY0 = middle;
                }

                if (y0 < middle)
                {
                    ModifyInterval(node * 2, y0, middleY1, intervalStart, middle, delta);
                }

                if (y1 > middle)
                {
                    ModifyInterval(node * 2 + 1, middleY0, y1, middle, intervalEnd, delta);
                }
            }

            Correct(node, intervalStart, intervalEnd);
        }

        private static void Correct(int node, int intervalStart, int intervalEnd)
        {
            if (!sum.ContainsKey(node))
            {
                sum.Add(node, 0);
            }

            if (!v.ContainsKey(node))
            {
                v.Add(node, 0);
            }

            if (v[node] > 0)
            {
                sum[node] = intervalEnd - intervalStart;

                if (intervalEnd == intervalStart)
                {
                    sum[node] += 1;
                }
            }
            else
            {
                sum[node] = 0;

                if (!sum.ContainsKey(node * 2))
                {
                    sum.Add(node * 2, 0);
                }

                if (!sum.ContainsKey(node * 2 + 1))
                {
                    sum.Add(node * 2 + 1, 0);
                }


                if (intervalStart < intervalEnd)
                {
                    sum[node] = sum[node * 2] + sum[node * 2 + 1];
                }
            }

        }
    }
}
