namespace LongestCommonSubsequence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input1 = Console.ReadLine();
            string input2 = Console.ReadLine();

            var matrix = new int[input1.Length + 1, input2.Length + 1];

            for (int r = 1; r < matrix.GetLength(0); r++)
            {
                for (int c = 1; c < matrix.GetLength(1); c++)
                {
                    if (input1[r - 1] == input2[c - 1])
                    {
                        matrix[r, c] = matrix[r - 1, c - 1] + 1;
                    }
                    else
                    {
                        matrix[r,c] = Math.Max(matrix[r, c - 1], matrix[r-1,c]);
                    }
                }
            }
            Console.WriteLine(matrix[input1.Length,input2.Length]);
        }
    }
}