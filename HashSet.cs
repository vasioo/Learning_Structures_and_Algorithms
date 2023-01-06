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

        public HashSet<TKey> UnionWith(HashSet<TKey> other)
        {
            return new HashSet<TKey>(other.table.Concat(this.table).Distinct());
        }

        public HashSet<TKey> IntersectWith(HashSet<TKey> other)
        {
            return new HashSet<TKey>(this.table.Where(x => other.Contains(x.Key)));
        }

        public HashSet<TKey> Except(HashSet<TKey> other)
        {

        }
        
        public HashSet<TKey> SymetricExcept(HashSet<TKey> other)
        {

        }

        /// <summary>
        /// sees if the key is contained in the collection
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(TKey key)
        {
            return this.table.ContainsKey(key);
        }
    }
}
