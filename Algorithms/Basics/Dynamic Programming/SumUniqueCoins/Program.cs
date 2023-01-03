namespace SumWithLimitedCoins
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int targetSum = int.Parse(Console.ReadLine());

            Console.WriteLine(CountTheSums(nums, targetSum));
        }

        private static int CountTheSums(int[] nums, int targetSum)
        {
            int counter = 0;
            var sums = new HashSet<int> { 0 };

            //adding all unique sums and counting them
            foreach (var num in nums)
            {
                var newSums = new HashSet<int> ();
                foreach (var sum in sums)
                {
                    var newSum = sum + num;

                    if (newSum==targetSum)
                    {
                        counter++;
                    }
                    newSums.Add(newSum);
                }
                sums.UnionWith(newSums);
            }
            return counter;
        }
    }
}