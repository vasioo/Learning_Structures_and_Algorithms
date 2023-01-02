namespace RoadReconstruction
{
    internal class Program
    {
        private static List<int>[] graph;
        private static List<Edge> edges;
        private static bool[] visited;

        static void Main(string[] args)
        {
            int numberOfBuildings = int.Parse(Console.ReadLine());
            int numberOfStreets = int.Parse(Console.ReadLine());

            graph = new List<int>[numberOfBuildings];
            edges = new List<Edge>();
            
            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<int>();
            }
            for (int i = 0; i < numberOfStreets; i++)
            {
                var input = Console.ReadLine()
                    .Split(" - ")
                    .Select(int.Parse)
                    .ToArray();

                var first = input[0];
                var second = input[1];

                graph[first].Add(second);
                graph[second].Add(first);

                edges.Add(new Edge
                {
                    First = first,
                    Second = second
                });
            }
            Console.WriteLine("Important streets: ");
            foreach (var edge in edges)
            {
                graph[edge.First].Remove(edge.Second);
                graph[edge.Second].Remove(edge.First);

                visited = new bool[graph.Length];

                DFS(0);

                if (visited.Contains(false))
                {
                    Console.WriteLine(edge);
                }

                graph[edge.First].Add(edge.Second);
                graph[edge.Second].Add(edge.First);
            }      
        }

        private static void DFS(int node)
        {
            if (visited[node])
            {
                return;
            }
            visited[node] = true;
            foreach (var child in graph[node])
            {
                DFS(child);
            }
        }
    }
}