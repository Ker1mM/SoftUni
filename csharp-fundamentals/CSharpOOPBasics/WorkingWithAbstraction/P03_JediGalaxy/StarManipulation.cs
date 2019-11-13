namespace P03_JediGalaxy
{
    public static class StarManipulation
    {
        public static int[,] FillMatrix(int[] dimensions)
        {
            int x = dimensions[0];
            int y = dimensions[1];

            int[,] matrix = new int[x, y];

            int value = 0;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    matrix[i, j] = value++;
                }
            }

            return matrix;
        }

        public static void DestroyStars(int[,] matrix, int[] evilCoordinates)
        {
            int evilRow = evilCoordinates[0];
            int evilCol = evilCoordinates[1];

            while (evilRow >= 0 && evilCol >= 0)
            {
                if (evilRow >= 0 && evilRow < matrix.GetLength(0) &&
                    evilCol >= 0 && evilCol < matrix.GetLength(1))
                {
                    matrix[evilRow, evilCol] = 0;
                }
                evilRow--;
                evilCol--;
            }
        }

        public static int CollectStars(int[,] matrix, int[] ivoCoordinates)
        {

            int ivoRow = ivoCoordinates[0];
            int ivoCol = ivoCoordinates[1];
            int sum = 0;

            while (ivoRow >= 0 && ivoCol < matrix.GetLength(1))
            {
                if (ivoRow >= 0 && ivoRow < matrix.GetLength(0) &&
                    ivoCol >= 0 && ivoCol < matrix.GetLength(1))
                {
                    sum += matrix[ivoRow, ivoCol];
                }

                ivoCol++;
                ivoRow--;
            }

            return sum;
        }
    }
}
