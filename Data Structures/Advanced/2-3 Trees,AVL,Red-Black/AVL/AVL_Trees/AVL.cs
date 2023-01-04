using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void Delete(T item)
    {
        if (Contains(item))
        {
            this.Root = Remove(this.Root, item);
            UpdateHeight(this.Root);
        }
    }

    private Node<T> Remove(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Remove(node.Left, item);
            UpdateHeight(node);
        }
        else if (cmp > 0)
        {
            node.Right = this.Remove(node.Right, item);
            UpdateHeight(node);
        }

        if (cmp == 0)
        {
            //if 0 children
            if (node.Left == null && node.Right == null)
            {
                return null;
                //because the parent is the node
            }
            //if smaller
            if (node.Left != null && node.Right == null)
            {
                return node.Left;
            }
            //if bigger
            if (node.Left == null && node.Right != null)
            {
                return node.Right;
            }
            if (node.Left.Height > node.Right.Height)
            {
                var theReplacement = Greatest(node.Left);
                node.Value = theReplacement.Value;
                node.Left = Remove(node.Left, theReplacement.Value);
                UpdateHeight(node.Left);
                UpdateHeight(node);
            }
            else
            {
                var theReplacement = Smallest(node.Right);
                node.Value = theReplacement.Value;
                node.Right = Remove(node.Right, theReplacement.Value);
                UpdateHeight(node.Right);
                UpdateHeight(node);
            }
        }
        return Balance(node);
    }

    public void DeleteMin()
    {
        if (this.Root != null)
        {
            var smallestElement = Smallest(Root);
            Delete(smallestElement.Value);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }

        node = Balance(node);
        UpdateHeight(node);
        return node;
    }

    private Node<T> Balance(Node<T> node)
    {
        var balance = Height(node.Left) - Height(node.Right);
        if (balance > 1)
        {
            var childBalance = Height(node.Left.Left) - Height(node.Left.Right);
            if (childBalance < 0)
            {
                node.Left = RotateLeft(node.Left);
            }

            node = RotateRight(node);
        }
        else if (balance < -1)
        {
            var childBalance = Height(node.Right.Left) - Height(node.Right.Right);
            if (childBalance > 0)
            {
                node.Right = RotateRight(node.Right);
            }

            node = RotateLeft(node);
        }

        return node;
    }

    private void UpdateHeight(Node<T> node)
    {
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private int Height(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        var left = node.Left;
        node.Left = left.Right;
        left.Right = node;

        UpdateHeight(node);

        return left;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        var right = node.Right;
        node.Right = right.Left;
        right.Left = node;

        UpdateHeight(node);

        return right;
    }

    private Node<T> Smallest(Node<T> node)
    {
        if (node == null)
        {
            return null;
        }
        if (node.Left != null)
        {
            return Smallest(node.Left);
        }
        else
        {
            return node;
        }
    }

    private Node<T> Greatest(Node<T> node)
    {
        if (node == null)
        {
            return null;
        }
        if (node.Right != null)
        {
            return Greatest(node.Right);
        }
        else
        {
            return node;
        }
    }
}
