using Wintellect.PowerCollections;

namespace CableNetwork
{
    public class Edge
    {
        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{this.First} {this.Second} {this.Weight}";
        }
    }

    internal class Program
    {
        private static List<Edge>[] graph;
        private static HashSet<int> spanningTree;

        //using Prim 
        static void Main(string[] args)
        {
            int budget = int.Parse(Console.ReadLine());
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            spanningTree = new HashSet<int>();
            var result = new List<Edge>[nodesCount + 1];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var data = Console.ReadLine().Split();

                var first = int.Parse(data[0]);
                var second = int.Parse(data[1]);
                var weigth = int.Parse(data[2]);

                if (data.Length == 4)
                {
                    spanningTree.Add(first);
                    spanningTree.Add(second);
                }

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weigth,
                };
                result[first].Add(edge);
                result[second].Add(edge);
            }
            graph = result;

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

           var usedBudget = Prim(budget);

            Console.WriteLine(usedBudget);
        }

        private static int Prim(int budget)
        {
            var usedBudget = 0;

            var queue = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            foreach (var node in spanningTree)
            {
                queue.AddMany(graph[node]);
            }

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();

                var nonTreeNode = -1;

                if (spanningTree.Contains(edge.First)
                    && !spanningTree.Contains(edge.Second))
                {
                    nonTreeNode = edge.Second;
                }
                else if (!spanningTree.Contains(edge.First)
                    && spanningTree.Contains(edge.Second))
                {
                    nonTreeNode = edge.First;
                }

                if (nonTreeNode == -1)
                {
                    continue;
                }

                if (edge.Weight > budget)
                {
                    break;
                }

                usedBudget += edge.Weight;
                budget -= edge.Weight;

                spanningTree.Add(nonTreeNode);
                queue.AddMany(graph[nonTreeNode]);
            }
            return usedBudget;
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