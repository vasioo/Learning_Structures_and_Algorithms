namespace LongestStringChain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var words = Console.ReadLine()
                .Split();

            var length = new int[words.Length];
            var parent = new int[words.Length];
            var maxStringChain = 0;
            var lastIndex = 0;

            for (int current = 0; current < words.Length; current++)
            {
                length[current] = 1;
                parent[current] = -1;

                var currentWord = words[current];

                for (int prev = current - 1; prev >= 0; prev--)
                {
                    var prevWord = words[prev];

                    if (currentWord.Length > prevWord.Length
                        && length[prev] + 1 > length[current])
                    {
                        length[current] = length[prev] + 1;
                        parent[current] = prev;
                    }
                }
                if (length[current] > maxStringChain)
                {
                    lastIndex = current;
                    maxStringChain = length[current];
                }
            }

            while (lastIndex != -1)
            {
                Console.WriteLine(words[lastIndex]);
                lastIndex = parent[lastIndex];
            }
        }
    }
}