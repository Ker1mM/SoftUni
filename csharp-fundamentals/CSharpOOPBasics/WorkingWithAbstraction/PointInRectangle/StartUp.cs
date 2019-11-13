using System;

namespace PointInRectangle
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Rectangle rectangle = Rectangle.Parse(Console.ReadLine());
            int count = int.Parse(Console.ReadLine());
            while (count-- > 0)
            {
                Point point = Point.Parse(Console.ReadLine());
                Console.WriteLine(rectangle.Contains(point));
            }
        }
    }
}
