using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashing_and_Collision
{
    internal class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        public const float LoadFactor = 0.7f;
        public const int DefaultCapacity = 16;

        /// <summary>
        /// used for the chaining
        /// </summary>
        private LinkedList<KeyValue<TKey, TValue>>[] elements;

        public int Count { get; set; }

        public int Capacity
        {
            get
            {
                return this.elements.Length;
            }
        }

        public HashTable(int defaultCapacity = DefaultCapacity)
        {
            this.elements
                = new LinkedList<KeyValue<TKey, TValue>>[defaultCapacity];
        }

        /// <summary>
        /// checking if has to be resized and casting the element to the corresponding space
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public void Add(TKey key, TValue value)
        {
            GrowIfNeeded();
            int index = Math.Abs(key.GetHashCode() % this.Capacity);

            if (this.elements[index] == null)
            {
                this.elements[index] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in this.elements[index])
            {
                if (element.Key.Equals(key))
                {
                    throw new ArgumentException("The key already has a value");
                }
            }

            KeyValue<TKey, TValue> kvp = new KeyValue<TKey, TValue>(key, value);
            this.Count++;
            this.elements[index].AddLast(kvp);
        }

        /// <summary>
        /// clears the collection
        /// </summary>
        public void Clear()
        {
            this.elements = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
        }

        /// <summary>
        /// finds the element position
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int index = Math.Abs(key.GetHashCode() % this.Capacity);

            if (this.elements[index] != null)
            {
                foreach (var item in this.elements[index])
                {
                    if (item.Key.Equals(key))
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// checks if the key is present
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return this.Find(key) != null;
        }

        /// <summary>
        /// resizing if needed
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void GrowIfNeeded()
        {
            float loadFactor = (float)(this.Count + 1) / this.Capacity;

            if (loadFactor >= LoadFactor)
            {
                Grow();
            }
        }

        /// <summary>
        /// grows the table
        /// </summary>
        private void Grow()
        {
            HashTable<TKey, TValue> newTable
                = new HashTable<TKey, TValue>(Capacity * 2);

            foreach (var element in elements)
            {
                foreach (var kvp in element)
                {
                    newTable.Add(kvp.Key, kvp.Value);
                }
            }
            this.elements = newTable.elements;
        }

        /// <summary>
        /// tries getting the value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            KeyValue<TKey, TValue> kvp = this.Find(key);

            if (kvp == null)
            {
                value = default(TValue);
                return false;
            }
            value = kvp.Value;
            return true;
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.Get(key);
            }
            set
            {
                this.AddOrReplace(key, value);
            }
        }

        /// <summary>
        /// replaces or adds a value according to the hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrReplace(TKey? key, TValue? value)
        {
            GrowIfNeeded();

            int index = Math.Abs(key.GetHashCode() % this.Capacity);

            if (this.elements[index] == null)
            {
                this.elements[index] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in this.elements[index])
            {
                if (element.Key.Equals(key))
                {
                    element.Value = value;
                    return true;
                }
            }

            KeyValue<TKey, TValue> kvp = new KeyValue<TKey, TValue>(key, value);

            this.elements[index].AddLast(kvp);
            this.Count++;
            return false;
        }

        /// <summary>
        /// gets the value corresponding to the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public TValue Get(TKey key)
        {
            KeyValue<TKey, TValue> kvp = this.Find(key);

            if (kvp == null)
            {
                throw new KeyNotFoundException();
            }
            return kvp.Value;
        }

        /// <summary>
        /// gets all the values 
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                foreach (var kvp in elements)
                {
                    foreach (var item in kvp)
                    {
                        yield return item.Value;
                    }
                }
            }
        }

        /// <summary>
        /// gets all the keys
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get
            {
                if (this.Count == 0)
                {
                    return new LinkedList<TKey>();
                }
                return this.elements.SelectMany(x => x).Select(y => y.Key);
            }
        }

        public bool Remove(TKey key)
        {

            int index = Math.Abs(key.GetHashCode() % this.Capacity);

            if (this.elements[index] == null)
            {
                return false;
            }
            KeyValue<TKey, TValue> kvpToRemove = null;
            foreach (var element in this.elements[index])
            {
                if (element.Key.Equals(key))
                {
                    kvpToRemove = element;
                }
            }
            if (kvpToRemove != null)
            {
                return false;
            }
            this.elements[index].Remove(kvpToRemove);
            this.Count--;
            return true;
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var element in this.elements.Where(x => x != null))
            {
                foreach (var kvp in element)
                {
                    yield return kvp;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
