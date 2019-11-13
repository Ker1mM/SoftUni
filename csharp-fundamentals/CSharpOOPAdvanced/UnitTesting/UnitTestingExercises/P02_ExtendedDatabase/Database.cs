using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P02_ExtendedDatabase
{
    public class Database
    {
        private const int Capacity = 16;

        private Person[] people;
        private int nextFree;

        public Database(params Person[] args)
        {
            this.people = new Person[Capacity];
            this.nextFree = 0;
            this.Add(args);
        }

        public void Add(params Person[] args)
        {
            if (nextFree + args.Length > 16)
            {
                throw new InvalidOperationException();
            }

            int initialNextFree = this.nextFree;
            foreach (var person in args)
            {
                if (this.people.Where(x => x != null).Any(x => x.Name == person.Name || x.Id == person.Id))
                {
                    this.nextFree = initialNextFree;
                    throw new InvalidOperationException();
                }
                this.people[nextFree] = person;
                nextFree++;
            }
        }

        public Person Remove()
        {
            if (this.nextFree == 0)
            {
                throw new InvalidOperationException();
            }

            this.nextFree--;
            return this.people[this.nextFree];
        }

        public Person FindByUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException();
            }

            Person result = this.people.Where(x => x != null).FirstOrDefault(x => x.Name == username);

            if (result == null)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public Person FindById(long id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            Person result = this.people.Where(x => x != null).FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public Person[] Fetch()
        {
            return this.people.Take(nextFree).ToArray();
        }
    }
}
