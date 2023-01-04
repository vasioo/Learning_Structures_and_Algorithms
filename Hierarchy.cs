using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_2_3_AVL
{
    internal class Hierarchy<T> : IHierarchy<T>
    {
        private Node<T>? root;
        private Dictionary<T, Node<T>> elements = new Dictionary<T, Node<T>>();

        public Hierarchy(T root)
        {
            this.root = CreateNode(root);
        }

        public int Count { get; }

        /// <summary>
        /// checking wether the element is present in the dictionary
        /// if it is throw an ex, otherwise add it
        /// </summary>
        /// <param name="element"></param>
        /// <param name="child"></param>
        /// <exception cref="ArgumentException"></exception>
        public void Add(T element, T child)
        {
            ContainsItem(element);

            if (elements.ContainsKey(child))
            {
                throw new ArgumentException();
            }

            var node = CreateNode(child);
            node.Parent = elements[child];
            elements[child].Children.Add(node);
        }

        /// <summary>
        /// adding the node to the collection
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        private Node<T> CreateNode(T? child)
        {
            var node = new Node<T>(child);
            elements[child] = node;
            return node;
        }

        /// <summary>
        /// checking wether the element is present
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Contains(T element)
        {
            return elements.ContainsKey(element);
        }

        /// <summary>
        /// checking wether the element is present
        /// getting all the children with LINQ 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public IEnumerable<T> GetChildren(T element)
        {
            ContainsItem(element);
            return elements[element].Children.Select(c => c.Value);
        }

        /// <summary>
        /// returns the elements that are common for both the collections
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public IEnumerable<T> GetCommonElements(IHierarchy<T> other)
        {
            foreach (var element in elements)
            {
                //Value.Value because of the dictionary kvp
                if (other.Contains(element.Value.Value))
                {
                    yield return element.Value.Value;
                }
            }
        }

        /// <summary>
        /// enumerates over all the elements by levels
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(this.root);

            while (queue.Count>0)
            {
                var node = queue.Dequeue();

                yield return node.Value;

                foreach (var item in node.Children)
                {
                    queue.Enqueue(item);
                }
            }
        }

        /// <summary>
        /// returns the parent element or the type
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public T GetParent(T element)
        {
            ContainsItem(element);
            var node = elements[element];
            //if the parent is present returns the value otherwise returns the type
            return node.Parent != null ? node.Parent.Value : default(T);
        }

        /// <summary>
        /// removing the element
        /// </summary>
        /// <param name="element"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Remove(T element)
        {
            if (this.root.Value.Equals(element))
            {
                throw new InvalidOperationException();
            }
            ContainsItem(element);
            RemoveElement(element);
        }

        /// <summary>
        /// helper method for removing the element and changing
        /// if the element is a root it throws an ex
        /// if the element is a parent it is removed but the children are added to a new parent
        /// </summary>
        /// <param name="element"></param>
        private void RemoveElement(T? element)
        {
            var node = elements[element];
            node.Parent.Children.Remove(node);
            if (node.Parent != null && node.Children.Count > 0)
            {
                foreach (var child in node.Children)
                {
                    child.Parent = node.Parent;
                    node.Parent.Children.Add(child);
                }
            }
            elements.Remove(element);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// self-explanatory
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="ArgumentException"></exception>
        private void ContainsItem(T item)
        {
            if (!Contains(item))
            {
                throw new ArgumentException("The element is not contained in the tree");
            }
        }
    }
}
