using System.Linq;

namespace PointInRectangle
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Point Parse(string allCoordinates)
        {
            int[] coordinates = allCoordinates.Split().Select(int.Parse).ToArray();
            Point result = new Point(coordinates[0], coordinates[1]);
            return result;
        }
    }
}
