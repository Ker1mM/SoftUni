using System;
using System.Collections.Generic;
using System.Text;

namespace TestClient
{
    public class BankAccount
    {
        private int id;
        private decimal balance;

        public BankAccount(int id)
        {
            this.id = id;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public decimal Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }
        public void Withdraw(decimal amount)
        {
            this.Balance -= amount;
        }

        public override string ToString()
        {
            return $"Account ID{this.Id}, balance {this.Balance:f2}";
        }
    }
}
