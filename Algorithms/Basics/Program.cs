namespace ReversingArray
{
    public class Program
    {
        public static int[] array;

        static void Main(string[] args)
        {
            array = Console.ReadLine().Split().Select(int.Parse).ToArray();

            //Console.WriteLine(Reverse(array));

            UsingRecursion(array,0,array.Length-1);
        }

        //recursion
        private static void UsingRecursion(int[] array, int start, int end)
        {
            if (start>=end)
            {
                Console.WriteLine(string.Join(" ",array));
                return;
            }
            int temp = array[start];
            array[start] = array[end];
            array[end] = temp;
            UsingRecursion(array, start+1, end-1);
        }

        //looping
        private static string Reverse(int[] array)
        {
            for (int i = 0; i < array.Length/2; i++)
            {
                int temp = array[i];
                array[i] = array[array.Length-i-1];
                array[array.Length - i-1] = temp;
            }
            return string.Join(" ", array);
        }
    }
}