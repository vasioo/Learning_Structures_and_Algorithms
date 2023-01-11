namespace ArticulationPoints
{
    internal class Program
    {
        private static List<int>[] graph;
        private static int[] depths;
        private static int[] lowPoints;
        private static int[] parent;
        private static bool[] visited;
        private static List<int> articulationPoints;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int linesCount = int.Parse(Console.ReadLine());

            graph = ReadGraph(nodesCount, linesCount);
            depths = new int[nodesCount];
            lowPoints = new int[nodesCount];
            parent = new int[nodesCount];
            visited = new bool[nodesCount];
            articulationPoints = new List<int>();
            Array.Fill(parent, -1);

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    FinddArticulationPoint(node, 1);
                }
            }
            Console.WriteLine(string.Join(", ", articulationPoints));
        }

        private static void FinddArticulationPoint(int node, int depth)
        {
            visited[node] = true;
            lowPoints[node] = depth;
            depths[node] = depth;

            var childCount = 0;
            var isArticulationPoint = false;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    parent[child] = node;
                    FinddArticulationPoint(child, depth + 1);
                    childCount++;

                    if (lowPoints[child] >= depth)
                    {
                        isArticulationPoint = true;
                    }

                    lowPoints[node] = Math.Min(lowPoints[node], lowPoints[child]);
                }

                else if (parent[node] != child)
                {
                    lowPoints[node] = Math.Min(lowPoints[node], depths[child]);
                }
            }

            if (parent[node] == -1 && childCount > 1
                || parent[node] != -1 && isArticulationPoint)
            {
                articulationPoints.Add(node);
            }
        }

        private static List<int>[] ReadGraph(int nodesCount, int linesCount)
        {
            var result = new List<int>[nodesCount];

            for (int node = 0; node < result.Length; node++)
            {
                result[node] = new List<int>();
            }

            for (int i = 0; i < linesCount; i++)
            {
                var parts = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                var first = parts[0];

                for (int j = 1; j < parts.Length; j++)
                {
                    var second = parts[j];
                    result[first].Add(second);
                    result[second].Add(first);
                }
            }
            return result;
        }
    }
}