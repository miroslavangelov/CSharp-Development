using System.Linq;

namespace BlackFriday
{
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;

    class Program
    {
        private static Dictionary<int, List<Edge>> graph;
        private static HashSet<int> spanningTree;
        private static int result;

        static void Main(string[] args)
        {
            int shops = int.Parse(Console.ReadLine());
            int streets = int.Parse(Console.ReadLine());
            spanningTree = new HashSet<int>();
            graph = new Dictionary<int, List<Edge>>();
            result = 0;

            for (int i = 0; i < streets; i++)
            {
                int[] edgeParts = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                int from = edgeParts[0];
                int to = edgeParts[1];
                int time = edgeParts[2];

                if (!graph.ContainsKey(from))
                {
                    graph.Add(from, new List<Edge>());
                }

                if (!graph.ContainsKey(to))
                {
                    graph.Add(to, new List<Edge>());
                }

                Edge edge = new Edge
                {
                    First = from,
                    Second = to,
                    Weight = time
                };
                graph[from].Add(edge);
                graph[to].Add(edge);
            }

            foreach (var node in graph.Keys)
            {
                if (!spanningTree.Contains(node))
                {
                    Prim(node);
                }
            }

            Console.WriteLine(result);
        }

        private static void Prim(int node)
        {
            spanningTree.Add(node);
            var queue = new OrderedBag<Edge>(graph[node], Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            while (queue.Count > 0)
            {
                Edge min = queue.RemoveFirst();
                int nonTreeNode = -1;

                if (spanningTree.Contains(min.First) && !spanningTree.Contains(min.Second)) {
                    nonTreeNode = min.Second;
                }

                if (!spanningTree.Contains(min.First) && spanningTree.Contains(min.Second)) {
                    nonTreeNode = min.First;
                }

                if (nonTreeNode == -1) {
                    continue;
                }

                result += min.Weight;
                spanningTree.Add(nonTreeNode);
                queue.AddMany(graph[nonTreeNode]);
            }
        }

        public class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public int Weight { get; set; }
        }
    }
}