namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this.head;

            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            this.EnsureNotEmpty();

            var headItem =head.Item;
            var newHead = head.Next;
            head.Next = null;
            head = newHead;

            this.Count--;

            return headItem;
        }

        public void Enqueue(T item)
            //this implementation ensures O(1)
            //because we always keep the pointer to the last element
            //whenever there is a need for adding we have the position and
            //just have to surpass the current element
        {
            var newNode = new Node<T>(item, null);
            if (Count == 0)
            {
                head = tail = newNode;
                this.Count++;
                return;
            }
            tail.Next = newNode;
            tail = tail.Next;
            this.Count++;
        }

        public T Peek()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.head;
            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();


        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();
        }
    }
}