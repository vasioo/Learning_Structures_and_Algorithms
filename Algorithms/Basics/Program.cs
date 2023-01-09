using Wintellect.PowerCollections;

namespace Dijkstra
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static Dictionary<int, List<Edge>> edgesByNode;

        static void Main(string[] args)
        {
            int e = int.Parse(Console.ReadLine());

            edgesByNode = ReadEdges(e);

            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());

            var maxNode = edgesByNode.Keys.Max();

            var distance = new int[maxNode + 1];
            Array.Fill(distance, int.MaxValue);

            distance[start] = 0;

            var prev = new int[maxNode + 1];
            prev[start] = -1;

            var queue = new OrderedBag<int>
                (Comparer<int>.Create((f, s) => distance[f] - distance[s]));

            queue.Add(start);

            while (queue.Count > 0)
            {
                var minNode = queue.RemoveFirst();
                var children = edgesByNode[minNode];

                if (minNode == end)
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

                        queue = new OrderedBag<int>
                            (queue, Comparer<int>.Create((f, s) => distance[f] - distance[s]));
                    }
                }
            }
            Console.WriteLine(distance[end]);

            var path = new Stack<int>();
            var node = end;

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }
            Console.WriteLine(string.Join(" ", path));
        }

        private static Dictionary<int, List<Edge>> ReadEdges(int e)
        {
            var result = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < e; i++)
            {
                var data = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

                var firstNode = data[0];
                var secondNode = data[1];
                var weight = data[2];

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
                    Weight = weight
                };

                result[firstNode].Add(edge);
                result[secondNode].Add(edge);
            }
            return result;
        }
    }
}