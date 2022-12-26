using System.Reflection.Emit;
using System.Xml.Linq;

namespace Traversing_BFS_and_DFS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfItems = int.Parse(Console.ReadLine());

            int root=0;
            for (int i = 0; i < numberOfItems; i++)
            {
                int[] input = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                if (i==0)
                {
                    root=input[0];
                }
            }
            Tree<int> tree = new Tree<int>();
            tree.Print();
        }
    }
}