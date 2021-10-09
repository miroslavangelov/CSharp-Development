using System;
using System.Data;

namespace Warships
{
    class Program
    {
        static void Main(string[] args)
        {
            int fieldSize = int.Parse(Console.ReadLine());
            string[] commands = Console.ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries);
            string[,] battleField = new string[fieldSize, fieldSize];
            int firstPlayerShips = 0;
            int secondPlayerShips = 0;
            int totalShipsDestroyed = 0;
            bool playerWon = false;

            for (int i = 0; i < fieldSize; i++)
            {
                string[] currentRow = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < currentRow.Length; j++)
                {
                    if (currentRow[j] == "<")
                    {
                        firstPlayerShips++;
                    }
                    else if (currentRow[j] == ">")
                    {
                        secondPlayerShips++;
                    }
                    battleField[i, j] = currentRow[j];
                }
            }

            for (int i = 0; i < commands.Length; i++)
            {
                string[] currentCommand = commands[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int row = int.Parse(currentCommand[0]);
                int col = int.Parse(currentCommand[1]);

                if (IsInRange(row, col, fieldSize))
                {
                    string target = battleField[row, col];

                    if (target == "#")
                    {
                        battleField[row, col] = "X";
                        if (IsInRange(row + 1, col, fieldSize))
                        {
                            if (battleField[row + 1, col] == "<")
                            {
                                battleField[row + 1, col] = "X";
                                firstPlayerShips--;
                                totalShipsDestroyed++;
                            }
                            else if (battleField[row + 1, col] == ">")
                            {
                                battleField[row + 1, col] = "X";
                                secondPlayerShips--;
                                totalShipsDestroyed++;
                            }
                        }
                        if (IsInRange(row - 1, col, fieldSize))
                        {
                            if (battleField[row - 1, col] == "<")
                            {
                                battleField[row - 1, col] = "X";
                                firstPlayerShips--;
                                totalShipsDestroyed++;
                            }
                            else if (battleField[row - 1, col] == ">")
                            {
                                battleField[row - 1, col] = "X";
                                secondPlayerShips--;
                                totalShipsDestroyed++;
                            }
                        }
                        if (IsInRange(row, col + 1, fieldSize))
                        {
                            if (battleField[row, col + 1] == "<")
                            {
                                battleField[row, col + 1] = "X";
                                firstPlayerShips--;
                                totalShipsDestroyed++;
                            }
                            else if (battleField[row, col + 1] == ">")
                            {
                                battleField[row, col + 1] = "X";
                                secondPlayerShips--;
                                totalShipsDestroyed++;
                            }
                        }
                        if (IsInRange(row, col - 1, fieldSize))
                        {
                            if (battleField[row, col - 1] == "<")
                            {
                                battleField[row, col - 1] = "X";
                                firstPlayerShips--;
                                totalShipsDestroyed++;
                            }
                            else if (battleField[row, col - 1] == ">")
                            {
                                battleField[row, col - 1] = "X";
                                secondPlayerShips--;
                                totalShipsDestroyed++;
                            }
                        }
                        if (IsInRange(row - 1, col + 1, fieldSize))
                        {
                            if (battleField[row - 1, col + 1] == "<")
                            {
                                battleField[row - 1, col + 1] = "X";
                                firstPlayerShips--;
                                totalShipsDestroyed++;
                            }
                            else if (battleField[row - 1, col + 1] == ">")
                            {
                                battleField[row - 1, col + 1] = "X";
                                secondPlayerShips--;
                                totalShipsDestroyed++;
                            }
                        }
                        if (IsInRange(row - 1, col - 1, fieldSize))
                        {
                            if (battleField[row - 1, col - 1] == "<")
                            {
                                battleField[row - 1, col - 1] = "X";
                                firstPlayerShips--;
                                totalShipsDestroyed++;
                            }
                            else if (battleField[row - 1, col - 1] == ">")
                            {
                                battleField[row - 1, col - 1] = "X";
                                secondPlayerShips--;
                                totalShipsDestroyed++;
                            }
                        }
                        if (IsInRange(row + 1, col - 1, fieldSize))
                        {
                            if (battleField[row + 1, col - 1] == "<")
                            {
                                battleField[row + 1, col - 1] = "X";
                                firstPlayerShips--;
                                totalShipsDestroyed++;
                            }
                            else if (battleField[row + 1, col - 1] == ">")
                            {
                                battleField[row + 1, col - 1] = "X";
                                secondPlayerShips--;
                                totalShipsDestroyed++;
                            }
                        }
                        if (IsInRange(row + 1, col + 1, fieldSize))
                        {
                            if (battleField[row + 1, col + 1] == "<")
                            {
                                battleField[row + 1, col + 1] = "X";
                                firstPlayerShips--;
                                totalShipsDestroyed++;
                            }
                            else if (battleField[row + 1, col + 1] == ">")
                            {
                                battleField[row + 1, col + 1] = "X";
                                secondPlayerShips--;
                                totalShipsDestroyed++;
                            }
                        }
                    }
                    else if (target == "<")
                    {
                        battleField[row, col] = "X";
                        firstPlayerShips--;
                        totalShipsDestroyed++;
                    }
                    else if (target == ">")
                    {
                        battleField[row, col] = "X";
                        secondPlayerShips--;
                        totalShipsDestroyed++;
                    }
                }

                if (firstPlayerShips == 0)
                {
                    playerWon = true;
                    Console.WriteLine($"Player Two has won the game! {totalShipsDestroyed} ships have been sunk in the battle.");
                    break;
                }

                if (secondPlayerShips == 0)
                {
                    playerWon = true;
                    Console.WriteLine($"Player One has won the game! {totalShipsDestroyed} ships have been sunk in the battle.");
                    break;
                }
            }

            if (!playerWon)
            {
                Console.WriteLine($"It's a draw! Player One has {firstPlayerShips} ships left. Player Two has {secondPlayerShips} ships left.");
            }

            static bool IsInRange(int row, int col, int fieldSize)
            {
                return row >= 0 && col >= 0 && row < fieldSize && col < fieldSize;
            }
        }
    }
}