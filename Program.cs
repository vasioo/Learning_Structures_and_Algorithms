namespace BubbleSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            BubbleSort(numbers);

            Console.WriteLine(string.Join(" ", numbers));
        }

        private static void BubbleSort(int[] numbers)
        {
            var sortedCount = 0;
            var isSorted = false;

            while (!isSorted)
            {
                isSorted = true;

                //if we swap even once then it is not sorted
                //can be done with two nested loops
                for (int j = 1; j < numbers.Length - sortedCount; j++)
                {
                    var i = j - 1;

                    if (numbers[i] >numbers[j])
                    {
                        Swap(numbers, i, j);
                        isSorted = false;
                    }
                }
                sortedCount++;
            }
        }
        private static void Swap(int[] numbers, int first, int second)
        {
            var temp = numbers[first];
            numbers[first] = numbers[second];
            numbers[second] = temp;
        }

    }
}