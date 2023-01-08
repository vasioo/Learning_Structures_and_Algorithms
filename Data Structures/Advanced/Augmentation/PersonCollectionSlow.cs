using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Augmentation
{
    public class PersonCollectionSlow : IPersonCollection
    {
        List<Person> people = new List<Person>();

        public int Count { get => people.Count; }

        /// <summary>
        /// adds a person to the collection
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="town"></param>
        /// <returns></returns>
        public bool AddPerson(string email, string name, int age, string town)
        {
            var person = people.FirstOrDefault(p => p.Email == email);

            if (person == null)
            {
                people.Add(new Person()
                {
                    Email = email,
                    Name = name,
                    Age = age,
                    Town = town
                });
            }
            return person == null;
        }

        /// <summary>
        /// removes a person from the collection
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool DeletePerson(string email)
        {
            var person = people.FirstOrDefault(p => p.Email == email);

            if (person != null)
            {
                return people.Remove(person);
            }
            return false;
        }

        public Person FindPerson(string email)
        {
            return people.FirstOrDefault(p => p.Email == email);
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            Regex marcher = new Regex($"@({emailDomain})(?!\\S)");
            return people
                .Where(p => marcher.IsMatch(p.Email))
                .OrderBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            return people
                .Where(p => p.Name == name && p.Town == town)
                .OrderBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            return people
                .Where(p => startAge <= p.Age && p.Age <= endAge);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            return people
                .Where(p => startAge <= p.Age && p.Age <= endAge && p.Town == town)
                .OrderBy(p=>p,new PersonCompare());
        }
    }
    internal class PersonCompare : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            var compare = x.Age.CompareTo(y.Age);
            return compare == 0 ? x.Email.CompareTo(y.Email) : compare;
        }
    }
}
