using BillsPaymentSystem.App.Core.Contracts;
using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BillsPaymentSystem.App.Core.Commands
{
    public class UserInfoCommand : ICommand
    {
        private BillsPaymentSystemContext context;

        public UserInfoCommand(BillsPaymentSystemContext context)
        {
            this.context = context;
        }

        public string Execute(string[] args)
        {
            var sb = new StringBuilder();
            int userId = int.Parse(args[0]);

            if (context.Users.FirstOrDefault(x => x.UserId == userId) == null)
            {
                throw new ArgumentNullException(null, $"User with id {userId} not found!");
            }

            var user = context.Users
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    Name = x.FirstName + " " + x.LastName,
                    BankAccounts = x.PaymentMethods
                        .Where(y => y.BankAccountId != null)
                        .Select(y => y.BankAccount)
                        .OrderBy(y => y.BankAccountId)
                        .ToList(),

                    CreditCards = x.PaymentMethods
                        .Where(y => y.CreditCardId != null)
                        .Select(y => y.CreditCard)
                        .OrderBy(y => y.CreditCardId)
                        .ToList()
                })
                .FirstOrDefault();

            sb.AppendLine("User: " + user.Name);

            if (user.BankAccounts.Count == 0)
            {
                sb.AppendLine($"User with id {userId} has no Bank Accounts!");
            }
            else
            {
                sb.AppendLine("Bank Accounts:");
                foreach (var account in user.BankAccounts)
                {
                    sb.AppendLine("-- ID: " + account.BankAccountId);
                    sb.AppendLine($"--- Balance: {account.Balance:f2}");
                    sb.AppendLine($"--- Bank: {account.BankName}");
                    sb.AppendLine($"--- SWIFT: {account.SWIFT}");
                }
            }

            if (user.CreditCards.Count == 0)
            {
                sb.AppendLine($"User with id {userId} has no Credit Cards!");
            }
            else
            {
                sb.AppendLine("Credit Cards");
                foreach (var card in user.CreditCards.Where(x => x != null).OrderBy(x => x.CreditCardId))
                {
                    sb.AppendLine("-- ID: " + card.CreditCardId);
                    sb.AppendLine($"--- Limit: {card.Limit:f2}");
                    sb.AppendLine($"--- Money Owed: {card.MoneyOwed:f2}");
                    sb.AppendLine($"--- Limit Left: {card.LimitLeft:f2}");
                    sb.AppendLine($"--- Expiration Date: {card.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
                }
            }

            return sb.ToString();
        }
    }
}
