using BillsPaymentSystem.App.Core.Contracts;
using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillsPaymentSystem.App.Core
{
    public class DepositCommand : ICommand
    {
        private BillsPaymentSystemContext context;

        public DepositCommand(BillsPaymentSystemContext context)
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
                .OrderBy(x => x.BankAccountId)
                .ToList();

            if (userBankAccounts.Count == 0)
            {
                throw new ArgumentNullException(null, $"User with ID: {userId} has no Bank Accounts!");
            }

            var selectedAccount = userBankAccounts.First();
            if (userBankAccounts.Count > 1)
            {
                var sb = new StringBuilder();

                Console.WriteLine($"User has {userBankAccounts.Count} bank accounts. Select your preference:");
                foreach (var bankAccount in userBankAccounts)
                {
                    sb.AppendLine($"Account ID: {bankAccount.BankAccountId} - Balance: {bankAccount.Balance:f2}");
                }

                while (true)
                {
                    Console.WriteLine(sb.ToString());
                    Console.Write("Enter account ID:");
                    int bankId = int.Parse(Console.ReadLine());

                    selectedAccount = userBankAccounts.Where(x => x.BankAccountId == bankId).FirstOrDefault();

                    if (selectedAccount != null)
                    {
                        break;
                    }
                    Console.WriteLine("Incorrect ID!");
                }
            }

            selectedAccount.Balance += amount;
            context.SaveChanges();

            return "Depost successful!";

        }
    }
}
