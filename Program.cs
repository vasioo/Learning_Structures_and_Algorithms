namespace AcyclicGraphs
{
    internal class Program
    {
        private static Dictionary<string, List<string>> graph;
        private static HashSet<string> visited;
        private static HashSet<string> isAcyclic;

        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split("-");

            graph = new Dictionary<string, List<string>>();
            visited = new HashSet<string>();
            isAcyclic = new HashSet<string>();
            while (input[0] != "End")
            {
                var from = input[0];
                var to = input[1];

                if (!graph.ContainsKey(from))
                {
                    graph.Add(from, new List<string>());
                }

                if (!graph.ContainsKey(to))
                {
                    graph.Add(to, new List<string>());
                }

                graph[from].Add(to);
                input = Console.ReadLine().Split("-");
            }
            try
            {
                foreach (var node in graph.Keys)
                {
                    DFS(node);
                }
                Console.WriteLine($"Acyclic: Yes");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Acyclic: No");
                throw;
            }
        }

        private static void DFS(string node)
        {
            if (isAcyclic.Contains(node))
            {
                throw new ArgumentException("A result cannot be returned");
            }
            if (visited.Contains(node))
            {
                return;
            }

            visited.Add(node);
            isAcyclic.Add(node);

            foreach (var child in graph[node])
            {
                DFS(child);
            }
            isAcyclic.Remove(node);
        }
    }
}