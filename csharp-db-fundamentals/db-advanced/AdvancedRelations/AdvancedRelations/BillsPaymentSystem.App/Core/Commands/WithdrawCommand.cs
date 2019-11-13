using BillsPaymentSystem.App.Core.Contracts;
using BillsPaymentSystem.Data;
using System;
using System.Linq;

namespace BillsPaymentSystem.App.Core.Commands
{
    public class WithdrawCommand : ICommand
    {
        private BillsPaymentSystemContext context;

        public WithdrawCommand(BillsPaymentSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            int userId = int.Parse(args[0]);
            decimal amount = decimal.Parse(args[1]);

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(null, "Amount can not be negative!");
            }

            var userBankAccounts = context.PaymentMethods
                .Where(x => x.UserId == userId && x.BankAccountId != null)
                .Select(x => x.BankAccount)
                .ToList();

            foreach (var account in userBankAccounts)
            {
                if (amount <= 0)
                {
                    break;
                }

                if (account.Balance >= amount)
                {
                    account.Balance -= amount;
                    amount = 0;
                }
                else
                {
                    amount -= account.Balance;
                    account.Balance = 0;
                }
            }

            var userCreditCards = context.PaymentMethods
                .Where(x => x.UserId == userId && x.CreditCardId != null)
                .Select(x => x.CreditCard)
                .ToList();

            foreach (var card in userCreditCards)
            {
                if (amount <= 0)
                {
                    break;
                }

                if (card.LimitLeft >= amount)
                {
                    card.MoneyOwed += amount;
                    amount = 0;
                }
                else
                {
                    amount -= card.LimitLeft;
                    card.MoneyOwed = card.Limit;
                }
            }

            if (amount > 0)
            {
                throw new ArgumentException("Insufficient funds!");
            }

            string result = "Withdraw successful!";
            context.SaveChanges();

            return result;
        }
    }
}

