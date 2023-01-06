using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashing_and_Collision
{
    internal class HashSet<TKey>
    {
        private HashTable<TKey, TKey> table;

        public HashSet()
        {
            this.table = new HashTable<TKey, TKey>();
        }

        public HashSet(IEnumerable<KeyValue<TKey, TKey>> enumerable)
        {
            this.table = new HashTable<TKey, TKey>();

            foreach (var item in enumerable)
            {
                this.table.AddOrReplace(item.Key, item.Key);
            }
        }

        public void Add(TKey key)
        {
            this.table.AddOrReplace(key, key);
        }

        // returns the collection with the distinct elements b/ween the 2
        public HashSet<TKey> UnionWith(HashSet<TKey> other)
        {
            return new HashSet<TKey>(other.table.Concat(this.table).Distinct());
        }

        // returns all the same elements
        public HashSet<TKey> IntersectWith(HashSet<TKey> other)
        {
            return new HashSet<TKey>(this.table.Where(x => other.Contains(x.Key)));
        }

        // returns the unique elements which are not contained in the second
        public HashSet<TKey> Except(HashSet<TKey> other)
        {
            return new HashSet<TKey>(this.table.Where(x => !other.Contains(x.Key)));
        }

        // returns only the elements which are not in both the sets
        public HashSet<TKey> SymetricExcept(HashSet<TKey> other)
        {
            return this.UnionWith(other).Except(this.IntersectWith(other));
        }

        // sees if the key is contained in the collection
        public bool Contains(TKey key)
        {
            return this.table.ContainsKey(key);
        }
    }
}
