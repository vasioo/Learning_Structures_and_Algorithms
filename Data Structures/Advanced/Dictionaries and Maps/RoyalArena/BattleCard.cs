using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyaleArena
{
    public class BattleCard : IComparable<BattleCard>
    {
        public BattleCard(int id, CardType type, string name, double damage, double swag)
        {
            Id = id;
            Type = type;
            Name = name;
            Damage = damage;
            Swag = swag;
        }

        public int Id { get; set; }

        public CardType Type { get; set; }

        public string Name { get; set; }

        public double Damage { get; set; }

        public double Swag { get; set; }

        public int CompareTo(BattleCard other)
        {
            int compare = other.Damage.CompareTo(Damage);

            if (compare == 0)
            {
                compare = Id.CompareTo(other.Id);
            }

            return compare;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null || GetType().Name.CompareTo(o.GetType().Name) != 0) return false;
            BattleCard bc = (BattleCard)o;
            return Id == bc.Id &&
                   bc.Damage.Equals(Damage) &&
                   bc.Swag.Equals(Swag) &&
                   Type.Equals(bc.Type) &&
                   Name.Equals(bc.Name);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
