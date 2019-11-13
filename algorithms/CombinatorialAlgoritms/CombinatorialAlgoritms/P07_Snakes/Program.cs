using System;
using System.Collections.Generic;
using System.Linq;

namespace P07_Snakes
{
    class Program
    {
        private static int inputParameter;
        private static int snakesCount;
        private static HashSet<string> usedSnakes = new HashSet<string>();

        public static void Main()
        {
            inputParameter = int.Parse(Console.ReadLine());
            Stack<Cell> snake = new Stack<Cell>();
            List<char> directions = new List<char>();

            GenerateSnake(new Cell { Row = 0, Column = 0 }, 'S', snake, directions);

            Console.WriteLine("Snakes count = {0}", snakesCount);
        }

        private static void GenerateSnake(Cell cell, char direction, Stack<Cell> snake, List<char> directions)
        {
            if (snake.Count == inputParameter)
            {
                string currentSnake = new string(directions.ToArray());

                if (!usedSnakes.Contains(currentSnake))
                {
                    Console.WriteLine(currentSnake);
                    snakesCount++;
                    usedSnakes.Add(currentSnake);
                    MarkIsomorphicSnakes(directions.ToList());
                }

                return;
            }

            if (snake.Contains(cell))
            {
                return;
            }

            snake.Push(cell);
            directions.Add(direction);

            GenerateSnake(new Cell { Row = cell.Row, Column = cell.Column + 1 }, 'R', snake, directions);

            if (snake.Count > 1)
            {
                GenerateSnake(new Cell { Row = cell.Row + 1, Column = cell.Column }, 'D', snake, directions);
            }

            if (snake.Count > 2)
            {
                GenerateSnake(new Cell { Row = cell.Row, Column = cell.Column - 1 }, 'L', snake, directions);
            }

            if (snake.Count > 3)
            {
                GenerateSnake(new Cell { Row = cell.Row - 1, Column = cell.Column }, 'U', snake, directions);
            }

            snake.Pop();
            directions.RemoveAt(directions.Count - 1);
        }

        private static void MarkIsomorphicSnakes(List<char> directions)
        {
            Flip(directions);
            usedSnakes.Add(new string(directions.ToArray()));

            ChangeHeadAndTail(directions);
            directions.Reverse();
            while (directions[1] != 'R')
            {
                RotateClockwise(directions);
            }

            usedSnakes.Add(new string(directions.ToArray()));

            Flip(directions);
            usedSnakes.Add(new string(directions.ToArray()));
        }

        private static void ChangeHeadAndTail(List<char> directions)
        {
            char tmp = directions[0];
            directions.RemoveAt(0);
            directions.Add(tmp);

            for (int index = 0; index < directions.Count; index++)
            {
                switch (directions[index])
                {
                    case 'U':
                        directions[index] = 'D';
                        break;
                    case 'D':
                        directions[index] = 'U';
                        break;
                    case 'R':
                        directions[index] = 'L';
                        break;
                    case 'L':
                        directions[index] = 'R';
                        break;
                }
            }
        }

        private static void RotateClockwise(List<char> directions)
        {
            for (int index = 0; index < directions.Count; index++)
            {
                switch (directions[index])
                {
                    case 'U':
                        directions[index] = 'R';
                        break;
                    case 'D':
                        directions[index] = 'L';
                        break;
                    case 'R':
                        directions[index] = 'D';
                        break;
                    case 'L':
                        directions[index] = 'U';
                        break;
                }
            }
        }

        private static void Flip(List<char> directions)
        {
            for (int index = 0; index < directions.Count; index++)
            {
                switch (directions[index])
                {
                    case 'U':
                        directions[index] = 'D';
                        break;
                    case 'D':
                        directions[index] = 'U';
                        break;
                }
            }
        }

        public struct Cell
        {
            public int Row { get; set; }

            public int Column { get; set; }
        }

    }
}
