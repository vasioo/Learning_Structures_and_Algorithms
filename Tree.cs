using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traversing_BFS_and_DFS
{
    internal class Tree<T>
    {
        public Node<T> Root { get; set; }

        public void Print(Node<T> node,int level)
        {
            Console.Write(new string(' ', level));
            Console.WriteLine(node);
            foreach (var child in node.Children)
            {
                Print(child, level + 2);
            }
        }
    }
}
