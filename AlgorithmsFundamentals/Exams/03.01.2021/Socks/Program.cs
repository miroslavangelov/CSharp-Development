using System;

namespace Socks
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] firstSock = Console.ReadLine().Split(' ');
            string[] secondSock = Console.ReadLine().Split(' ');
            int[,] lcs = new int[firstSock.Length + 1, secondSock.Length + 1];

            for (int row = 1; row <= firstSock.Length; row++)
            {
                for (int col = 1; col <= secondSock.Length; col++)
                {
                    if (firstSock[row - 1] == secondSock[col - 1])
                    {
                        lcs[row, col] = lcs[row - 1, col - 1] + 1;
                    }
                    else
                    {
                        int up = lcs[row - 1, col];
                        int left = lcs[row, col - 1];
                        lcs[row, col] = Math.Max(up, left);
                    }
                }
            }

            Console.WriteLine(lcs[firstSock.Length, secondSock.Length]);
        }
    }
}
