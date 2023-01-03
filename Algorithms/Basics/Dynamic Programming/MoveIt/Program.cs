namespace Move_Down_Right_Sum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            var matrix = new int[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                var rowElements = Console.ReadLine()
                     .Split()
                     .Select(int.Parse)
                     .ToArray();
                for (int c = 0; c < rowElements.Length; c++)
                {
                    matrix[r, c] = rowElements[c];
                }
            }
            var dynamicProgramming = new int[rows, cols];

            dynamicProgramming[0, 0] = matrix[0, 0];

            for (int c = 1; c < cols; c++)
            {
                dynamicProgramming[0, c] = dynamicProgramming[0, c - 1] + matrix[0, c];
            }
            for (int r = 1; r < rows; r++)
            {
                dynamicProgramming[r, 0] = dynamicProgramming[r - 1, 0] + matrix[r, 0];
            }
            for (int r = 1; r < rows; r++)
            {
                for (int c = 1; c < cols; c++)
                {
                    var upper = dynamicProgramming[r - 1, c];
                    var left = dynamicProgramming[r, c - 1];

                    dynamicProgramming[r, c] = Math.Max(left, upper) + matrix[r, c];
                }
            }
            var path = new Stack<string>();
            var row = rows - 1;
            var col = cols - 1;

            while (row > 0 && col > 0)
            {
                path.Push($"[{row},{col}]");
                var upper = dynamicProgramming[row - 1, col];
                var left = dynamicProgramming[row, col - 1];

                if (upper > left)
                {
                    row--;
                }
                else
                {
                    col--;
                }
            }
            while (row>0)
            {
                path.Push($"[{row},{col}]");
                row--;
            }
            while (col>0)
            {
                path.Push($"[{row},{col}]");
                col--;
            }
            path.Push($"[{row},{col}]");

            Console.WriteLine(string.Join(" ",path));
        }
    }
}