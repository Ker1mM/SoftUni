using System;

namespace Dome
{
    class Program
    {
        static void Main(string[] args)
        {
            double aFactor = 0.61803;
            double bFactor = 0.54653;



            double sideA = 5;
            double sideB = 4.4;

            Console.WriteLine($"Side A = {sideA:F1}");
            Console.WriteLine($"Side B = {sideB:F1}");


            double s1 = (sideA + sideA + sideA) / 2;
            double s2 = (2 * sideB + sideA) / 2;

            double area1 = FindArea(sideA, sideA, sideA);
            double area2 = FindArea(sideA, sideB, sideB);

            double hA1 = (2 / sideA) * (area1);
            double hA2 = (2 / sideA) * (area2);

            double hB = (2 / sideB) * (area2);

            Console.WriteLine($"hA1 = {hA1}");
            Console.WriteLine();
            Console.WriteLine($"hA2 = {hA2}");
            Console.WriteLine($"hB = {hB}");
        }

        public static double FindArea(double side1, double side2, double side3)
        {
            double s = (side1 + side2 + side3) / 2;

            return Math.Sqrt(s * (s - side1) * (s - side2) * (s - side3));

        }
    }
}
