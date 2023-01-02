namespace SumOfCoins
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var coins = new Queue<int>(Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .OrderByDescending(x => x));

            int needed = int.Parse(Console.ReadLine());
            var usedCoins = new Dictionary<int, int>();
            var totalCoins = 0;

            while (needed > 0 && coins.Count > 0)
            {
                var currentCoin = coins.Dequeue();
                var count = needed / currentCoin;

                if (count == 0)
                {
                    continue;
                }

                usedCoins[currentCoin] = count;
                totalCoins += count;

                needed %= currentCoin;
            }

            if (needed==0)
            {
                Console.WriteLine($"Number of coins to take: {totalCoins}");

                foreach (var item in usedCoins)
                {
                    Console.WriteLine($"{item.Value} coin(s) with value {item.Key}");
                }
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}