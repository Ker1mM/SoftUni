using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace P14.MatrixShuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            string text = Console.ReadLine();

            int rowS = 0;
            int rowE = size - 1;
            int colS = 0;
            int colE = size - 1;
            int textIndex = 0;

            char[,] matrix = new char[size, size];
            while (rowS <= rowE && colS <= colE)
            {
                for (int i = colS; i <= colE; i++)
                {
                    char letter = textIndex < text.Length ? text[textIndex] : ' ';
                    matrix[rowS, i] = letter;
                    textIndex++;
                }
                rowS++;

                for (int i = rowS; i <= rowE; i++)
                {
                    char letter = textIndex < text.Length ? text[textIndex] : ' ';
                    matrix[i, colE] = letter;
                    textIndex++;
                }
                colE--;

                for (int i = colE; i >= colS; i--)
                {
                    char letter = textIndex < text.Length ? text[textIndex] : ' ';
                    matrix[rowE, i] = letter;
                    textIndex++;
                }
                rowE--;

                for (int i = rowE; i >= rowS; i--)
                {
                    char letter = textIndex < text.Length ? text[textIndex] : ' ';
                    matrix[i, colS] = letter;
                    textIndex++;
                }
                colS++;
            }

            StringBuilder evens = new StringBuilder();
            StringBuilder odds = new StringBuilder();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        evens.Append(matrix[i, j]);
                    }
                    else
                    {
                        odds.Append(matrix[i, j]);
                    }
                }
            }

            string sentence = evens.ToString() + odds.ToString();
            string trimmedSentence = Regex.Replace(sentence.ToLower(), @"[^a-z]", "");

            if (trimmedSentence == new string(trimmedSentence.ToCharArray().Reverse().ToArray()))
            {
                Console.WriteLine("<div style='background-color:#4FE000'>{0}</div>", sentence);
            }
            else
            {
                Console.WriteLine("<div style='background-color:#E0000F'>{0}</div>", sentence);
            }
        }
    }
}
