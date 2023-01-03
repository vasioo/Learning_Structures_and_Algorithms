namespace WordDifferences
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s1 = Console.ReadLine();
            string s2 = Console.ReadLine();

            var dp = new int[s1.Length + 1, s2.Length + 1];

            for (int row = 1; row < dp.GetLength(0); row++)
            {
                dp[row, 0] = row;
            } 
            for (int col = 1; col < dp.GetLength(1); col++)
            {
                dp[0, col] = col;
            }
            for (int row = 1; row < dp.GetLength(0); row++)
            {
                for (int col = 1; col < dp.GetLength(1); col++)
                {
                    if (s1[row - 1] == s2[col-1])
                    {
                        dp[row, col] = dp[row - 1, col-1];
                    }
                    else
                    {
                        dp[row, col] = Math.Min(dp[row - 1, col], dp[row,col-1])+1;
                    }
                }
            }
            Console.WriteLine("Deletions and Insertions: "+dp[s1.Length,s2.Length]);
        }
    }
}