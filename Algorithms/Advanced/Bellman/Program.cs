namespace BellmanFord
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    internal class Program
    {
        private static List<Edge> edges;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            edges = new List<Edge>();

            for (int i = 0; i < edgesCount; i++)
            {
                var data = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

                var from = data[0];
                var to = data[1];
                var weight = data[2];

                edges.Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var distance = new double[nodes + 1];
            Array.Fill(distance, double.PositiveInfinity);

            distance[source] = 0;
            var prev = new int[nodes + 1];
            Array.Fill(prev, -1);

            for (int i = 0; i < nodes - 1; i++)
            {
                var updated = false;
                foreach (var edge in edges)
                {
                    if (double.IsPositiveInfinity(edge.From))
                    {
                        continue;
                    }

                    var newDistance = distance[edge.From] + edge.Weight;

                    if (newDistance < distance[edge.To])
                    {
                        distance[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                        updated = true;
                    }
                }
                if (!updated)
                {
                    break;
                }
                foreach (var edge in edges)
                {
                    if (double.IsPositiveInfinity(edge.From))
                    {
                        continue;
                    }

                    var newDistance = distance[edge.From] + edge.Weight;

                    if (newDistance < distance[edge.To])
                    {
                        Console.WriteLine("Negative Cycle Detected");
                        return;
                    }
                }
            }
            var path = new Stack<int>();
            var node = destination;

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }

            Console.WriteLine(string.Join(" ", path));
            Console.WriteLine(distance[destination]);
        }
    }
}