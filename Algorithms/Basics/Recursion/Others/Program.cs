namespace Recursion_And_Backtracking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            int[] array = new int[n];

            BitImplementation(array, 0);
            //TODO DEBUG
        }

        private static void BitImplementation(int[] array, int index)
        {
            if (index >= array.Length)
            {
                Console.WriteLine(string.Join(string.Empty, array));
                return;
            }
            //implements both 0 and 1 for a given position and whenever both of them are written
            //we recursivly add different implementation for every other position
            for (int i = 0; i < 2; i++)
            {
                array[index] = i;
                BitImplementation(array, index + 1);
            }
        }
    }
}