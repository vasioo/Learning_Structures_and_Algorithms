namespace AdjencyLists
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var graph = new List<int>[]
            {
                new List<int>{3, 6},
                new List<int>{2, 3, 4, 5, 6},
                new List<int>{2, 4, 5},
                new List<int>{0, 1, 5},
                new List<int>{1, 2, 6},
                new List<int>{1, 2, 3},
                new List<int>{0, 1, 4}
            };

            Console.WriteLine(string.Join(" ", graph[0]));

            //how to add indices to the graph elements
            var mapping = new Dictionary<string, int>
            {
                {"Sofia",1 },
                {"Varna",2 }
            };
             
        }
    }
}