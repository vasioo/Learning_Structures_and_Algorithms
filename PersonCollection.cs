namespace Augmentation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class PersonCollection : IPersonCollection
    {
        Dictionary<string, Person> byEmail = new Dictionary<string, Person>();

        Dictionary<string, SortedSet<Person>> byEmailDomain
            = new Dictionary<string, SortedSet<Person>>();

        Dictionary<string, SortedSet<Person>> byNameTown
            = new Dictionary<string, SortedSet<Person>>();
        SortedDictionary<int, Dictionary<string, SortedSet<Person>>> byAgeTown = new SortedDictionary<int, Dictionary<string, SortedSet<Person>>>();

        public int Count { get => byEmail.Count; }

        public bool AddPerson(string email, string name, int age, string town)
        {
            var person = FindPerson(email);

            if (person != null)
            {
                return false;
            }
            var pp = new Person()
            {
                Email = email,
                Age = age,
                Name = name,
                Town = town
            };

            byEmail[email] = pp;

            var emailDomain = email.Split('@')[1];
            byEmailDomain.AppendValueToKey(emailDomain, pp);

            var nameTown = GetNameTown(pp);
            byNameTown.AppendValueToKey(nameTown, pp);

            byAgeTown.EnsureKeyExists(age);
            byAgeTown[age].AppendValueToKey(town, person);

            return true;
        }

        private string GetNameTown(Person pp)
        {
            return GetNameTown(pp.Name, pp.Town);
        }

        private string GetNameTown(string name, string town)
        {
            return $"{name}_{town}";
        }

        public bool DeletePerson(string email)
        {
            return true;
        }

        public Person FindPerson(string email)
        {
            if (byEmail.ContainsKey(email))
            {
                return byEmail[email];
            }
            return null;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            return byEmailDomain.GetValuesForKey(emailDomain);
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            var key = GetNameTown(name, town);

            return byNameTown.GetValuesForKey(key);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            SortedSet<int> ages = new SortedSet<int>(byAgeTown.Keys);

            var resultKeys = ages.GetViewBetween(startAge, endAge);
            return resultKeys
                .SelectMany(k => byAgeTown[k].Values.SelectMany(v => v));

        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            SortedSet<int> ages = new SortedSet<int>(byAgeTown.Keys);

            var resultKeys = ages.GetViewBetween(startAge, endAge);
            return resultKeys.SelectMany(k =>
                    byAgeTown[k].GetValuesForKey(town)
                .OrderBy(p => p.Email));

        }
    }
}
