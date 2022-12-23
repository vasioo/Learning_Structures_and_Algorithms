using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementing_Trees
{
    internal class Node<T>
    {
        public Node(T value, params Node<T>[] items)
        {
            Value = value;
            Children = items.ToList() ;
        }

        public T Value { get; set; }
        public List<Node<T>> Children { get; set; }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
