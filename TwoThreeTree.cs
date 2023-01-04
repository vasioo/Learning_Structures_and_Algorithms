using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementing_2_3_tree
{
    internal class TwoThreeTree<T> where T : IComparable<T>
    {
        private TreeNode<T> root;

        public void Insert(T key)
        {
            root = Insert(root, key);
        }

        /// <summary>
        /// helper method
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private TreeNode<T> Insert(TreeNode<T> node, T key)
        {
            if (node == null)
            {
                return new TreeNode<T>(key);
            }

            TreeNode<T> returnNode;

            if (node.IsLeaf())
            {
                return Merge(node,new TreeNode<T>(key));
            }

            //is the key smaller than the left key
            if (key.CompareTo(node.LeftKey) < 0)
            {
                returnNode = Insert(node.LeftChild, key);

                if (returnNode == node.LeftChild)
                {
                    return node;
                }
                else
                {
                    return Merge(node, returnNode);
                }
            }
            else if (node.IsTwoNode() || key.CompareTo(node.RightKey) < 0)
            {
                returnNode = Insert(node.MiddleChild, key);

                if (returnNode == node.MiddleChild)
                {
                    return node;
                }
                else
                {
                    return Merge(node,returnNode);
                }
            }
            else
            {
                returnNode = Insert(node.RightChild, key);

                if (returnNode == node.RightChild)
                {
                    return node;
                }
                else
                {
                    return Merge(node, returnNode);
                }
            }
        }

        private TreeNode<T> Merge(TreeNode<T> current, TreeNode<T> currentWithNode)
        {
            if (current.RightKey == null)
            {
                if (current.LeftKey.CompareTo(currentWithNode.LeftKey) < 0)
                {
                    current.RightKey = currentWithNode.LeftKey;
                    current.MiddleChild = currentWithNode.LeftChild;
                    current.RightChild = currentWithNode.MiddleChild;
                }
                else
                {
                    current.RightKey = current.LeftKey;
                    current.RightChild = current.MiddleChild;
                    current.LeftKey = currentWithNode.LeftKey;
                    current.MiddleChild = currentWithNode.MiddleChild;
                }
                return current;
            }

            else if (current.LeftKey.CompareTo(currentWithNode.LeftKey) >= 0)
            {
                TreeNode<T> mergeNode = new TreeNode<T>(current.LeftKey)
                {
                    LeftChild = currentWithNode,
                    MiddleChild = current
                };
                currentWithNode.LeftChild = current.LeftChild;
                current.LeftChild = current.MiddleChild;
                current.MiddleChild = current.RightChild;
                current.RightChild = null;
                current.LeftKey = current.RightKey;
                current.RightKey = default(T);
                return mergeNode;
            }

            else if (current.RightKey.CompareTo(currentWithNode.RightKey) >= 0)
            {
                currentWithNode.MiddleChild = new TreeNode<T>(current.RightKey)
                {
                    LeftChild = currentWithNode.MiddleChild,
                    MiddleChild = current.RightChild
                };
                currentWithNode.LeftChild = current;
                current.RightKey = default(T);
                current.RightChild = null;
                return currentWithNode;
            }

            else
            {
                TreeNode<T> mergedNode = new TreeNode<T>(current.RightKey)
                {
                    LeftChild = current,
                    MiddleChild = currentWithNode
                };
                
                currentWithNode.LeftChild = current.RightChild;
                current.RightChild = null;
                current.RightKey = default(T);
                return mergedNode;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            RecursivePrint(this.root,sb);
            return sb.ToString();
        }

        /// <summary>
        /// printing all the nodes using recursion
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sb"></param>
        private void RecursivePrint(TreeNode<T> node, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }
            if (node.LeftKey != null)
            {
                sb.Append(node.LeftKey).Append(" ");
            }
            if (node.RightKey != null)
            {
                sb.Append(node.RightKey).Append(Environment.NewLine);
            }
            else
            {
                sb.Append(Environment.NewLine);
            }
            if (node.IsTwoNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
            }
            else if (node.IsThreeNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
                RecursivePrint(node.RightChild, sb);
            }
        }
    }
}
