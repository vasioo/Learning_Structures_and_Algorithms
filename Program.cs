namespace Big_Trip
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
        private static List<Edge>[] edges;
        //using Longest Path Algorihtm
        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            var result = new List<Edge>[nodesCount + 1];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var street = Console.ReadLine()
                    .Split(" - ")
                    .Select(int.Parse)
                    .ToArray();

                var from = street[0];
                var to = street[1];
                var weigth = street[2];

                result[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weigth,
                });
            }
            edges = result;

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var distances = new double[edges.Length];
            var prev = new int[edges.Length];

            Array.Fill(distances, double.NegativeInfinity);
            Array.Fill(prev, -1);

            distances[source] = 0;

            var sortedNodes = TopologicalSort();

            while (sortedNodes.Count > 0)
            {
                var node = sortedNodes.Pop();

                foreach (var edge in edges[node])
                {
                    var newDistance = distances[node] + edge.Weight ;

                    if (newDistance > distances[edge.To])
                    {
                        prev[edge.To] = node;
                        distances[edge.To] = newDistance;
                    }
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

        private static Stack<int> TopologicalSort()
        {
            var visited = new bool[edges.Length];
            var sorted = new Stack<int>();

            for (int i = 1; i < edges.Length; i++)
            {
                DFS(i, visited, sorted);
            }
            return sorted;
        }

        private static void DFS(int node, bool[] visited, Stack<int> sorted)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var edge in edges[node])
            {
                DFS(edge.To, visited, sorted);
            }
            sorted.Push(node);
        }
    }
}