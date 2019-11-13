using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankAccount
{
    class Person
    {
        private string name;
        private int age;
        private List<BankAccount> accounts;

        public Person(string name, int age)
        {
            this.age = age;
            this.name = name;
            this.accounts = new List<BankAccount>();
        }

        public Person(string name, int age, List<BankAccount> accounts) : this(name, age)
        {
            this.accounts = accounts;
        }

        public decimal GetBalance()
        {
            return this.accounts.Sum(x => x.Balance);
        }
    }
}
