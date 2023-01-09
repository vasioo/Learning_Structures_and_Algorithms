namespace Kruskal
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static List<Edge> edges;

        static void Main(string[] args)
        {
            var e = int.Parse(Console.ReadLine());

            edges = ReadEdges(e);

            var sortedEdges = edges.OrderBy(e => e.Weight).ToList();

            //using union to add the second elements in the first collection to compare
            var nodes = edges.Select(e => e.First)
                 .Union(edges.Select(e => e.Second))
                 .ToHashSet();

            var parents = new int[nodes.Max() + 1];

            foreach (var node in nodes)
            {
                parents[node] = node;
            }

            foreach (var tree in sortedEdges)
            {

                var firstNodeRoot = GetRoot(parents, tree.First);
                var secondNodeRoot = GetRoot(parents, tree.Second);

                if (firstNodeRoot == secondNodeRoot)
                {
                    continue;
                }

                Console.WriteLine($"{tree.First} - {tree.Second}");

                //we do this so we dont get a loop
                parents[firstNodeRoot] = secondNodeRoot;
            }
        }

        private static int GetRoot(int[] parents, int node)
        {
            while (node != parents[node])
            {
                node = parents[node];
            }
            return node;
        }

        private static List<Edge> ReadEdges(int e)
        {
            var list = new List<Edge>();

            for (int i = 0; i < e; i++)
            {
                var data = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                var firstNode = data[0];
                var secondNode = data[1];
                var weight = data[2];

                list.Add(new Edge
                {
                    First = firstNode,
                    Second = secondNode,
                    Weight = weight
                });
            }
            return edges;
        }
    }
}