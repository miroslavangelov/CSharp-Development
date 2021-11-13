using System;
using System.Collections.Generic;
using System.Linq;

namespace Climbing
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            int[,] matrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                int[] input = Console.ReadLine()
                    .Split(" ")
                    .Select(number => int.Parse(number)).ToArray();
                for (int j = 0; j < input.Length; j++)
                {
                    matrix[i, j] = input[j];
                }
            }

            int[,] sums = new int[rows, cols];
            sums[0, 0] = matrix[0, 0];

            for (int row = 1; row < rows; row++) {
                sums[row, 0] = sums[row - 1, 0] + matrix[row, 0];
            }

            for (int col = 1; col < cols; col++) {
                sums[0, col] = sums[0, col - 1] + matrix[0, col];
            }

            for (int row = 1; row < rows; row++) {
                for (int col = 1; col < cols; col++) {
                    int max = Math.Max(sums[row - 1, col], sums[row, col - 1]) + matrix[row, col];
                    sums[row, col] = max;
                }
            }

            List<int> result = new List<int>();
            int currentRow = rows - 1;
            int currentCol = cols - 1;
            result.Add(matrix[currentRow, currentCol]);

            while (currentRow != 0 || currentCol != 0)
            {
                int top = -1;
                if (currentRow - 1 >= 0) {
                    top = sums[currentRow - 1, currentCol];
                }

                int left = -1;
                if (currentCol - 1 >= 0) {
                    left = sums[currentRow, currentCol - 1];
                }

                if (top > left) {
                    result.Add(matrix[currentRow - 1, currentCol]);
                    currentRow--;
                } else {
                    result.Add(matrix[currentRow, currentCol - 1]);
                    currentCol--;
                }
            }

            Console.WriteLine(sums[rows - 1, cols -1]);
            Console.WriteLine(string.Join(" ", result));
        }
    }
}
