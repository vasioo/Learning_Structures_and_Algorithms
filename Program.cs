namespace Find_bi_connected_components
{
    internal class Program
    {
        private static List<int>[] graph;
        private static int[] depths;
        private static int[] lowPoints;
        private static int[] parents;
        private static bool[] visited;

        private static Stack<int> stack;

        private static List<HashSet<int>> components;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            graph = ReadGraph(nodesCount, edgesCount);
            depths = new int[nodesCount];
            lowPoints = new int[nodesCount];
            parents = new int[nodesCount];
            Array.Fill(parents, -1);
            visited = new bool[nodesCount];
            stack = new Stack<int>();
            components = new List<HashSet<int>>();


            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    FindArticulationPoints(node, 1);

                    var component = new HashSet<int>();
                    while (stack.Count > 1)
                    {
                        var stackChild = stack.Pop();
                        var stackNode = stack.Pop();

                        component.Add(stackNode);
                        component.Add(stackChild);
                    }
                    components.Add(component);
                }
            }
            foreach (var component in components)
            {
                Console.WriteLine(string.Join(" ",component));
            }
        }

        private static void FindArticulationPoints(int node, int depth)
        {
            visited[node] = true;
            depths[node] = depth;
            lowPoints[node] = depth;

            var childCount = 0;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    stack.Push(node);
                    stack.Push(child);

                    parents[child] = node;
                    childCount++;

                    FindArticulationPoints(child, depth + 1);

                    if ((parents[node] == -1 && childCount > 1)
                        || (parents[node] != -1 && lowPoints[child] >= depth))
                    {
                        var component = new HashSet<int>();
                        while (true)
                        {
                            var stackChild = stack.Pop();
                            var stackNode = stack.Pop();

                            component.Add(stackNode);
                            component.Add(stackChild);

                            if (stackNode == node && stackChild == child)
                            {
                                break;
                            }
                        }
                        components.Add(component);
                    }

                    lowPoints[node] = Math.Min(lowPoints[node], lowPoints[child]);
                }

                else if (parents[node] != child
                    && depths[child] < lowPoints[node])
                {
                    lowPoints[node] = depths[child];

                    stack.Push(node);
                    stack.Push(child);
                }
            }
        }

        private static List<int>[] ReadGraph(int nodesCount, int edgesCount)
        {
            var result = new List<int>[nodesCount];

            for (int node = 0; node < result.Length; node++)
            {
                result[node] = new List<int>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edge = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var first = edge[0];
                var second = edge[1];

                result[first].Add(second);
                result[second].Add(first);
            }
            return result;
        }
    }
}