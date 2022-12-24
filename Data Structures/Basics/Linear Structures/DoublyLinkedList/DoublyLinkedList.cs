namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node<T>
            {
                Item = item,
                Next = this.head
            };
            this.head = newNode;
            this.Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node<T>
            {
                Item = item
            };

            if (this.head is null)
            {
                this.head = newNode;
                this.tail = newNode;
            }
            else
            {
                var current = this.head;

                while (current.Next != null)
                {
                    current = current.Next;
                }
                if (current.Next == null)
                {
                    this.tail = current;
                }

                current.Next = newNode;
            }
            newNode.Previous = tail;
            this.Count++;
        }

        public T GetFirst()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException("The Linked list is empty");
            }
            else
            {
                return this.head.Item;
            }
        }

        public T GetLast()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException("The Linked list is empty");
            }
            else
            {
                return this.tail.Item;
            }
        }

        public T RemoveFirst()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException("The Linked list is empty");
            }
            else
            {
                this.head.Next = head;
                this.head = null;
                this.Count--;
                return this.head.Item;
            }

        }

        public T RemoveLast()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException("The Linked list is empty");
            }
            else
            {
                this.tail.Previous = tail;
                this.tail = null;
                this.Count--;
                return this.tail.Item;
            }
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

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}