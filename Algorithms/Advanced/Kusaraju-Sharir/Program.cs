namespace Kosaraju_Sharir
{
    class Edges
    {

    }

    internal class Program
    {
        private static List<int>[] graph;
        private static List<int>[] reversedGraph;
        private static Stack<int> sorted;

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int linesCount = int.Parse(Console.ReadLine());

            (graph, reversedGraph)
                = ReadGraph(nodesCount, linesCount);

            sorted = TopologicalSorting();

            var visited = new bool[nodesCount];

            while (sorted.Count > 0)
            {
                var node = sorted.Pop();

                if (visited[node])
                {
                    continue;
                }

                var component = new Stack<int>();

                DFS(reversedGraph,node, visited, component);

                Console.WriteLine($"{{{string.Join(", ", component)}}}");
            }
        }

        private static Stack<int> TopologicalSorting()
        {
            var result = new Stack<int>();
            var visited = new bool[graph.Length];

            for (int i = 0; i < graph.Length; i++)
            {
                DFS(graph, i, visited, result);
            }

            return result;
        }

        private static void DFS(List<int>[] source, int node, bool[] visited, Stack<int> result)
        {
            if (visited[node])
            {
                return;
            }
            visited[node] = true;

            foreach (var child in source[node])
            {
                DFS(source,child, visited, result);
            }
            result.Push(node);
        }

        //reversing it(returns both of them in a tuple)
        private static (List<int>[] original, List<int>[] reversed) ReadGraph(int nodesCount, int linesCount)
        {
            var result = new List<int>[nodesCount];
            var reversed = new List<int>[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                result[i] = new List<int>();
            }

            for (int i = 0; i < linesCount; i++)
            {
                var input = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                var node = input[0];

                for (int j = 1; j < input.Length; j++)
                {
                    var child = input[j];
                    result[i].Add(child);

                    reversed[child].Add(node);
                }
            }
            return (result, reversed);
        }
    }
}