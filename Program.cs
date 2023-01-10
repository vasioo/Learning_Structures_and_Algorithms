namespace Undefinded
{
    public class Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{this.From} {this.To} {this.Weight}";
        }
    }

    internal class Program
    {
        private static List<Edge> edges;

        //using Bellman-Ford
        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            var result = new List<Edge>();

            for (int i = 0; i < edgesCount; i++)
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
                    From = first,
                    To = second,
                    Weight = weigth,
                });
            }
            edges = result;

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var distances = new double[nodesCount + 1];
            var prev = new int[nodesCount + 1];

            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = double.PositiveInfinity;
                prev[i] = -1;
            }

            distances[source] = 0;

            for (int i = 0; i < nodesCount - 1; i++)
            {
                var updated = false;
                foreach (var edge in edges)
                {
                    if (double.IsPositiveInfinity(distances[edge.From]))
                    {
                        continue;
                    }

                    var newDistance = distances[edge.From] + edge.Weight;

                    if (newDistance < distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = edge.From;
                        updated = true;
                    }
                }

                if (!updated)
                {
                    break;
                }
            }
            foreach (var edge in edges)
            {
                if (double.IsPositiveInfinity(distances[edge.From]))
                {
                    continue;
                }

                var newDistance = distances[edge.From] + edge.Weight;

                if (newDistance < distances[edge.To])
                {
                    Console.WriteLine("Undefined");
                    return;
                }
            }
            Console.WriteLine(distances[destination]);

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