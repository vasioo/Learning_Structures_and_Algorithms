using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA_Tree
{
    internal class AA_Tree<T> where T : IComparable<T>
    {
        Node<T> root;

        /// <summary>
        /// checks if the root is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return root == null;
        }

        /// <summary>
        /// clears the tree
        /// </summary>
        public void Clear()
        {
            root = null;
        }

        /// <summary>
        /// calls the insert method
        /// </summary>
        /// <param name="element"></param>
        public void Insert(T element)
        {
            root = Insert(root, element);
        }

        /// <summary>
        /// inserts the element into the collection
        /// </summary>
        /// <param name="node"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private Node<T> Insert(Node<T> node, T element)
        {
            if (node == null)
            {
                return new Node<T>(element);
            }

            var comparer = element.CompareTo(node.Value);

            if (comparer > 0)
            {
                node.Right = Insert(node.Right, element);
            }
            if (comparer < 0)
            {
                node.Left = Insert(node.Left, element);
            }
            node = Skew(node);
            node = Split(node);

            node.Count = GetCount(node.Left) + GetCount(node.Right) + 1;

            return node;
        }

        /// <summary>
        /// gets the count if zero returns null
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int GetCount(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Count;
        }

        /// <summary>
        /// did when 2 horizontal links are connected and have to rebalance
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private Node<T> Split(Node<T> node)
        {
            if (node.Level == node.Right?.Right?.Level)
            {
                var temp = node.Right;
                node.Right = temp.Left;
                temp.Left = temp;

                node.Count = GetCount(node.Left) + GetCount(node.Right) + 1;
                temp.Level = Level(temp.Right) + 1;

                return temp;
            }
            return node;
        }

        /// <summary>
        /// gets the level of the node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int Level(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Level;
        }

        /// <summary>
        /// does a right rotation(single)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private Node<T> Skew(Node<T> node)
        {
            if (node.Level == node.Left?.Level)
            {
                var temp = node.Left;
                node.Left = temp.Right;
                temp.Right = node;

                node.Count = GetCount(node.Left) + GetCount(node.Right) + 1;

                return temp;
            }
            return node;
        }

        /// <summary>
        /// returns the count
        /// </summary>
        /// <returns></returns>
        public int CountNodes()
        {
            return this.root != null ? this.root.Count : 0;
        }

        /// <summary>
        /// left-> root -> right
        /// </summary>
        /// <param name="action"></param>
        public void InOrder(Action<T> action)
        {
            VisitInOrder(this.root, action);
        }

        /// <summary>
        /// sees the elements in a particular order
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void VisitInOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            VisitInOrder(node.Left, action);//left
            action(node.Value);//root
            VisitInOrder(node.Right, action);//right
        }

        /// <summary>
        /// root-> left -> right
        /// </summary>
        /// <param name="action"></param>
        public void PreOrder(Action<T> action)
        {
            VisitPreOrder(this.root, action);
        }

        /// <summary>
        /// sees the elements in a particular order
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void VisitPreOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            action(node.Value);//root
            VisitInOrder(node.Left, action);//left
            VisitInOrder(node.Right, action);//right
        }

        /// <summary>
        /// left -> right -> root
        /// </summary>
        /// <param name="action"></param>
        public void PostOrder(Action<T> action)
        {
            VisitPostOrder(this.root, action);
        }


        /// <summary>
        /// sees the elements in a particular order
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void VisitPostOrder(Node<T> node, Action<T> action)
        {
            if (node==null)
            {
                return;
            }
            VisitInOrder(node.Left, action);//left
            VisitInOrder(node.Right, action);//right
            action(node.Value);//root
        }

        /// <summary>
        /// searching a particular element
        /// </summary>
        /// <param name="element"></param>
        /// <returns>(true/false)</returns>
        public bool Search(T element)
        {
            return Search(this.root, element);
        }

        /// <summary>
        /// sees if an element is present in the collection and returns either true or false
        /// </summary>
        /// <param name="node"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool Search(Node<T> node, T element)
        {
            if (node==null)
            {
                return false;
            }
            var comparer = element.CompareTo(node.Value);

            if (comparer>0)
            {
                return Search(node.Right,element);
            }
            if (comparer < 0)
            {
                return Search(node.Left, element);
            }
            return true;
        }
    }
}
