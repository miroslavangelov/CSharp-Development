using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace EmergencyPlan
{
    class Program
    {
        private static Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>();
        private static int[] exitRooms;
        private static int nodesCount;
        private static int edgesCount;

        static void Main(string[] args)
        {
            nodesCount = int.Parse(Console.ReadLine());
            exitRooms = Console.ReadLine().Split().Select(int.Parse).ToArray();
            edgesCount = int.Parse(Console.ReadLine());

            ReadGraph(edgesCount);
            
            string[] inputTime = Console.ReadLine().Split(":", StringSplitOptions.RemoveEmptyEntries);
            int timeToEvacuate = int.Parse(inputTime[0]) * 60 + int.Parse(inputTime[1]);

            for (int node = 0; node < nodesCount; node++)
            {
                if (exitRooms.Contains(node))
                {
                    continue;
                }

                if (!graph.ContainsKey(node))
                {
                    Console.WriteLine($"Unreachable {node} (N/A)");
                    continue;
                }

                int bestTime = int.MaxValue;
                for (int j = 0; j < exitRooms.Length; j++)
                {
                    int currentExitRoom = exitRooms[j];
                    int currentTIme = dijkstraAlgorithm(node, currentExitRoom);

                    if (currentTIme < bestTime)
                    {
                        bestTime = currentTIme;
                    }
                }

                if (bestTime > timeToEvacuate)
                {
                    int hours = bestTime / 3600;
                    int mins = (bestTime % 3600) / 60;
                    bestTime = bestTime % 60;
                    Console.WriteLine($"Unsafe {node} ({hours:D2}:{mins:D2}:{bestTime:D2})");
                }
                else
                {
                    int hours = bestTime / 3600;
                    int mins = (bestTime % 3600) / 60;
                    bestTime = bestTime % 60;
                    Console.WriteLine($"Safe {node} ({hours:D2}:{mins:D2}:{bestTime:D2})");
                }
            }
        }
        
        private static int dijkstraAlgorithm(int startNode, int endNode) {
            int[] distance = new int[edgesCount + 1];
            int[] prev = new int[nodesCount + 1];

            for (int i = 0; i < distance.Length; i++) {
                distance[i] = int.MaxValue;
                prev[i] = -1;
            }
            for (int i = 0; i < prev.Length; i++) {
                prev[i] = -1;
            }
            distance[startNode] = 0;
            var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => distance[f] - distance[s]));
            queue.Add(startNode);

            while (queue.Count > 0)
            {
                var minNode = queue.RemoveFirst();
                var children = graph[minNode];

                if (exitRooms.Contains(minNode))
                {
                    break;
                }

                foreach (var child in children)
                {
                    var childNode = child.First == minNode ? child.Second : child.First;

                    if (distance[childNode] == int.MaxValue)
                    {
                        queue.Add(childNode);
                    }

                    var newDistance = child.Weight + distance[minNode];

                    if (newDistance < distance[childNode])
                    {
                        distance[childNode] = newDistance;
                        prev[childNode] = minNode;
                        queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => distance[f] - distance[s]));
                    }
                }
            }

            if (prev[endNode] == -1) {
                return 0;
            }

            return distance[endNode];
        }

        private static void ReadGraph(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                string[] edgeParts = Console.ReadLine().Split();
                string[] inputTime = edgeParts[2].Split(":", StringSplitOptions.RemoveEmptyEntries);
                int from = int.Parse(edgeParts[0]);
                int to = int.Parse(edgeParts[1]);
                int time = int.Parse(inputTime[0]) * 60 + int.Parse(inputTime[1]);

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
        }

        public class Edge
        {
            public int First { get; set; }

            public int Second { get; set; }

            public int Weight { get; set; }
        }
    }
}