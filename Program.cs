namespace SourceRemovalAlgorithm
{
    internal class Program
    {
        private static Dictionary<string, List<string>> graph;
        private static Dictionary<string, int> dependencies;

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            graph = ReadGraph(n);

            dependencies = ExtractDependencies(graph);

            var sorted = new List<string>();    
            while (dependencies.Count>0)
            {
                var nodeToRemove = dependencies.FirstOrDefault(d => d.Value == 0).Key;

                if (nodeToRemove == null)
                {
                    break;
                }
                dependencies.Remove(nodeToRemove);
                sorted.Add(nodeToRemove);

                foreach (var child in graph[nodeToRemove])
                {
                    dependencies[child] -= 1;
                }
            }
            if (dependencies.Count==0)
            {
                Console.WriteLine($"Topological sorting: {string.Join(", ", sorted)}");
            }
            else
            {
                Console.WriteLine("Invalid logical sorting");
            }
        }

        private static Dictionary<string, int> ExtractDependencies(Dictionary<string, List<string>> _graph)
        {
            var result = new Dictionary<string,int>();
            foreach (var kvp in _graph)
            {
                var node = kvp.Key;
                var children = kvp.Value;

                if (!result.ContainsKey(node)) 
                {
                    result[node] = 0;
                }
                foreach (var child in children)
                {
                    if (!result.ContainsKey(child))
                    {
                        result[child] = 1;
                    }
                    else
                    {
                        result[child] += 1;
                    }
                }
            }
            return result;
        }

        private static Dictionary<string, List<string>> ReadGraph(int n)
        {
            var result  = new Dictionary<string, List<string>>();

            for (int i = 0; i < n; i++)
            {
                var parts = Console.ReadLine()
                    .Split("->", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();
                var key = parts[0];

                if (parts.Length==1)
                {
                    result[key] = new List<string>();
                }
                else
                {
                    var children = parts[1].Split(", ").ToList();

                    result[key] = children;
                }
            }
            return result;
        }
    }
}