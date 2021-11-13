using System;
using System.Collections.Generic;
using System.Linq;

namespace Paths
{
    class Program
    {
        private static Dictionary<int, List<int>> graph;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            
            graph = ReadGraph(nodesCount);
            for (int node = 0; node < graph.Count; node++)
            {
                List<int> component = new List<int>();
                DFS(node, component);
            }
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

                List<int> children = nodeData.Split(' ').Select(int.Parse).ToList();
                result[i] = children;
            }

            return result;
        }

        private static void DFS(int node, List<int> component)
        {
            if (component.Contains(node))
            {
                return;
            }

            component.Add(node);
            foreach (int child in graph[node])
            {
                DFS(child, component);
                if (graph[child].Count == 0)
                {
                    Console.WriteLine(string.Join(" ", component));
                }

                int last = component.Last();
                component.Remove(last);
            }
        }
    }
}
