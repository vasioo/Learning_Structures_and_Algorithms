namespace LongestPath
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
        private static List<Edge>[] graph;

        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            graph = new List<Edge>[nodes + 1];

            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var data = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

                var from = data[0];
                var to = data[1];
                var weight = data[2];

                graph[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var sortedNodes = TopologicalSorting();

            var distances = new double[graph.Length];
            Array.Fill(distances, double.NegativeInfinity);
            distances[source] = 0;

            var prev = new int[graph.Length];
            Array.Fill(prev, -1);

            //if we want to see the shortest path we have to change
            //negativeInfinity and the > symbol to < in the if check
            while (sortedNodes.Count > 0)
            {
                var node = sortedNodes.Pop();

                foreach (var edge in graph[node])
                {
                    var newDistance = distances[node] + edge.Weight;

                    if (newDistance > distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = node;
                    }
                }
            }
            Console.WriteLine(distances[destination]);
            Console.WriteLine(string.Join(" ", GetPath(prev, destination)));
        }

        private static Stack<int> GetPath(int[] prev, int node)
        {
            var path = new Stack<int>();

            while (node != -1)
            {
                path.Push(node);
                node = prev[node];
            }
            return path;
        }

        private static Stack<int> TopologicalSorting()
        {
            var visited = new bool[graph.Length];
            var sorted = new Stack<int>();

            for (int node = 1; node < graph.Length; node++)
            {
                DFS(node, visited, sorted);
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

            foreach (var edge in graph[node])
            {
                DFS(edge.To, visited, sorted);
            }
            sorted.Push(node);
        }
    }
}