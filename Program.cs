namespace CheapTownTour
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

    internal class Program
    {
        private static List<Edge> graph;

        //using Kruskal
        static void Main(string[] args)
        {
            int numberOfTowns = int.Parse(Console.ReadLine());
            int numberOfStreets = int.Parse(Console.ReadLine());

            var result = new List<Edge>();

            for (int i = 0; i < numberOfStreets; i++)
            {
                var street = Console.ReadLine()
                    .Split(" - ")
                    .Select(int.Parse)
                    .ToArray();

                var first = street[0];
                var second = street[1];
                var weigth = street[2];

                result.Add(new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weigth,
                });
            }
            graph = result;

            var sortedEdges = graph
                .OrderBy(e => e.Weight)
                .ToList();

            var root = new int[numberOfTowns];

            for (int i = 0; i < numberOfTowns; i++)
            {
                root[i] = i;
            }
            var totalCost = 0;

            foreach (var edge in sortedEdges)
            {
                var firstRoot = GetRoot(edge.First, root);
                var secondRoot = GetRoot(edge.Second, root);

                if (firstRoot != secondRoot)
                {
                    root[firstRoot] = secondRoot;
                    totalCost += edge.Weight;
                }
            }

            Console.WriteLine(totalCost);
        }

        private static int GetRoot(int node, int[] root)
        {
            while (node != root[node])
            {
                node = root[node];
            }
            return node;
        }
    }
}