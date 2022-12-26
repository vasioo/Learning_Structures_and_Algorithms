namespace DataStructures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Node<int> root = new Node<int>(1,
                    new Node<int>(5,
                         new Node<int>(2),
                         new Node<int>(3)),
                    new Node<int>(7,
                         new Node<int>(9),
                         new Node<int>(11))
                );
            BinaryTree<int> binaryTree = new BinaryTree<int>(root);

            //pre order starts at root
            Console.WriteLine(binaryTree.DFS_PreOrder(binaryTree.Root,0));

            Console.WriteLine("----------------------");
            Console.WriteLine("----------------------");

            //in order starts from the elements at the bottom of the tree
            Console.WriteLine(binaryTree.DFS_InOrder(binaryTree.Root,0));

            Console.WriteLine("----------------------");
            Console.WriteLine("----------------------");

            //post order makes the root last
            Console.WriteLine(binaryTree.DFS_PostOrder(binaryTree.Root,0));
        }
    }
}