using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArrangeNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine()
                .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            numbers = numbers.OrderBy(x => IntToString(x)).ToList();
            Console.WriteLine(String.Join(", ", numbers));
        }

        public static string IntToString(int number)
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                int digit = number % 10;
                number /= 10;

                switch (digit)
                {
                    case 0:
                        sb.Insert(0, "zero");
                        break;
                    case 1:
                        sb.Insert(0, "one");
                        break;
                    case 2:
                        sb.Insert(0, "two");
                        break;
                    case 3:
                        sb.Insert(0, "three");
                        break;
                    case 4:
                        sb.Insert(0, "four");
                        break;
                    case 5:
                        sb.Insert(0, "five");
                        break;
                    case 6:
                        sb.Insert(0, "six");
                        break;
                    case 7:
                        sb.Insert(0, "seven");
                        break;
                    case 8:
                        sb.Insert(0, "eight");
                        break;
                    case 9:
                        sb.Insert(0, "nine");
                        break;
                    default:
                        break;
                }

                if (number == 0)
                {
                    break;
                }
                else
                {
                    sb.Insert(0, "-");
                }
            }

            return sb.ToString();
        }
    }
}
