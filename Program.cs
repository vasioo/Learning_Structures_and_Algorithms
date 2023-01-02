namespace DistanceBetweenVerticles
{
    internal class Program
    {
        private static Dictionary<int, List<int>> mainGraph;
        private static Dictionary<int, List<int>> connections;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int p = int.Parse(Console.ReadLine());

            mainGraph = new Dictionary<int, List<int>>();
            connections = new Dictionary<int, List<int>>();

            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split(':', StringSplitOptions.RemoveEmptyEntries);

                var parent = int.Parse(input[0]);
                if (input.Length == 1)
                {
                    mainGraph[parent] = new List<int>();
                }
                else
                {
                    var children = input[1].Split().Select(int.Parse).ToList();
                    mainGraph[parent] = children;
                }
            }

            for (int i = 0; i < p; i++)
            {
                int[] s = Console.ReadLine().Split("-").Select(int.Parse).ToArray();
                int start = s[0];
                int destination = s[1];

                Console.WriteLine($"{start},{destination} -> {ShortestDistance(start, destination)}");
            }
        }
        //BFS
        private static int ShortestDistance(int start, int destination)
        {
            var queue = new Queue<int>();
            queue.Enqueue(start);
            var visited = new HashSet<int>();
            var parent = new Dictionary<int, int> { { start, -1 } };
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == destination)
                {
                    return Steps(parent,destination);
                }

                foreach (var child in mainGraph[node])
                {
                    if (visited.Contains(child))
                    {
                        continue;
                    }
                    visited.Add(child);
                    queue.Enqueue(child);
                    parent[child] = node;
                }
            }

            return -1;
        }

        private static int Steps(Dictionary<int, int> parent, int destination)
        {
            var steps = -1;
            var node = destination;
            while (node!=-1)
            {
                node = parent[node];
                steps++;
            }
            return steps;
        }
    }
}