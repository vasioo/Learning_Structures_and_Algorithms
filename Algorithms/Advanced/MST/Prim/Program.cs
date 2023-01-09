using Wintellect.PowerCollections;

namespace Prim
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static Dictionary<int, List<Edge>> edgesByNodes;
        private static HashSet<int> forest;

        static void Main(string[] args)
        {
            var e = int.Parse(Console.ReadLine());

            edgesByNodes = ReadGraph(e);

            foreach (var node in edgesByNodes.Keys)
            {
                if (!forest.Contains(node))
                {
                    Prim(node);
                }
            }
        }

        private static void Prim(int node)
        {
            forest.Add(node);

            var queue = new OrderedBag<Edge>(
                edgesByNodes[node],
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();
                var nonTreeNode = -1;

                if (forest.Contains(edge.First) && !forest.Contains(edge.Second))
                {
                    nonTreeNode = edge.Second;
                }

                else if (forest.Contains(edge.Second) && !forest.Contains(edge.First))
                {
                    nonTreeNode = edge.First;
                }

                if (nonTreeNode == -1)
                {
                    continue;
                }

                Console.WriteLine($"{edge.First} - {edge.Second}");

                forest.Add(nonTreeNode);
                queue.AddMany(edgesByNodes[nonTreeNode]);
            }
        }

        private static Dictionary<int, List<Edge>> ReadGraph(int e)
        {
            var result = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < e; i++)
            {
                var data = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();


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