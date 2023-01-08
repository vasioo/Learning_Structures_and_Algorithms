using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyaleArena
{
    public abstract class Index<TKey> : IComparer<BattleCard>, IEnumerable<TKey>
        where TKey : IComparable<TKey>
    {
        internal abstract Func<BattleCard, TKey> GetKey { get; }

        protected abstract SortedSet<TKey> Keys { get; }

        public TKey Min => Keys.Min;

        public TKey Max => Keys.Max;

        public int Count => Keys.Count;

        public void Add(TKey key)
        {
            Keys.Add(key);
        }

        public void Remove(TKey key)
        {
            Keys.Remove(key);
        }

        public IEnumerable<TKey> GetViewBetween(TKey min, TKey max)
        {
            return Keys.GetViewBetween(min, max);
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return Keys.GetEnumerator();
        }

        public virtual int Compare(BattleCard? x, BattleCard? y)
        {
            return GetKey(x).CompareTo(GetKey(y));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SwagIndex : Index<double>
    {
        SortedSet<double> keys = new SortedSet<double>();

        protected override SortedSet<double> Keys => keys;

        internal override Func<BattleCard, double> GetKey => (card) => card.Swag;
    }

    public class DamageIndex : Index<double>
    {
        SortedSet<double> keys = new SortedSet<double>();

        protected override SortedSet<double> Keys => keys;

        internal override Func<BattleCard, double> GetKey => (card) => card.Damage;

        public override int Compare(BattleCard? x, BattleCard? y)
        {
            int cmp = base.Compare(x, y);

            if (cmp == 0)
            {
                cmp = x.Id.CompareTo(y.Id);
            }
            return cmp;
        }
    }
}
