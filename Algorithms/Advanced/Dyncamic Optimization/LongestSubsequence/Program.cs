namespace Longest_IncreasingSubsequence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var lenght = new int[numbers.Length];
            var prev = new int[numbers.Length];

            var bestLength = 0;
            var lastIndex = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                prev[i] = -1;

                var currentNumber = numbers[i];
                var currentBestSequence = 1;

                for (int j = i - 1; j >= 0; j--)
                {
                    var prevNumber = numbers[j];

                    if (prevNumber < currentNumber
                        && lenght[j] + 1 >= currentBestSequence)
                    {
                        currentBestSequence = lenght[j] + 1;
                        prev[i] = j;
                    }
                }

                if (currentBestSequence > bestLength)
                {
                    bestLength = currentBestSequence;
                    lastIndex = i;
                }

                lenght[i] = currentBestSequence;
            }
            Console.WriteLine("Best sequence of numbers:");

            while (lastIndex != -1)
            {
                Console.WriteLine(numbers[lastIndex]);
                lastIndex = prev[lastIndex];
            }
        }
    }
}