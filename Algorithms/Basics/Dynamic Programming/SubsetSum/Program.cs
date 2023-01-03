namespace SubsetSum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] nums = new[] { 3, 5, 2 };
            int targetSum = 54;

            var sums = new bool[targetSum + 1];
            sums[0] = true;

            for (int sum = 0; sum < sums.Length; sum++)
            {
                if (!sums[sum])
                {
                    continue;
                }
                foreach (var num in nums)
                {
                    var newSum = sum + num;

                    if (newSum > targetSum)
                    {
                        continue;
                    }
                    sums[newSum] = true;
                }
            }
            var subset = new List<int>();
            while (targetSum > 0)
            {
                foreach (var num in nums)
                {
                    var preSum = targetSum - num;
                    if (preSum >= 0 && sums[preSum])
                    {
                        subset.Add(num);
                        targetSum = preSum;
                        if (targetSum==0)
                        {
                            break;
                        }
                    }
                }
            }
            Console.WriteLine(string.Join(", ",subset));
        }
    }
}