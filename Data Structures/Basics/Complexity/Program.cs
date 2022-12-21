namespace DataStructures_Fundamentals
{
    internal class Program
    {
        static int count = 0;
        static void Main(string[] args)
        {
            int n = 5;
            //the count of the algorithm is O(n*n)
            //though the real algorithm has to count all the operations(incrementation, changes and etc.)

            Console.WriteLine(GetOperationsCount(5));
        }
        static long GetOperationsCount(int n)
        {
            //n=5
            //Time complexity = 3(n*n)+3n+3 = 93
            //Algorithm complexity = O(n*n) = 25
            long counter = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    counter++;
                }
            }
            return counter;
        }

        //O(2^n)
        static void Algorithm(int n)
        {
            if (n==0)
            {
                return;
            }
            Algorithm(n - 1);
            Algorithm(n - 1);
        }
    }
}