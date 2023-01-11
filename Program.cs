namespace MaximumTasksAssignment
{
    internal class Program
    {
        private static int[,] graph;
        private static int[] parents;

        static void Main(string[] args)
        {
            int peopleCount = int.Parse(Console.ReadLine());
            int tasksCount = int.Parse(Console.ReadLine());

            graph = ReadGraph(peopleCount, tasksCount);
            var nodes = graph.GetLength(0);

            parents = new int[nodes];

            Array.Fill(parents, -1);

            var start = 0;
            var target = nodes - 1;

            while (BFS(start, target))
            {
                var node = target;

                while (node != start)
                {
                    var parent = parents[node];

                    graph[parent, node] = 0;
                    graph[node, parent] = 1;

                    node = parent;
                }
            }
            for (int person = 1; person <= peopleCount; person++)
            {
                for (int task = peopleCount + 1; task <= peopleCount + tasksCount; task++)
                {
                    if (graph[task, person] > 0)
                    {
                        Console.WriteLine($"{(char)(64 + person)} - {task - peopleCount}");
                        break;
                        
                        //3
                        //3
                        //YNY
                        //NYY
                        //YYN
                    }
                }
            }
        }

        private static int[,] ReadGraph(int people, int tasks)
        {
            var nodes = people + tasks + 2;
            var result = new int[nodes, nodes];
            var start = 0;
            var target = nodes - 1;

            for (int person = 1; person <= people; person++)
            {
                result[start, person] = 1;
            }
            for (int task = people + 1; task <= people + tasks; task++)
            {
                result[task, target] = 1;
            }

            for (int person = 1; person <= people; person++)
            {
                var personTasks = Console.ReadLine();

                for (int task = 0; task < personTasks.Length; task++)
                {
                    if (personTasks[task] == 'Y')
                    {
                        result[person, people + 1 + task] = 1;
                    }
                }
            }
            return result;

        }

        private static bool BFS(int start, int target)
        {
            var visited = new bool[graph.GetLength(0)];
            var queue = new Queue<int>();

            visited[start] = true;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == target)
                {
                    return true;
                }

                for (int child = 0; child < graph.GetLength(1); child++)
                {
                    if (!visited[child] && graph[node, child] > 0)
                    {
                        parents[child] = node;
                        visited[child] = true;
                        queue.Enqueue(child);
                    }
                }
            }
            return false;
        }
    }
}