namespace QuickSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            QuickSort(numbers, 0, numbers.Length - 1);
            Console.WriteLine(string.Join("", numbers));
        }

        private static void QuickSort(int[] numbers, int start, int end)
        {
            if (start >= end)
            {
                return;
            }
            var pivot = start;
            var leftPointer = start+1;
            var rightPointer = end;

            while (leftPointer<=rightPointer)
            {
                if (numbers[leftPointer] > numbers[pivot] 
                    && numbers[rightPointer] < numbers[pivot])
                {
                    Swap(numbers,leftPointer,rightPointer);
                }
                if (numbers[leftPointer] <= numbers[pivot])
                {
                    leftPointer += 1;
                }
                if (numbers[rightPointer] >= numbers[pivot])
                {
                    rightPointer -= 1;
                }
            }
            Swap(numbers,pivot, rightPointer);

            QuickSort(numbers, start,rightPointer-1);
            QuickSort(numbers, rightPointer + 1, end);
        }

        private static void Swap(int[] numbers, int first, int second)
        {
            var temp = numbers[first];
            numbers[first] = numbers[second];
            numbers[second] = temp;
        }
    }
}