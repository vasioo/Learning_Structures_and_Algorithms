using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    internal class Node<T>
    {
        public Node(T value, Node<T> left=null, Node<T> right=null)
        {
            Value = value;
            LeftChild = left;
            RightChild = right;
        }

        public T Value { get; set; }

        public Node<T> LeftChild { get; set; }
        public Node<T> RightChild { get; set; }
    }
}
