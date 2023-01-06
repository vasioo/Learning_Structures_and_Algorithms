using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashing_and_Collision
{
    internal class KeyValue<TKey,TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValue(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public override int GetHashCode()
        {
            return this.CombineHashCodes(this.Key.GetHashCode(), this.Value.GetHashCode());
        }

        public override bool Equals(object? other)
        {
            KeyValue<TKey, TValue> element = (KeyValue<TKey, TValue>)other;
            bool equals = Object.Equals(this.Key, element.Key) && Object.Equals(this.Value, element);
            return equals;
        }

        private int CombineHashCodes(int h1,int h2)
        {
            return ((h1 << 5) + h1) ^ h2;
        }

        public override string ToString()
        {
            return $" [{this.Key} -> {this.Value}]"; 
        }
    }
}
