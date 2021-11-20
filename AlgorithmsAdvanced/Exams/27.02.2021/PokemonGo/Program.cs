using System;
using System.Collections.Generic;

namespace PokemonGo
{
    class Program
    {
        private static List<Street> streets;
        private static int[,] knapSack;

        static void Main(string[] args)
        {
            int fuel = int.Parse(Console.ReadLine());
            string nextRow = Console.ReadLine();
            streets = new List<Street>();

            while (nextRow != "End")
            {
                string[] streetData = nextRow.Split(", ", StringSplitOptions.RemoveEmptyEntries);
                string streetName = streetData[0];
                int pokemonsCount = int.Parse(streetData[1]);
                int streetLength = int.Parse(streetData[2]);
                Street street = new Street
                {
                    Name = streetName,
                    Length = streetLength,
                    Value = pokemonsCount
                };
                streets.Add(street);

                nextRow = Console.ReadLine();
            }

            knapSack = new int[streets.Count + 1, fuel + 1];
            fillKnapsack();

            SortedSet<string> selectedStreets = new SortedSet<string>();
            int row = knapSack.GetLength(0) - 1;
            int col = knapSack.GetLength(1) - 1;
            int usedFuel = 0;
            int totalValue = 0;

            while (row > 0 && col > 0)
            {
                if (knapSack[row, col] != knapSack[row - 1, col])
                {
                    Street selectedItem = streets[row - 1];

                    selectedStreets.Add(selectedItem.Name);
                    usedFuel += selectedItem.Length;
                    totalValue += selectedItem.Value;
                    col -= selectedItem.Length;
                }

                row -= 1;
            }

            if (selectedStreets.Count > 0)
            {
                Console.WriteLine(string.Join(" -> ", selectedStreets));
            }
            Console.WriteLine($"Total Pokemon caught -> {totalValue}");
            Console.WriteLine($"Fuel Left -> {fuel - usedFuel}");
        }

        public class Street
        {
            public string Name { get; set; }

            public int Value { get; set; }

            public int Length { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        private static void fillKnapsack() {
            for (int itemIndex = 1; itemIndex < knapSack.GetLength(0); itemIndex++)
            {
                var currentItem = streets[itemIndex - 1];

                for (int capacity = 1; capacity < knapSack.GetLength(1); capacity++)
                {
                    if (capacity < currentItem.Length)
                    {
                        knapSack[itemIndex, capacity] = knapSack[itemIndex - 1, capacity];
                    }
                    else
                    {
                        knapSack[itemIndex, capacity] = Math.Max(
                            knapSack[itemIndex - 1, capacity],
                            knapSack[itemIndex - 1, capacity - currentItem.Length] + currentItem.Value);
                    }
                }
            }
        }
    }
}
