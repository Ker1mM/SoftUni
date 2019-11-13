using System;
using System.Text;

namespace RhombusOfStars
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());
            PrintTop(input);
            PrintBottom(input);
        }

        public static void PrintTop(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                int spaceCount = count - i;
                PrintRow(i, spaceCount);
            }
        }

        public static void PrintBottom(int count)
        {
            for (int i = count - 1; i >= 1; i--)
            {
                int spaceCount = count - i;
                PrintRow(i, spaceCount);
            }
        }

        public static void PrintRow(int startCount, int spaceCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(new string(' ', spaceCount));
            sb.Append(GetStars(startCount));
            Console.WriteLine(sb);
        }

        public static string GetStars(int count)
        {
            string result = String.Join(" ", new string('*', count).ToCharArray());
            return result;
        }
    }
}
