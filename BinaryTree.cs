namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            Value = value;

            LeftChild = leftChild;

            if (LeftChild != null)
            {
                LeftChild.Parent = this;
            }

            RightChild = rightChild;

            if (RightChild != null)
            {
                RightChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            var firstNode = GetAncestors(Search(first));
            var secondNode = GetAncestors(Search(second));
            var intersect = firstNode
                .Intersect(secondNode);

            return intersect.ToArray()[0];
        }

        private List<T> GetAncestors(IAbstractBinaryTree<T> root)
        {
            var result = new List<T>();

            while (root!=null)
            {
                result.Add(root.Value);
                root = root.Parent;
            }

            return result;
        }

        public IAbstractBinaryTree<T> Search(T element)
        {
            var node = this;
            while (node != null)
            {
                if (IsGreater(node.Value, element))
                {
                    node = node.LeftChild;
                }
                else if (IsSmaller(node.Value, element))
                {
                    node = node.RightChild;
                }
                else
                {
                    return node;
                }
            }
            return null;
        }

        private bool IsSmaller(T value, T element)
        {
            return value.CompareTo(element) < 0;
        }

        private bool IsGreater(T value, T element)
        {
            return value.CompareTo(element) > 0;
        }
    }
}
