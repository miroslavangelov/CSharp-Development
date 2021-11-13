using System;

namespace TBC
{
    class Program
    {
        private static char[,] matrix;
        
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            matrix = new char[rows, cols];
            int tunnelsCount = 0;

            for (int i = 0; i < rows; i++)
            {
                char[] input = Console.ReadLine().ToCharArray();
                for (int j = 0; j < input.Length; j++)
                {
                    matrix[i, j] = input[j];
                }
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 't')
                    {
                        tunnelsCount++;
                        FindTunnels(i, j);
                    }
                }
            }
            Console.WriteLine(tunnelsCount);
        }

        private static void FindTunnels(int row, int col)
        {
            if (row >= matrix.GetLength(0) || col >= matrix.GetLength(1) ||
                row < 0 || col < 0 || matrix[row, col] != 't')
            {
                return;
            }

            matrix[row, col] = 'X';
            FindTunnels(row, col + 1); //right
            FindTunnels(row + 1, col); //down
            FindTunnels(row, col - 1); //left
            FindTunnels(row - 1, col); //up
            FindTunnels(row - 1, col - 1); //up-left
            FindTunnels(row - 1, col + 1); //up-right
            FindTunnels(row + 1, col + 1); //down-right
            FindTunnels(row + 1, col - 1); //down-left
        }
    }
}
