using System.Linq;

namespace PointInRectangle
{
    public class Rectangle
    {
        public Point TopLeft { get; set; }
        public Point BottomRight { get; set; }

        public Rectangle(Point topLeft, Point bottomRight)
        {
            this.TopLeft = topLeft;
            this.BottomRight = bottomRight;
        }

        public static Rectangle Parse(string allCoordinates)
        {
            int[] coordinates = allCoordinates.Split().Select(int.Parse).ToArray();
            Rectangle result =
                new Rectangle(new Point(coordinates[0], coordinates[1]), new Point(coordinates[2], coordinates[3]));
            return result;
        }

        public bool Contains(Point point)
        {
            bool result =
                point.X >= this.TopLeft.X && point.X <= this.BottomRight.X &&
                point.Y >= this.TopLeft.Y && point.Y <= this.BottomRight.Y;
            return result;
        }
    }
}
