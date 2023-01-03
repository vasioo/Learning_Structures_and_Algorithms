namespace SumOfAmountOfCoins
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int targetSum = int.Parse(Console.ReadLine());

            Console.WriteLine(CountTheSums(nums, targetSum));
            //checkin how many ways it can be solved by making the number smaller as the logic applies forwards
        }

        private static int CountTheSums(int[] nums, int targetSum)
        {
            //always start at 1 because the element 1 can be get to by adding 1

            var sums = new int[targetSum + 1];
            sums[0] = 1;

            foreach (var num in nums)
            {
                for (int sum = num; sum <= targetSum; sum++)
                {
                    sums[sum] += sums[sum - num];
                }
            }
            return sums[targetSum];
        }
    }
}