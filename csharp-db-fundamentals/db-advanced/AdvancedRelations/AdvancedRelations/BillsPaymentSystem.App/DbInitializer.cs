using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models;
using BillsPaymentSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsPaymentSystem.App
{
    public class DbInitializer
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            SeedUsers(context);
            SeedBankAccounts(context);
            SeedCreditCards(context);
            SeedPaymentMethods(context);
        }

        private static void SeedPaymentMethods(BillsPaymentSystemContext context)
        {
            var paymentMethods = new List<PaymentMethod>();

            int[] userIds = new int[] { 1, 1, 1, 4, 5, 6, 7 };
            int[] types = new int[] { 1, 1, 2, 1, 2, 3, 3 };
            int?[] creditCardIds = new int?[] { null, null, 1, null, 3, 6, null };
            int?[] bankAccountIds = new int?[] { 1, 2, null, 5, null, 6, null };

            for (int i = 0; i < 7; i++)
            {
                var paymentMethod = new PaymentMethod
                {
                    UserId = userIds[i],
                    Type = (PaymentType)types[i],
                    CreditCardId = creditCardIds[i],
                    BankAccountId = bankAccountIds[i]
                };


                if (IsValid(paymentMethod))
                {
                    paymentMethods.Add(paymentMethod);
                }
            }

            context.PaymentMethods.AddRange(paymentMethods);
            context.SaveChanges();
        }

        private static void SeedBankAccounts(BillsPaymentSystemContext context)
        {
            var bankAccounts = new List<BankAccount>();

            decimal[] balances = new decimal[] { 20, 66.99m, 99.66m, 0.00m, 987654321, 1, 1, -1 };
            string[] bankName = new string[] { "First Bank", "Second Bank", "Third Bank", "Not My Bank", "Your Bank", "Bank", "BK", "Test" };
            string[] SWIFTcodes = new string[] { "Code One", "Code Two", "Code Code", "code Blue", "123456", "SW", "Test" };
            for (int i = 0; i < 7; i++)
            {
                var bankAccount = new BankAccount
                {
                    Balance = balances[i],
                    BankName = bankName[i],
                    SWIFT = SWIFTcodes[i]
                };

                if (IsValid(bankAccount))
                {
                    bankAccounts.Add(bankAccount);
                }
            }

            context.BankAccounts.AddRange(bankAccounts);
            context.SaveChanges();
        }

        private static void SeedCreditCards(BillsPaymentSystemContext context)
        {
            var creditCards = new List<CreditCard>();

            decimal[] limits = new decimal[] { 200.50m, 25000, 25.20m, 250, 12811, -20, -0.01m };
            decimal[] moneyOwed = new decimal[] { 22.50m, 2000, 25m, 0.01m, 12811, -20, -0.01m };
            DateTime[] expirationDates = new DateTime[] {
            new DateTime(2020, 1, 27),
            new DateTime(2121, 5, 24),
            new DateTime(2019, 12 ,12),
            new DateTime(2034, 3, 4),
            new DateTime(2022, 2, 22),
            new DateTime(2000, 1, 1),
            new DateTime(2019, 3, 14),
            };

            for (int i = 0; i < 7; i++)
            {
                var creditCard = new CreditCard
                {
                    Limit = limits[i],
                    MoneyOwed = moneyOwed[i],
                    ExpirationDate = expirationDates[i]
                };

                if (IsValid(creditCard))
                {
                    creditCards.Add(creditCard);
                }
            }

            context.CreditCards.AddRange(creditCards);
            context.SaveChanges();
        }

        private static void SeedUsers(BillsPaymentSystemContext context)
        {
            string[] firstNames = { "Chocho", "Kiki", "Pepi", "Gosho", null, "", "Tony" };
            string[] lastNames = { "Chochev", "AreYi'", "Ivanov", "Peshev", null, "Error", "Moony" };
            string[] emails = { "Chochev@abv.bg", "AreYi@gmail.com", "Ivanovi@abv.bg", "otpochivka@email.me", null, "Error", "myemail@gmail.com" };
            string[] passwords = { "125", "321'", "aa22a", "sdgs!@$%%!", null, "Error", "strongpassword" };

            var users = new List<User>();

            for (int i = 0; i < firstNames.Length; i++)
            {
                var user = new User
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    Email = emails[i],
                    Password = passwords[i]
                };

                if (IsValid(user))
                {
                    users.Add(user);
                }

            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static bool IsValid(object entity)
        {
            var vallidationContex = new ValidationContext(entity);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, vallidationContex, validationResults, true);

            return isValid;
        }
    }
}
