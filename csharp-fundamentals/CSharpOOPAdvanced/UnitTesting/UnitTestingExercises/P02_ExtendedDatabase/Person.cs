using System;
using System.Collections.Generic;
using System.Text;

namespace P02_ExtendedDatabase
{
    public class Person
    {
        public string Name { get; private set; }
        public long Id { get; private set; }

        public Person(string name, long id)
        {
            this.Name = name;
            this.Id = id;
        }
    }
}
