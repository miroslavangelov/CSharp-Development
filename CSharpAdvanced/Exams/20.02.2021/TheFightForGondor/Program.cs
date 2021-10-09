using System;
using System.Collections.Generic;
using System.Linq;

namespace TheFightForGondor
{
    class Program
    {
        static void Main(string[] args)
        {
            int wavesCount = int.Parse(Console.ReadLine());
            Queue<int> plates = new Queue<int>(Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse));
            Stack<int> orcs = new Stack<int>();

            for (int i = 1; i <= wavesCount; i++)
            {
                orcs = new Stack<int>(Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse));

                if (i % 3 == 0)
                {
                    int extraPlate = int.Parse(Console.ReadLine());
                    plates.Enqueue(extraPlate);
                }

                while (orcs.Any() && plates.Any())
                {
                    int plate = plates.Peek();
                    int orc = orcs.Peek();

                    if (orc > plate)
                    {
                        int orcLeftPower = orc - plate;

                        plates.Dequeue();
                        orcs.Pop();
                        orcs.Push(orcLeftPower);
                    }
                    else if (plate > orc)
                    {
                        int plateLeftPower = plate - orc;

                        orcs.Pop();
                        plates.Dequeue();

                        int[] platesArray = new int[plates.Count];
                        
                        plates.CopyTo(platesArray, 0);
                        plates.Clear();

                        plates.Enqueue(plateLeftPower);
                        
                        foreach (var plateLeft in platesArray)
                        {
                            plates.Enqueue(plateLeft);
                        }
                    }
                    else if (plate == orc)
                    {
                        orcs.Pop();
                        plates.Dequeue();
                    }
                }

                if (plates.Count == 0)
                {
                    break;
                }
            }

            if (plates.Count == 0)
            {
                Console.WriteLine("The orcs successfully destroyed the Gondor's defense.");
                Console.WriteLine("Orcs left: " + string.Join(", ", orcs));
            }
            else
            {
                Console.WriteLine("The people successfully repulsed the orc's attack.");
                Console.WriteLine("Plates left: " + string.Join(", ", plates));
            }
        }
    }
}