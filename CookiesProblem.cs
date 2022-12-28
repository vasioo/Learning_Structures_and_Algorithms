using Magnum.Collections;
using System;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            var bag = new OrderedBag<int>(cookies);
            var smallestElement = bag.GetFirst();
            var operations = 0;

            while (smallestElement < k && bag.Count > 1)
            {
                var smallestCookie = bag.RemoveFirst();
                var secondSmallestCookie = bag.RemoveFirst();
                operations++;

                bag.Add(smallestCookie + 2 * secondSmallestCookie);
                smallestElement = bag.GetFirst();
            }
            return smallestElement >= k ? operations : -1;
        }
    }
}
