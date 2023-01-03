namespace DividingPresents
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var allSums = AllSums(nums);
            var sum = nums.Sum();
            var alan = sum / 2;

            while (true)
            {
                if (allSums.ContainsKey(alan))
                {
                    var alanPresents = FindSubsets(allSums,alan);

                    Console.WriteLine($"Difference: {Math.Abs((sum - alan) - alan)}");
                    Console.WriteLine($"Alan:{alan} Bob:{sum - alan}");
                    Console.WriteLine($"Alan takes: {string.Join(" ",alanPresents)}");
                    Console.WriteLine("Bob takes the rest.");

                    break;
                }
                alan--;
            }
        }

        private static List<int> FindSubsets(Dictionary<int, int> sums, int target)
        {
            var subset = new List<int>();

            while (target!=0)
            {
                var element = sums[target];
                subset.Add(element);

                target -= element;
            }

            return subset;
        }

        private static Dictionary<int, int> AllSums(int[] nums)
        {
            var result = new Dictionary<int, int> { { 0, 0 } };

            foreach (int num in nums)
            {
                var currentSums = result.Keys.ToArray();

                foreach (var sum in currentSums)
                {
                    var newSum = sum + num;
                    if (result.ContainsKey(newSum))
                    {
                        continue;
                    }
                    result.Add(newSum, num);
                }
            }

            return result;
        }
    }
}