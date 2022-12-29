namespace ConnectedAreasInMatrix
{
    internal class Program
    {
        private static char[,] matrix;
        private static int size = 0;

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            matrix = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string elements = Console.ReadLine();

                for (int c = 0; c < cols; c++)
                {
                    matrix[i, c] = elements[c];
                }
            }
            var areas = new List<Area>();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    size = 0;
                    ConnectedAreasInMatrix(r, c);
                    if (size != 0)
                    {
                        areas.Add(new Area
                        {
                            Row = r,
                            Col = c,
                            Size = size
                        });
                    }
                }
            }

            var sorted 
                = areas.OrderByDescending(a => a.Size)
                .ThenBy(a => a.Row)
                .ThenBy(a => a.Col)
                .ToList();

            Console.WriteLine($"Total areas found: {areas.Count}");
            for (int i = 0; i < sorted.Count; i++)
            {
                var area = sorted[i];
                Console.WriteLine($"Area #{i+1} at ({area.Row}, {area.Col}), size: {area.Size}");
            }
        }

        //this method explores all of the matrix
        private static void ConnectedAreasInMatrix(int row, int col)
        {
            if (IsOutsideOfMatrix(row, col) || IsWall(row, col) || IsVisited(row, col))
            {
                return;
            }

            size += 1;
            matrix[row, col] = 'v';

            //using backtracking to find all the elements that do not touch any walls using recursion
            ConnectedAreasInMatrix(row - 1, col);//Up
            ConnectedAreasInMatrix(row + 1, col);//Down
            ConnectedAreasInMatrix(row, col - 1);//Left
            ConnectedAreasInMatrix(row, col + 1);//Right
        }

        private static bool IsVisited(int row, int col)
        {
            return matrix[row, col] == 'v';
        }

        private static bool IsWall(int row, int col)
        {
            return matrix[row, col] == '*';
        }

        private static bool IsOutsideOfMatrix(int row, int col)
        {
            return row < 0 || col < 0 || row >= matrix.GetLength(0) || col >= matrix.GetLength(1);
        }
    }
}