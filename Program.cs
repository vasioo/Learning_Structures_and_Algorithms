using System;

namespace NestedLoops
{
    internal class Program
    {
        private static int[] elements;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            elements = new int[n];

            Permutations(0);
        }

        private static void Permutations(int index)
        {
            if (index>=elements.Length)
            {
                Console.WriteLine(string.Join(" ",elements));
                return;
            }

            for (int i = 1; i <=elements.Length; i++)
            {
                elements[index]= i;
                Permutations(index+1);
            }
        }
    }
}