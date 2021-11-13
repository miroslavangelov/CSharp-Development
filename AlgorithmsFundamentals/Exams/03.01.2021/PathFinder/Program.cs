using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    class Program
    {
        private static Dictionary<int, List<int>> graph;
        private static List<List<int>> paths = new List<List<int>>();

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            graph = ReadGraph(nodesCount);
            
            int pathsCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < pathsCount; i++)
            {
                int[] currentPath = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
                if (pathExists(currentPath))
                {
                    Console.WriteLine("yes");
                }
                else
                {
                    Console.WriteLine("no");
                }
            }
        }

        private static bool pathExists(int[] path)
        {
            int currentNode = path[0];
            bool pathExists = true;

            for (int i = 0; i < path.Length - 1; i++)
            {
                if (graph[currentNode].Count == 0)
                {
                    pathExists = false;
                    break;
                }

                if (graph[currentNode].Contains(path[i + 1]))
                {
                    currentNode = path[i + 1];
                }
                else
                {
                    pathExists = false;
                    break;
                }
            }

            return pathExists;
        }

        private static Dictionary<int, List<int>> ReadGraph(int nodesCount)
        {
            Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();

            for (int i = 0; i < nodesCount; i++)
            {
                String nodeData = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nodeData))
                {
                    result[i] = new List<int>();
                    continue;
                }

                List<int> children = nodeData.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                result[i] = children;
            }

            return result;
        }
    }
}