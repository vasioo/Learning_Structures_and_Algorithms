using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augmentation
{
    public class Person : IComparable<Person>
    {
        public string Email { get; set; }

        public int Age { get; set; }

        public string Name { get; set; }

        public string Town { get; set; }

        public int CompareTo(Person other)
        {
            return Email.CompareTo(other.Email);
        }

        public override bool Equals(object obj)
        {
            Person other = obj as Person;

            if (other == null)
            {
                return false;
            }
            return other.Age == Age
                && other.Name == Name
                && other.Email == Email
                && other.Town == Town;
        }

        public override int GetHashCode()
        {
            return Email.GetHashCode();
        }
    }
}
