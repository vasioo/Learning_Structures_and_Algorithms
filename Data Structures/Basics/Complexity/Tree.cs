using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementing_Trees
{
    internal class Tree<T>
    {
        public Node<T> Root { get; set; }

        public List<Node<T>> BFS(Node<T> root)
        {
            List<Node<T>> list = new List<Node<T>>();
            Queue<Node<T>> queue = new Queue<Node<T>>();

            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node<T> node = queue.Dequeue();
                list.Add(node);
                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return list;
        }
    }
}
