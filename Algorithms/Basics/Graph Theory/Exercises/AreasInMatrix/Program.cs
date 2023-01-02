namespace AreasInMatrix
{
    internal class Program
    {
        private static char[,] matrix;
        private static bool[,] visited;
        private static Dictionary<char, int> areas;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());

            matrix = new char[n, m];
            visited = new bool[n, m];
            areas = new Dictionary<char, int>();

            for (int row = 0; row < n; row++)
            {
                string input = Console.ReadLine();

                List<char> elements = new List<char>(input.ToCharArray());

                for (int col = 0; col < m; col++)
                {
                    matrix[row, col] = elements[col];
                }
            }

            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < m; col++)
                {
                    if (visited[row, col])
                    {
                        continue;
                    }
                    var parentNode = matrix[row, col];
                    DFS(row, col, parentNode);

                    if (areas.ContainsKey(parentNode))
                    {
                        areas[parentNode]++;
                    }
                    else
                    {
                        areas[parentNode] = 1;
                    }
                }
            }
            int totalCounter = 0;
            foreach (var area in areas)
            {
                totalCounter += area.Value;
            }
            Console.WriteLine($"Areas: {totalCounter}");
            foreach (var area in areas)
            {
                Console.WriteLine($"Letter '{area.Key}' -> {area.Value}");
            }
        }
        private static void DFS(int row, int col, char parentNode)
        {
            if (row < 0 || row >= matrix.GetLength(0) 
                || col < 0 || col >= matrix.GetLength(1))
            {
                return;
            }
            if (visited[row, col])
            {
                return;
            }
            if (matrix[row, col] != parentNode)
            {
                return;
            }

            visited[row,col] = true;
            DFS(row, col - 1, parentNode);
            DFS(row, col + 1, parentNode);
            DFS(row - 1, col, parentNode);
            DFS(row + 1, col, parentNode);
        }
    }
}