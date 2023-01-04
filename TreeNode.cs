using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementing_2_3_tree
{
    internal class TreeNode<T> where T : IComparable<T>
    {
        public T LeftKey;
        public T RightKey;

        public TreeNode<T> LeftChild;
        public TreeNode<T> MiddleChild;
        public TreeNode<T> RightChild;

        public TreeNode(T key)
        {
            this.LeftKey = key;
        }

        public bool IsThreeNode()
        {
            return RightKey != null;
        }
        public bool IsTwoNode()
        {
            return RightKey == null;
        }
        public bool IsLeaf()
        {
            return LeftChild == null && MiddleChild == null && RightChild == null;
        }
    }
}
