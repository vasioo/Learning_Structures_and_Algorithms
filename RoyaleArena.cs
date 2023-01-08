using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyaleArena
{
    public class RoyaleArena : IArena
    {
        private Dictionary<int, BattleCard> deck = new Dictionary<int, BattleCard>();

        private Dictionary<CardType, Table<BattleCard>> cardTypeSortedByDamage = new Dictionary<CardType, Table<BattleCard>>();

        private Dictionary<string, Table<BattleCard>> nameSortedBySwag = new Dictionary<string, Table<BattleCard>>();

        private Table<BattleCard> sortBySwag = new Table<BattleCard>(new SwagIndex());

        public int Count { get => deck.Keys.Count; }

        public void Add(BattleCard card)
        {
            deck[card.Id] = card;
            AddToGroupCollection<DamageIndex>(cardTypeSortedByDamage, card, (c) => c.Type);
            AddToGroupCollection<SwagIndex>(nameSortedBySwag, card, (c) => c.Name);
            sortBySwag.Add(card);
        }

        private void AddToGroupCollection<T>(IDictionary dictionary, BattleCard card, Func<BattleCard, object> getKey)
             where T : Index<double>, new()
        {
            var key = getKey(card);

            if (dictionary[key] == null)
            {
                dictionary[key] = new Table<BattleCard>(new T());
            }
            (dictionary[key] as Table<BattleCard>).Add(card);
        }

        public void ChangeCardType(int id, CardType type)
        {
            if (!deck.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }
            RemoveFromTheGivenCollection(cardTypeSortedByDamage, deck[id], (c) => c.Damage);
            deck[id].Type = type;
            AddToGroupCollection<DamageIndex>(cardTypeSortedByDamage, deck[id], (c) => c.Type);
        }

        private void RemoveFromTheGivenCollection(IDictionary dictionary, BattleCard card, Func<BattleCard, object> getKey)
        {
            var key = getKey(card);

            if (dictionary[key] != null)
            {
                var items = dictionary[key] as Table<BattleCard>;
                items.Remove(card);

                if (items.Count() == 0)
                {
                    dictionary.Remove(key);
                }
            }
        }

        public bool Contains(BattleCard card)
        {
            return deck.ContainsKey(card.Id);
        }

        public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
        {
            return sortBySwag.GetFirstN(n, c => c.Id);
        }

        public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
        {
            return sortBySwag.GetViewBetween(lo, hi);
        }

        public IEnumerable<BattleCard> GetByCardType(CardType type)
        {
            return cardTypeSortedByDamage[type];
        }

        public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
        {
            return cardTypeSortedByDamage[type]?
                .GetViewBetween(cardTypeSortedByDamage[type].MinKey, damage)
                .OrderBy(c => c);
        }

        public BattleCard GetById(int id)
        {
            return deck[id];
        }

        public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
        {
            return nameSortedBySwag[name]?.GetViewBetween(lo, hi);
        }

        public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
        {
            return nameSortedBySwag[name].Reverse();
        }

        public IEnumerable<BattleCard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
        {
            return cardTypeSortedByDamage[type]?.GetViewBetween(lo, hi)
                .OrderBy(c => c);
        }

        public IEnumerator<BattleCard> GetEnumerator()
        {
            return deck.Values.GetEnumerator();
        }

        public void RemoveById(int id)
        {
            if (!deck.ContainsKey(id))
            {
                throw new InvalidOperationException();
            }

            RemoveFromTheGivenCollection(cardTypeSortedByDamage, deck[id], (c) => c.Type);
            RemoveFromTheGivenCollection(nameSortedBySwag, deck[id], (c) => c.Name);
            sortBySwag.Remove(deck[id]);
            deck.Remove(id);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
