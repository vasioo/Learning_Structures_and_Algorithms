using Wintellect.PowerCollections;

namespace Most_Reliable_Path
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{this.First} {this.Second} {this.Weight}";
        }
    }

    //using Dijkstra
    internal class Program
    {
        private static List<Edge>[] graph;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            var result = new List<Edge>[nodesCount];


            for (int i = 0; i < nodesCount; i++)
            {
                result[i] = new List<Edge>();
            }


            for (int i = 0; i < edgesCount; i++)
            {
                var node = Console.ReadLine()
                    .Split(" - ")
                    .Select(int.Parse)
                    .ToArray();

                var first = node[0];
                var second = node[1];
                var weight = node[2];

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };
                result[first].Add(edge);
                result[second].Add(edge);
            }
            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var distances = new double[nodesCount];
            var prev = new int[nodesCount];

            Array.Fill(distances, double.NegativeInfinity);
            Array.Fill(prev, -1);

            distances[source] = 100;

            var queue = new OrderedBag<int>(
                Comparer<int>.Create
                    ((f, s) => distances[s].CompareTo(distances[f])));

            queue.Add(source);

            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();

                if (node == destination)
                {
                    break;
                }

                var children = graph[node];

                foreach (var edge in children)
                {
                    var childNode = edge.First == node ? edge.Second : edge.First;

                    if (double.IsNegativeInfinity(distances[childNode]))
                    {
                        queue.Add(childNode);
                    }

                    var newDistance = distances[node] * edge.Weight / 100.0;

                    if (newDistance > distances[childNode])
                    {
                        distances[childNode] = newDistance;
                        prev[childNode] = node;

                        queue = new OrderedBag<int>(queue,
                             Comparer<int>.Create
                                 ((f, s) => distances[s].CompareTo(distances[f])));
                    }
                }
            }
            Console.WriteLine(Math.Round(distances[destination], 2));
            var path = GetPath(prev, destination);

            Console.WriteLine(string.Join("->", path));
        }

        private static Stack<int> GetPath(int[] prev, int destination)
        {
            var result = new Stack<int>();

            while (destination != -1)
            {
                result.Push(destination);
                destination = prev[destination];
            }

            return result;
        }
    }
}