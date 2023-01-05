using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA_Tree
{
    internal class Node<T> where T:IComparable<T>
    {
        public Node(T value)
        {
            Value= value;
            Level = 1;
            Count = 1;
        }

        public T  Value { get; set; }

        public Node<T> Right { get; set; }

        public Node<T>  Left { get; set; }

        public int  Level { get; set; }

        public int Count { get; set; }
    }
}
