using System;
using System.Collections.Generic;

namespace Boxes
{
    class Program
    {
        static void Main(string[] args)
        {
            int boxesCount = int.Parse(Console.ReadLine());
            Box[] boxes = new Box[boxesCount];

            for (int i = 0; i < boxesCount; i++)
            {
                string[] boxData = Console.ReadLine().Split(" ");
                Box box = new Box
                {
                    Width = int.Parse(boxData[0]),
                    Depth = int.Parse(boxData[1]),
                    Height = int.Parse(boxData[2])
                };
                boxes[i] = box;
            }

            int[] solutions = new int[boxes.Length];
            int[] previousSolutions = new int[boxes.Length];
            int maxSolution = 0;
            int maxSolutionIndex = 0;

            for (int i = 0; i < boxes.Length; i++) {
                int solution = 1;
                Box currentBox = boxes[i];
                int previousIndex = -1;

                for (int j = 0; j < i; j++) {
                    Box previousBox = boxes[j];
                    int previousSolution = solutions[j];

                    if (currentBox.Depth > previousBox.Depth &&
                        currentBox.Width > previousBox.Width &&
                        currentBox.Height > previousBox.Height &&
                        solution <= previousSolution) {
                        solution = previousSolution + 1;
                        previousIndex = j;
                    }
                }

                solutions[i] = solution;
                previousSolutions[i] = previousIndex;
                if (solution > maxSolution) {
                    maxSolution = solution;
                    maxSolutionIndex = i;
                }
            }

            int index = maxSolutionIndex;
            Stack<Box> result = new Stack<Box>();

            while (index != -1) {
                Box currentBox = boxes[index];
                result.Push(currentBox);
                index = previousSolutions[index];
            }

            foreach (Box box in result)
            {
                Console.WriteLine(string.Format($"{box.Width} {box.Depth} {box.Height}"));
            }
        }

        public class Box
        {
            public int Width { get; set; }

            public int Depth { get; set; }

            public int Height { get; set; }
        }
    }
}