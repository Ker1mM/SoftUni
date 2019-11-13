using System;
using System.Collections.Generic;

namespace FamilyTree
{
    public class Person
    {
        public List<Person> Children { get; set; }
        public List<Person> Parents { get; set; }

        public string Name { get; set; }
        public string Birthday { get; set; }

        public Person()
        {
            this.Children = new List<Person>();
            this.Parents = new List<Person>();
        }

        public Person(string name, string birthday) : this()
        {
            this.Name = name;
            this.Birthday = birthday;
        }

        public Person(string info) : this()
        {
            if (!info.Contains("/"))
            {
                this.Name = info;
            }
            else
            {
                this.Birthday = info;
            }
        }

        public void Combine(Person person)
        {
            this.Name += person.Name;
            this.Birthday += person.Birthday;
            this.Children.AddRange(person.Children);
            this.Parents.AddRange(person.Parents);
        }

        public override string ToString()
        {
            return $"{this.Name} {this.Birthday}";
        }
    }
}
