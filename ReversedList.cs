namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> where T : IComparable<T>, IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this._items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                return this._items[index];
            }
            set
            {
                this._items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.Grow();
            //it will expand when necessary
            _items[_items.Length - 1] = item;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public int IndexOf(T item)
        {

            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            this.Validation(index);
            for (int i = this.Count; i >index; i--)
            {
                this._items[i] = this._items[i-1];
            }
            this._items[index] = item;
            this.Count ++;
        }

        public void Remove(T item)
        {
            if (IndexOf(item) != -1)
            {
                RemoveAt(IndexOf(item));
            }
        }

        public void RemoveAt(int index)
        {
            this.Validation(index);
            for (int i = index; i < _items.Length - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
        }

        private void Grow()
        {
            if (this.Count == this._items.Length)
            {
                this._items = this.Function();
            }
        }

        private T[] Function()
        {
            var newArray = new T[this.Count * 2];
            Array.Copy(this._items, newArray, this._items.Length);
            return newArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public void Validation(int index)
        {
            if (index<0||index>this.Count)
            {
                throw new IndexOutOfRangeException("index");
            }
        }
    }
}