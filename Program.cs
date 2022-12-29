namespace Labyrinth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            var lab = new char[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                var colElements = Console.ReadLine();
                for (int c = 0; c < cols; c++)
                {
                    lab[r, c] = colElements[c];
                }
            }
            FindPaths(lab, 0, 0, new List<string>(), string.Empty);
        }

        private static void FindPaths(char[,] lab, int row, int col, List<string> directions, string direction)
        {
            //validation
            if (row < 0 || row >= lab.GetLength(0) || col < 0 || col >= lab.GetLength(1))
            {
                return;
            }
            //if this symbol is seen the process stops or the v is seen it has been visited
            if (lab[row, col] == '*' || lab[row,col]=='v')
            {
                return;
            }
            //if reaching the end we remove the combination 
            if (lab[row, col] == 'e')
            {
                Console.WriteLine(string.Join(' ', directions));
                directions.RemoveAt(directions.Count - 1);
                return;
            }
            lab[row, col] = 'v';
            directions.Add(direction);
            FindPaths(lab, row - 1, col, directions, "U");//up
            FindPaths(lab, row + 1, col, directions, "D");//down
            FindPaths(lab, row, col - 1, directions, "L");//left
            FindPaths(lab, row, col + 1, directions, "R");//right

            lab[row, col] = '-';
            directions.RemoveAt(directions.Count - 1);
        }
    }
}