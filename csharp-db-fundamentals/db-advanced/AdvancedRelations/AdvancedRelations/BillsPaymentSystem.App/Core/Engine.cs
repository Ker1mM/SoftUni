using BillsPaymentSystem.App.Core.Contracts;
using BillsPaymentSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystem.App.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(ICommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            Console.WriteLine(AvailableCommands());
            while (true)
            {
                try
                {
                    Console.Write("Enter new command: ");
                    string[] args = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (args[0] == "Exit")
                    {
                        break;
                    }
                    using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
                    {
                        string result = commandInterpreter.Read(args, context);
                        Console.WriteLine(result);
                    }

                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
        }

        private string AvailableCommands()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Available commands:");
            sb.AppendLine("UserInfo <UserId>");
            sb.AppendLine("Withdraw <UserId> <Amount>");
            sb.AppendLine("PayBills <UserId> <Amount>");
            sb.AppendLine("Deposit <UserId> <Amount>");
            sb.AppendLine("Exit");
            return sb.ToString();
        }
    }
}
