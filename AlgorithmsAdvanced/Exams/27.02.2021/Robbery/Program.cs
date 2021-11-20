using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Robbery
{
    class Program
    {
        private static List<Edge>[] graph;
        private static bool[] cameras;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());
            graph = ReadGraph(nodesCount, edgesCount);
            cameras = ReadCameras(nodesCount);
            int startNode = int.Parse(Console.ReadLine());
            int endNode = int.Parse(Console.ReadLine());
            double[] distances = new double[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                distances[i] = double.PositiveInfinity;
            }

            var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
            distances[startNode] = 0;
            queue.Add(startNode);

            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();

                if (node == endNode)
                {
                    break;
                }

                foreach (var edge in graph[node])
                {
                    var child = edge.First == node ? edge.Second : edge.First;

                    if (cameras[child])
                    {
                        continue;
                    }

                    if (double.IsPositiveInfinity(distances[child]))
                    {
                        queue.Add(child);
                    }

                    var newDistance = distances[node] + edge.Weight;

                    if (newDistance < distances[child])
                    {
                        distances[child] = newDistance;
                        queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
                    }
                }
            }

            Console.WriteLine(distances[endNode]);
        }

        private static bool[] ReadCameras(int nodesCount)
        {
            bool[] result = new bool[nodesCount];
            string[] camerasData = Console.ReadLine().Split();

            for (int node = 0; node < camerasData.Length; node++)
            {
                char blackOrWhite = camerasData[node][1];

                if (blackOrWhite == 'b')
                {
                    result[node] = false;
                }
                else
                {
                    result[node] = true;
                }
            }

            return result;
        }
        
        private static List<Edge>[] ReadGraph(int nodesCount, int edgesCount)
        {
            List<Edge>[] result = new List<Edge>[nodesCount];

            for (int node = 0; node < result.Length; node++)
            {
                result[node] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeData = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int first = edgeData[0];
                int second = edgeData[1];
                int weight = edgeData[2];
                
                Edge edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };

                result[first].Add(edge);
                result[second].Add(edge);
            }

            return result;
        }
    }

    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }
}
