using System;

namespace Survivor
{
    class Program
    {
        static void Main(string[] args)
        {
            int beachRows = int.Parse(Console.ReadLine());
            string[][] beach = new string[beachRows][];

            for (int i = 0; i < beachRows; i++)
            {
                string[] currentRow = Console.ReadLine().Split(' ');
                beach[i] = currentRow;
            }

            string command = Console.ReadLine();
            int collectedTokens = 0;
            int opponentTokens = 0;

            while (command != "Gong")
            {
                string[] commandData = command.Split(' ');

                if (commandData[0] == "Find")
                {
                    int currentRow = int.Parse(commandData[1]);
                    int currentCol = int.Parse(commandData[2]);

                    try
                    {
                        string target = beach[currentRow][currentCol];
                        if (target == "T")
                        {
                            collectedTokens += 1;
                            beach[currentRow][currentCol] = "-";
                        }
                    }
                    catch (IndexOutOfRangeException exception)
                    {

                    }
                }
                else if (commandData[0] == "Opponent")
                {
                    int currentRow = int.Parse(commandData[1]);
                    int currentCol = int.Parse(commandData[2]);
                    string direction = commandData[3];

                    try
                    {
                        string target = beach[currentRow][currentCol];
                        if (target == "T")
                        {
                            opponentTokens += 1;
                            beach[currentRow][currentCol] = "-";
                        }

                        switch (direction)
                        {
                            case "up":
                                for (int i = 1; i <= 3; i++)
                                {
                                    target = beach[currentRow - i][currentCol];
                                    if (target == "T")
                                    {
                                        opponentTokens += 1;
                                        beach[currentRow - i][currentCol] = "-";
                                    }
                                }
                                break;
                            case "down":
                                for (int i = 1; i <= 3; i++)
                                {
                                    target = beach[currentRow + i][currentCol];
                                    if (target == "T")
                                    {
                                        opponentTokens += 1;
                                        beach[currentRow + i][currentCol] = "-";
                                    }
                                }
                                break;
                            case "left":
                                for (int i = 1; i <= 3; i++)
                                {
                                    target = beach[currentRow][currentCol - i];
                                    if (target == "T")
                                    {
                                        opponentTokens += 1;
                                        beach[currentRow][currentCol - i] = "-";
                                    }
                                }
                                break;
                            case "right":
                                for (int i = 1; i <= 3; i++)
                                {
                                    target = beach[currentRow][currentCol + i];
                                    if (target == "T")
                                    {
                                        opponentTokens += 1;
                                        beach[currentRow][currentCol + i] = "-";
                                    }
                                }
                                break;
                        }
                    }
                    catch (IndexOutOfRangeException exception)
                    {

                    }
                }
                command = Console.ReadLine();
            }

            foreach (var t in beach)
            {
                foreach (var t1 in t)
                {
                    Console.Write(t1 + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Collected tokens: {collectedTokens}");
            Console.WriteLine($"Opponent's tokens: {opponentTokens}");
        }
    }
}