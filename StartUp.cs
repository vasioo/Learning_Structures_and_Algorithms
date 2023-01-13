namespace LongestZigzagSubsequence
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var dp = new int[2, numbers.Length];

            dp[0, 0] = 1;
            dp[1, 0] = 1;

            var parent = new int[2, numbers.Length];

            parent[0, 0] = -1;
            parent[1, 0] = -1;

            var bestSize = 0;
            var lastRow = 0;
            var lastCol = 0;

            for (int current = 0; current < numbers.Length; current++)
            {
                var currentNumber = numbers[current];

                for (int prev = current - 1; prev >= 0; prev--)
                {
                    var prevNumber = numbers[prev];

                    if (currentNumber > prevNumber
                        && dp[1, prev] + 1 >= dp[0, current])
                    {
                        dp[0, current] = dp[1, prev] + 1;
                        parent[0, current] = prev;
                    }

                    if (currentNumber < prevNumber
                        && dp[0, prev] +1>= dp[1, current])
                    {
                        dp[1, current] = dp[0, prev] + 1;
                        parent[1, current] = prev;
                    }
                }
                if (dp[0, current] > bestSize)
                {
                    bestSize = dp[0, current];
                    lastRow = 0;
                    lastCol = current;
                }
                if (dp[1, current] > bestSize)
                {
                    bestSize = dp[1, current];
                    lastRow = 1;
                    lastCol = current;
                }
            }

            var sequence = new Stack<int>();
            while (lastCol != -1)
            {
                sequence.Push(numbers[lastCol]);

                lastCol = parent[lastRow, lastCol];

                if (lastRow == 0)
                {
                    lastRow = 1;
                }
                else
                {
                    lastRow = 0;
                }
            }
            Console.WriteLine(string.Join(" ", sequence));
        }
    }
}