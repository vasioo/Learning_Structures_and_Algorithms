namespace BinarySearch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numbers = new[] { 1, 5, 10, 15, 20, 30, 40, 50, 55, 60 };

            Console.WriteLine(BinarySearch(numbers, 55));
        }

        private static int BinarySearch(int[] numbers, int num)
        {
            int left = 0;
            int right = numbers.Length-1;

            while (left<=right)
            {
                var mid = (left + right) / 2;

                if (numbers[mid]==num)
                {
                    return mid;
                }
                else if (numbers[mid]<num)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return -1;
        }
    }
}