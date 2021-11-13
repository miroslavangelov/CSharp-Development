using System;

namespace SuperSet
{
    class Program
    {
        private static string[] elements;
        private static string[] combinations;
        
        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split(", ");

            for (int i = 0; i <= elements.Length; i++)
            {
                combinations = new string[i];
                GenerateSuperSets(0, 0);
            }
        }

        private static void GenerateSuperSets(int combinationsIndex, int elementsStartIndex)
        {
            if (combinationsIndex >= combinations.Length)
            {
                Console.WriteLine(string.Join(" ", combinations));
                return;
            }

            for (int i = elementsStartIndex; i < elements.Length; i++)
            {
                combinations[combinationsIndex] = elements[i];
                GenerateSuperSets(combinationsIndex + 1, i + 1);
            }
        }
    }
}
