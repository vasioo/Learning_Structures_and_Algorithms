namespace Dynamic_Optimizitaion
{
    internal class Program
    {
        private static Dictionary<int, long> cache = new Dictionary<int, long>();

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            Console.WriteLine(CalcFibonacci(n));
        }

        //private static long CalcFibonacci(int n)
        //{
        //    //using recursion it uses a lot of memory and at 50th element it stops
        //    if (n == 1)
        //    {
        //        return 0;
        //    }
        //    if (n == 2)
        //    {
        //        return 1;
        //    }
        //    return CalcFibonacci(n - 1) + CalcFibonacci(n - 2);
        //}
        private static long CalcFibonacci(int n)
        {
            if (cache.ContainsKey(n))
            {
                return cache[n];
            }
            if (n < 2)
            {
                return n;
            }
            var result = CalcFibonacci(n - 1) + CalcFibonacci(n - 2);
            cache[n] = result;
            return result;
        }
    }
}