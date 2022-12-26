using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    internal class BinaryTree<T>
    {
        public BinaryTree(Node<T> root)
        {
            Root = root;
        }
        public Node<T> Root { get; set; }

        public string DFS_PreOrder(Node<T> node, int indent)
        {
            string result = $"{new string(' ', indent)}{node.Value}\n";
            if (node.LeftChild != null)
            {
                result += DFS_PreOrder(node.LeftChild, indent + 3);
            }
            if (node.RightChild != null)
            {
                result += DFS_PreOrder(node.RightChild, indent + 3);
            }
            return result;
        }
        public string DFS_InOrder(Node<T> node, int indent)
        {
            string result = "";
            if (node.LeftChild != null)
            {
                result += DFS_InOrder(node.LeftChild, indent + 3);
            }
             result += $"{new string(' ', indent)}{node.Value}\n";
            if (node.RightChild != null)
            {
                result += DFS_InOrder(node.RightChild, indent + 3);
            }
            return result;
        }
        public string DFS_PostOrder(Node<T> node, int indent)
        {
            string result = "";
            if (node.LeftChild != null)
            {
                result += DFS_PostOrder(node.LeftChild, indent + 3);
            }
            
            if (node.RightChild != null)
            {
                result += DFS_PostOrder(node.RightChild, indent + 3);
            }
            result += $"{new string(' ', indent)}{node.Value}\n";
            return result;
        }
    }
}
