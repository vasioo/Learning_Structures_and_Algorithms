using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_2_3_AVL
{
    internal class Node<T>
    {
        public T Value { get; private set; }

        public Node<T> Parent { get; set; }

        public List<Node<T>> Children { get; private set; }

        public Node(T value)
        {
            Value = value;
            Children = new List<Node<T>>();
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }


    }
}
