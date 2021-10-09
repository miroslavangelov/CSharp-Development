using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> dishes = new Dictionary<string, int>
            {
                {"Dipping sauce", 150},
                {"Green salad", 250},
                {"Chocolate cake", 300},
                {"Lobster", 400}
            };
            int[] ingredients = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int[] freshnessLevels = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            SortedDictionary<string, int> preparedDishes = new SortedDictionary<string, int>();
            Queue<int> ingredientsQueue = new Queue<int>(ingredients);
            Stack<int> freshnessLevelsQueue = new Stack<int>(freshnessLevels);

            while (ingredientsQueue.Any() && freshnessLevelsQueue.Any())
            {
                int currentIngredient = ingredientsQueue.Peek();
                int currentFreshnessLevel = freshnessLevelsQueue.Peek();
                int product = currentIngredient * currentFreshnessLevel;

                if (currentIngredient == 0)
                {
                    ingredientsQueue.Dequeue();
                    continue;
                }

                if (dishes.ContainsValue(product))
                {
                    string currentDishes = dishes.FirstOrDefault(element => element.Value == product).Key;
                    if (preparedDishes.ContainsKey(currentDishes))
                    {
                        preparedDishes[currentDishes] += 1;
                    }
                    else
                    {
                        preparedDishes[currentDishes] = 1;
                    }

                    ingredientsQueue.Dequeue();
                    freshnessLevelsQueue.Pop();
                }
                else
                {
                    freshnessLevelsQueue.Pop();
                    ingredientsQueue.Dequeue();
                    ingredientsQueue.Enqueue(currentIngredient + 5);
                }
            }

            if (preparedDishes.Count == 4)
            {
                preparedDishes.OrderBy(element => element.Key);
                Console.WriteLine("Applause! The judges are fascinated by your dishes!");
                foreach (var element in preparedDishes)
                {
                    Console.WriteLine($"# {element.Key} --> {element.Value}");
                }
            }
            else
            {
                Console.WriteLine("You were voted off. Better luck next year.");
                if (ingredientsQueue.Count > 0)
                {
                    Console.WriteLine($"Ingredients left: {ingredientsQueue.Sum()}");
                }
                foreach (var element in preparedDishes)
                {
                    Console.WriteLine($"# {element.Key} --> {element.Value}");
                }
            }
        }
    }
}