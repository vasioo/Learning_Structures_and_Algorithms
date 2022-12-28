namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            Copy(root);
        }

        private void Copy(Node<T> root)
        {
            if (root != null)
            {
                Insert(root.Value);
                Copy(root.LeftChild);
                Copy(root.RightChild);
            }
        }

        public Node<T> Root { get; private set; }

        public int Count { get; private set; }

        public bool Contains(T element)
        {
            var node = this.Root;
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
                    return true;
                }
            }
            return false;
        }

        public void Insert(T element)
        {
            var newNode = new Node<T>(element);
            if (this.Count == 0)
            {
                this.Root = new Node<T>(element);
                Count++;
                return;
            }
            Node<T> parentNode = null;
            var node = Root;
            while (node != null)
            {
                parentNode = node;
                if (IsGreater(node.Value, element))
                {
                    node = node.LeftChild;
                }
                else if (IsSmaller(node.Value, element))
                {
                    node = node.RightChild;
                }
            }
            //added because we dont know the last element and where would it be
            if (IsGreater(parentNode.Value, element))
            {
                parentNode.LeftChild = newNode;
            }
            else
            {
                parentNode.RightChild = newNode;
            }
            Count++;
        }

        private bool IsGreater(T value, T other)
        {
            return value.CompareTo(other) > 0;
        }

        private bool IsSmaller(T value, T other)
        {
            return value.CompareTo(other) < 0;
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var node = this.Root;
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
                    return new BinarySearchTree<T>(node);
                }
            }
            return null;
        }

        public void EachInOrder(Action<T> action)
        {
            EachInOrder(action, Root);
        }

        private void EachInOrder(Action<T> action, Node<T> root)
        {
            if (root == null)
            {
                return;
            }
            this.EachInOrder(action, root.LeftChild);
            action(root.Value);
            this.EachInOrder(action, root.RightChild);

        }

        public List<T> Range(T lower, T upper)
        {
            var list = new List<T>();

            Range(lower, upper, Root, list);

            return list;
        }

        private void Range(T lower, T upper, Node<T> root, List<T> list)
        {
            if (root == null)
            {
                return;
            }

            var inTheLowerPart = lower.CompareTo(root.Value);
            var inTheHigherPart = upper.CompareTo(root.Value);

            if (inTheLowerPart < 0)
            {
                Range(lower, upper, root.LeftChild, list);
            }

            if (inTheLowerPart <= 0 && inTheHigherPart >= 0)
            {
                list.Add(root.Value);
            }

            if (inTheHigherPart > 0)
            {
                Range(lower, upper, root.RightChild, list);
            }
        }

        public void DeleteMin()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            Root.LeftChild = DeleteMin(Root.LeftChild);
        }

        private Node<T> DeleteMin(Node<T> root)
        {
            if (root.LeftChild == null)
            {
                Count--;
                return root.RightChild;
            }

            root.LeftChild = DeleteMin(root.LeftChild);
            return root;
        }

        public void DeleteMax()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            Root.RightChild = DeleteMax(Root.RightChild);
        }

        private Node<T> DeleteMax(Node<T> root)
        {
            if (root.RightChild == null)
            {
                Count--;
                return root.LeftChild;
            }
            root.RightChild = DeleteMax(root.RightChild);
            return root;

        }

        public int GetRank(T element)
        {
            return this.GetRankDfs(this.Root, element);
        }

        private int GetRankDfs(Node<T> current, T element)
        {
            if (current == null)
            {
                return 0;
            }

            if (this.IsLess(element, current.Value))
            {
                return this.GetRankDfs(current.LeftChild, element);
            }
            else if (this.AreEqual(element, current.Value))
            {
                return GetNodeCount(current);
            }

            return
                GetNodeCount(current.LeftChild)
                + 1
                + this.GetRankDfs(current.RightChild, element);
        }

        private bool AreEqual(T element, T value)
        {
            return element.CompareTo(value) == 0;
        }

        private int GetNodeCount(Node<T> current)
        {
            return current == null ? 0 : current.Count;
        }

        private bool IsLess(T element, T value)
        {
            return element.CompareTo(value) < 0;
        }
    }
}
