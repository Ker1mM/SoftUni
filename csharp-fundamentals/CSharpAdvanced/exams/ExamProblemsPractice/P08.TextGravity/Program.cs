using System;
using System.Text;

namespace P08.TextGravity
{
    class Program
    {
        static void Main(string[] args)
        {
            int lineLength = int.Parse(Console.ReadLine());

            char[] text = Console.ReadLine().ToCharArray();

            int rows = text.Length / lineLength;
            if (text.Length % lineLength != 0)
            {
                rows++;
            }

            char[][] matrix = new char[rows][];

            int counter = 0;
            for (int row = 0; row < rows; row++)
            {
                matrix[row] = new char[lineLength];
                for (int col = 0; col < lineLength; col++)
                {
                    if (counter < text.Length)
                    {
                        matrix[row][col] = text[counter];
                        counter++;
                    }
                    else
                    {
                        matrix[row][col] = ' ';
                    }
                }
            }

            for (int i = rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < lineLength; j++)
                {
                    int index = GetFirstNonEmptyIndex(matrix, j, i);
                    if (matrix[i][j] == ' ' && index >= 0)
                    {
                        matrix[i][j] = matrix[index][j];
                        matrix[index][j] = ' ';
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            foreach (var item in matrix)
            {
                sb.Append("<tr>");
                foreach (var current in item)
                {
                    sb.Append("<td>");
                    sb.Append(System.Security.SecurityElement.Escape(current.ToString()));
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            Console.WriteLine(sb);
        }

        public static int GetFirstNonEmptyIndex(char[][] matrix, int col, int row)
        {
            int result = -1;
            for (int i = row - 1; i >= 0; i--)
            {
                if (matrix[i][col] != ' ')
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
    }
}
