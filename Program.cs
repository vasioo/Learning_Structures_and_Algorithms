namespace Cinema
{
    internal class Program
    {
        private static string[] names;
        private static Dictionary<int, string> dictionary;
        private static List<string> namesOfPeopleWhoDontCare;
        private static bool[] isUsed;

        static void Main(string[] args)
        {
            names = Console.ReadLine().Split(", ");
            string[] input = Console.ReadLine().Split(" - ");

            dictionary = new Dictionary<int, string>();
            namesOfPeopleWhoDontCare = new List<string>(names);

            isUsed = new bool[names.Length];

            while (input[0] != "generate")
            {
                dictionary.Add(Convert.ToInt32(input[1]), input[0]);
                namesOfPeopleWhoDontCare.Remove(input[0]);
                input = Console.ReadLine().Split(" - ");
            }

            Permutations(0);
        }

        private static void Permutations(int index)
        {
            if (index >= namesOfPeopleWhoDontCare.Count)
            {
                string[] output = new string[names.Length];

                int insiderIndex = 0;

                for (int i = 0; i < names.Length; i++)
                {
                    if (dictionary.ContainsKey(i + 1))
                    {
                        output[i]=dictionary[i + 1];
                    }
                    else if (output[i]==null)
                    {
                        output[i] = namesOfPeopleWhoDontCare[insiderIndex++];
                    }
                }
                Console.WriteLine(string.Join(" ", output));
                return;
            }
            Permutations(index + 1);

            var used = new HashSet<string> { namesOfPeopleWhoDontCare[index] };

            for (int i = index + 1; i < namesOfPeopleWhoDontCare.Count; i++)
            {
                if (!used.Contains(namesOfPeopleWhoDontCare[i]))
                {
                    Swap(index, i);
                    Permutations(index + 1);
                    Swap(index, i);

                    used.Add(namesOfPeopleWhoDontCare[i]);
                }
            }
        }
        private static void Swap(int index, int i)
        {
            var temp = namesOfPeopleWhoDontCare[index];
            namesOfPeopleWhoDontCare[index] = namesOfPeopleWhoDontCare[i];
            namesOfPeopleWhoDontCare[i] = temp;
        }
    }
}