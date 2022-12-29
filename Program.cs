namespace Permutations
{
    internal class Program
    {
        private static string[] array;
        private static HashSet<string> permutations;
        private static bool[] isUsed;
        static void Main(string[] args)
        {
            array = Console.ReadLine().Split().ToArray();
            Console.WriteLine("---------------");
            permutations = new HashSet<string>();
            //isUsed = new bool[array.Length];

            PermuteWithoutRepetition(0);


        }

        //private static void PermuteWithRepetition(int index)
        //{
        //    if (index >= permutations.Length)
        //    {
        //        Console.WriteLine(string.Join(" ", permutations));
        //        return;
        //    }

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        if (!isUsed[i])
        //        {
        //            isUsed[i] = true;
        //            permutations[index] = array[i];

        //            PermuteWithRepetition(index + 1);
        //            isUsed[i] = false;
        //        }
        //    }

        private static void PermuteWithoutRepetition(int index)
        {
            if (index>=array.Length)
            {
                Console.WriteLine(string.Join(" ",array));
                return;
            }
            PermuteWithoutRepetition(index + 1);
            
            var used= new HashSet<string> { array[index] };

            for (int i = index+1; i < array.Length; i++)
            {
                if (!used.Contains(array[i]))
                {
                    Swap(index, i);
                    PermuteWithoutRepetition(index + 1);
                    Swap(index, i);

                    used.Add(array[i]);
                }
            }
        }

        private static void Swap(int index, int i)
        {
            var temp = array[index];
            array[index] = array[i];
            array[i] = temp;
        }
    }
}