namespace Combinations
{
    internal class Program
    {
        private static int k;
        private static string[] elements;
        private static string[] combinations;
        private static bool[] isUsed;
        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split();
            k = int.Parse(Console.ReadLine());

            combinations = new string[k];
            isUsed = new bool[elements.Length];

            Combinations(0,0);
        }

        private static void Combinations(int index,int elementsStartIndex)
        {
            if (index >= combinations.Length)
            {
                Console.WriteLine(string.Join(" ", combinations));
                return;
            }

            for (int i = elementsStartIndex; i < elements.Length; i++)
            {
                if (!isUsed[i])
                {
                    //isUsed[i] = true;
                    combinations[index] = elements[i];
                    Combinations(index + 1,i+1);
                    //isUsed[i] = false;
                }
            }
        }
    }
}