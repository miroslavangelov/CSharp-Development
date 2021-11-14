namespace EmergencyPlan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    public class Program
    {
        private static Dictionary<int, List<Edge>> edgesByNode;
        private static int[] prev;

        static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var ExitRooms = Console.ReadLine()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var edgesCount = int.Parse(Console.ReadLine());

            edgesByNode = ReadGraph(edgesCount);

            var inputTime = Console.ReadLine()
                .Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var timeInSecondsToSafeExit = inputTime[1] + (inputTime[0] * 60);
            var end = ExitRooms;

            for (int i = 0; i < nodesCount; i++)
            {
                if (ExitRooms.Contains(i))
                {
                    continue;
                }

                if (!edgesByNode.ContainsKey(i))
                {
                    Console.WriteLine($"Unreachable {i} (N/A)");
                    continue;
                }

                var start = i;
                var distances = new int[edgesCount + 1];

                for (int j = 0; j < distances.Length; j++)
                {
                    distances[j] = int.MaxValue;
                }

                distances[start] = 0;

                prev = new int[nodesCount + 1];
                for (int j = 0; j < prev.Length; j++)
                {
                    prev[j] = int.MaxValue;
                }

                prev[start] = -1;

                var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                queue.Add(start);

                while (queue.Count > 0)
                {
                    var minNode = queue.RemoveFirst();
                    var children = edgesByNode[minNode];

                    if (end.Contains(minNode))
                    {
                        break;
                    }

                    foreach (var child in children)
                    {
                        var childNode = child.First == minNode ? child.Second : child.First;

                        if (distances[childNode] == int.MaxValue)
                        {
                            queue.Add(childNode);
                        }

                        var newDistance = child.Weight + distances[minNode];

                        if (newDistance < distances[childNode])
                        {
                            distances[childNode] = newDistance;
                            prev[childNode] = minNode;
                            queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                        }
                    }
                }

                var minTimeExit = 0;

                if (end.Length > 1)
                {
                    var timeOfEnds = new List<int>();

                    foreach (var end1 in end)
                    {
                        timeOfEnds.Add(distances[end1]);
                    }
                    minTimeExit = timeOfEnds.Min();
                }
                else
                {
                    minTimeExit = distances[end[0]];
                }

                var isThereExit = false;

                foreach (var end1 in end)
                {
                    isThereExit = prev[end1] != int.MaxValue;

                    if (isThereExit == true)
                    {
                        break;
                    }
                }

                if (minTimeExit < 1)
                {
                    continue;
                }

                if (isThereExit)
                {
                    if (minTimeExit > timeInSecondsToSafeExit)
                    {
                        int hours = minTimeExit / 3600;
                        int mins = (minTimeExit % 3600) / 60;
                        minTimeExit = minTimeExit % 60;

                        Console.WriteLine($"Unsafe {i} ({hours:D2}:{mins:D2}:{minTimeExit:D2})");
                    }
                    else
                    {
                        int hours = minTimeExit / 3600;
                        int mins = (minTimeExit % 3600) / 60;
                        minTimeExit = minTimeExit % 60;

                        Console.WriteLine($"Safe {i} ({hours:D2}:{mins:D2}:{minTimeExit:D2})");
                    }
                }
                else
                {
                    Console.WriteLine($"Unreachable {i} (N/A)");
                }
            }
        }

        private static Dictionary<int, List<Edge>> ReadGraph(int e)
        {
            var result = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < e; i++)
            {
                var edgeData = Console.ReadLine().Split();
                var inputTime = edgeData[2].Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var firstNode = int.Parse(edgeData[0]);
                var secondNode = int.Parse(edgeData[1]);
                var weight = inputTime[1] + (inputTime[0] * 60);

                if (!result.ContainsKey(firstNode))
                {
                    result.Add(firstNode, new List<Edge>());
                }

                if (!result.ContainsKey(secondNode))
                {
                    result.Add(secondNode, new List<Edge>());
                }

                var edge = new Edge
                {
                    First = firstNode,
                    Second = secondNode,
                    Weight = weight,
                };

                result[firstNode].Add(edge);
                result[secondNode].Add(edge);
            }

            return result;
        }
    }
}
