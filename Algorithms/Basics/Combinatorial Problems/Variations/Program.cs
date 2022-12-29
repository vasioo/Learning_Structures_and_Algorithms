namespace Variations
{
    internal class Program
    {
        private static int k;
        private static string[] elements;
        private static string[] variations;
        private static bool[] isUsed;
        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split();
            k = int.Parse(Console.ReadLine());

            variations = new string[k];
            isUsed = new bool[elements.Length];

            Variations(0);
        }

        private static void Variations(int index)
        {
            if (index>= variations.Length)
            {
                Console.WriteLine(string.Join(" ",variations));
                return;
            }

            for (int i = 0; i < elements.Length; i++)
            {
                if (!isUsed[i])
                {
                    //isUsed[i] = true;
                    variations[index] = elements[i];
                    Variations(index+1);
                    //isUsed[i] = false;
                }
            }
        }
    }
}