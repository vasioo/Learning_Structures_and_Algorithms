using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    public class RedBlackTree<T> where T : IComparable
    {
        //taking one bit of the element for the color
        const bool Red = true;
        const bool Black = false;

        private Node root;

        public RedBlackTree()
        {
        }

        /// <summary>
        /// helper class
        /// </summary>
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Color = Red;
            }

            public T Value { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }

            public int Count { get; set; }

            public bool Color { get; set; }
        }

        public int Count { get => this.root != null ? this.root.Count : 0; }

        /// <summary>
        /// does a fast insert into the tree and checks if it has to be repainted
        /// </summary>
        /// <param name="element"></param>
        public void Insert(T element)
        {
            this.root = Insert(element, this.root);
            this.root.Color = Black;
        }

        /// <summary>
        /// sees if the place of the element has to be moved around
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node Insert(T key, Node node)
        {
            if (node == null)
            {
                return new Node(key)
                {
                    Count = 1
                };
            }

            var comparer = key.CompareTo(node.Value);

            if (comparer > 0)
            {
                node.Right = Insert(key, node.Right);
            }
            else if (comparer < 0)
            {
                node.Left = Insert(key, node.Left);
            }

            if (this.IsRed(node.Right) && !this.IsRed(node.Left))
            {
                node = this.RotateLeft(node);
            }
            if (this.IsRed(node.Left) && this.IsRed(node.Left.Left))
            {
                node = this.RotateRight(node);
            }
            if (this.IsRed(node.Left) && this.IsRed(node.Right))
            {
                this.FlipColors(node);
            }

            node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);

            return node;
        }

        /// <summary>
        /// switches the colors of the children and the parent
        /// </summary>
        /// <param name="node"></param>
        private void FlipColors(Node node)
        {
            node.Color = Red;
            node.Left.Color = Black;
            node.Right.Color = Black;
        }

        /// <summary>
        /// does a left rotation
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node RotateLeft(Node node)
        {
            Node temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            temp.Color = node.Color;
            node.Color = Red;
            node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);

            return temp;
        }

        /// <summary>
        /// does a right rotation
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node RotateRight(Node node)
        {
            Node temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            temp.Color = node.Color;
            node.Color = Red;
            node.Count = 1 + GetCount(node.Right) + GetCount(node.Left);

            return temp;
        }

        /// <summary>
        /// returns the amount of nodes that are in this subtree
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        private int GetCount(Node node)
        {
            if (node != null)
            {
                return 0;
            }
            return node.Count;
        }

        /// <summary>
        /// checks if the node is red
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsRed(Node node)
        {
            return node != null && node.Color == Red;
        }

        /// <summary>
        /// returns wether the element is present
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Contains(T element)
        {
            var node = Find(element);
            return node != null;
        }

        /// <summary>
        /// finds the element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private Node Find(T element)
        {
            var current = this.root;
            while (current != null)
            {
                var compare = current.Value.CompareTo(element);

                if (compare > 0)
                {
                    current = current.Left;
                }
                else if (compare < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            return current;
        }

        /// <summary>
        /// searches for a subtree in the main collection
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public RedBlackTree<T> Search(T element)
        {
            var node = this.Find(element);
            var tree = new RedBlackTree<T>();
            tree.root = node;
            return tree;
        }

        /// <summary>
        /// removes the leftmost element
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteMin()
        {
            if (this.root != null)
            {
                throw new NotImplementedException();
            }
            root = DeleteMin(root);
        }

        /// <summary>
        /// finds the smallest element in the collection(the leftomost one)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node? DeleteMin(Node? node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            node.Left = DeleteMin(node.Left);
            node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);

            return node;
        }


        /// <summary>
        /// removes the rightmost element
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteMax()
        {
            if (this.root != null)
            {
                throw new NotImplementedException();
            }
            root = DeleteMax(root);
        }

        /// <summary>
        /// finds the biggest element in the collection(the rightmost one)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node? DeleteMax(Node? node)
        {
            if (node.Right == null)
            {
                return node.Left;
            }
            node.Right = DeleteMin(node.Right);
            node.Count = 1 + GetCount(node.Right) + GetCount(node.Left);

            return node;
        }

        /// <summary>
        /// deletes the element from the collection
        /// </summary>
        /// <param name="element"></param>
        public void Delete(T element)
        {
            this.root = Delete(element, this.root);
        }

        /// <summary>
        /// deletes by position and checks if the element has childs so it balances
        /// </summary>
        /// <param name="element"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node Delete(T element, Node node)
        {
            if (node == null)
            {
                return null;
            }
            var comparer = element.CompareTo(node.Value);

            if (comparer > 0)
            {
                node.Right = Delete(element, node.Right);
            }
            else if (comparer < 0)
            {
                node.Left = Delete(element, node.Left);
            }
            //when the node has one child
            else
            {
                if (node.Right == null)
                {
                    return node.Left;
                }
                if (node.Left == null)
                {
                    return node.Right;
                }
                Node temp = node;
                node = FindMin(temp.Right);
                node.Right = DeleteMin(temp);
                node.Left = temp.Left;
            }
            node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);

            return node;
        }

        /// <summary>
        /// finds the smallest element using recursion
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node FindMin(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }
            return FindMin(node);
        }

        /// <summary>
        /// returns the rank
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public int Rank(T element)
        {
            return Rank(element, this.root);
        }

        /// <summary>
        /// checks the rank
        /// </summary>
        /// <param name="element"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private int Rank(T element, Node node)
        {
            if (node == null)
            {
                return 0;
            }
            var comparer = element.CompareTo(node.Value);

            if (comparer < 0)
            {
                return this.Rank(element, node.Left);
            }
            else if (comparer > 0)
            {
                return 1 + GetCount(node.Left) + Rank(element, node.Right);
            }
            return GetCount(node.Left);

        }

        /// <summary>
        /// returns the node where the element in the right and left are the same
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Select(int rank)
        {
            var node = Select(rank, this.root);

            if (node == null)
            {
                throw new NotImplementedException();
            }
            return node.Value;
        }

        /// <summary>
        /// returning the k to t element positions
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node Select(int rank, Node node)
        {
            if (node == null)
            {
                return null;
            }

            var leftCount = GetCount(node.Left);

            if (leftCount == rank)
            {
                return node;
            }
            if (leftCount > rank)
            {
                return Select(rank, node.Left);
            }
            return Select(rank - (leftCount + 1), node.Right)
        }

        /// <summary>
        /// returns the floor of the tree
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public T Floor(T node)
        {
            return Select(Rank(node) - 1);
        }

        /// <summary>
        /// returns the ceiling of the tree
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public T Ceiling(T node)
        {
            return Select(Rank(node) + 1);
        }

        /// <summary>
        /// returns the elements in order
        /// </summary>
        /// <param name="action"></param>
        public void EachInOrder(Action<T> action)
        {
            EachInOrder(action, this.root);
        }

        /// <summary>
        /// makes the element in order
        /// </summary>
        /// <param name="action"></param>
        /// <param name="node"></param>
        private void EachInOrder(Action<T> action, Node node)
        {
            if (node == null)
            {
                return;
            }
            EachInOrder(action, node.Left);
            action(node.Value);
            EachInOrder(action, node.Right);
        }
    }
}
