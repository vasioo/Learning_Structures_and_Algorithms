﻿namespace BattlePoints
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var requiredEnergy = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var battlePoints = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var enemies = requiredEnergy.Length;

            var initialEnergy = int.Parse(Console.ReadLine());

            var dp = new int[enemies + 1, initialEnergy + 1];

            for (int row = 1; row < dp.GetLength(0); row++)
            {
                var enemyIndex = row - 1;
                var enemyEnergy = requiredEnergy[enemyIndex];
                var enemyBattlePoints = battlePoints[enemyIndex];

                for (int energy = 1; energy < dp.GetLength(1); energy++)
                {
                    var skip = dp[row - 1, energy];

                    if (enemyEnergy > energy)
                    {
                        dp[row, energy] = skip;
                        continue;
                    }
                    var take = enemyBattlePoints + dp[row - 1, energy - enemyEnergy];

                    dp[row, energy] = Math.Max(skip, take);
                }
            }
            Console.WriteLine(dp[enemies, initialEnergy]);
        }
    }
}