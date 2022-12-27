namespace Traversing_BFS_and_DFS
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            Key = key;
            _children = new List<Tree<T>>();

            foreach (var child in children)
            {
                AddChild(child);
                child.AddParent(this);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => _children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            _children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            Parent = parent;
        }

        public string GetAsString()
        {
            return GetAsString(0).Trim();
        }

        private string GetAsString(int indent = 0)
        {
            var response = new string(' ', indent) + Key + Environment.NewLine;
            foreach (var child in Children)
            {
                response += child.GetAsString(indent + 2);
            }
            return response;
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            var leafNodes = BFS();

            int deepestNodeDepth = 0;
            Tree<T> deepestNode = null;

            foreach (var node in leafNodes)
            {
                int currentDepth = this.GetDepthFromLeafToParent(node);

                if (currentDepth > deepestNodeDepth)
                {
                    deepestNodeDepth = currentDepth;
                    deepestNode = node;
                }
            }

            return deepestNode;
        }

        public List<T> GetLeafKeys()
        {
            var nodes = this.GetLeafNodes();

            return nodes.Select(x => x.Key).ToList();
        }

        private List<Tree<T>> GetLeafNodes()
        {
            var leafNodes = new List<Tree<T>>();
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (node.Children.Count == 0)
                {
                    leafNodes.Add(node);
                }
                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return leafNodes;
        }

        public List<T> GetMiddleKeys()
        {
            var nodes = this.GetMiddleNodes();

            return nodes.Select(x => x.Key).ToList();
        }

        private List<Tree<T>> GetMiddleNodes()
        {
            var leafNodes = new List<Tree<T>>();
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (node.Children.Count > 0 && node.Parent != null)
                {
                    leafNodes.Add(node);
                }
                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return leafNodes;
        }

        public List<T> GetLongestPath()
        {
            var deepestNode = this.GetDeepestLeftomostNode();
            var currentNode = deepestNode;
            var resultPath = new Stack<T>();

            while (currentNode != null)
            {
                resultPath.Push(currentNode.Key);
                currentNode = currentNode.Parent;
            }

            return new List<T>(resultPath);
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var leafNodes = this.GetLeafNodes();
            var result = new List<List<T>>();

            foreach (var leaf in leafNodes)
            {
                var node = leaf;
                var currentSum = 0;
                var currentNodes = new List<T>();

                while (node != null)
                {
                    currentNodes.Add(node.Key);
                    currentSum += Convert.ToInt32(node.Key);
                    node = node.Parent;
                }

                if (currentSum == sum)
                {
                    currentNodes.Reverse();
                    result.Add(currentNodes);
                }
            }
            return result;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var roots = new List<Tree<T>>();

            DFS(this, sum, roots);

            return roots;
        }

        private int DFS(Tree<T> node, int target, List<Tree<T>> roots)
        {
            var currentSum = Convert.ToInt32(node.Key);
            foreach (var child in node.Children)
            {
                currentSum += DFS(child, target, roots);
            }
            if (currentSum == target)
            {
                roots.Add(node);
            }
            return currentSum;
        }

        private List<Tree<T>> BFS()
        {
            var result = new List<Tree<T>>();
            var nodes = new Queue<Tree<T>>();

            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();

                result.Add(currentNode);

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }

            return result;
        }

        private int GetDepthFromLeafToParent(Tree<T> node)
        {
            int depth = 0;
            var current = node;

            while (current.Parent != null)
            {
                depth++;
                current = current.Parent;
            }

            return depth;
        }

    }
}
