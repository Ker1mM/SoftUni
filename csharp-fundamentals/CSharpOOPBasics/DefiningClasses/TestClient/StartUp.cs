using System;
using System.Collections.Generic;

namespace TestClient
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var clients = new Dictionary<int, BankAccount>();
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split();
                string command = tokens[0];
                int id = int.Parse(tokens[1]);
                switch (command)
                {
                    case "Create":
                        if (!clients.ContainsKey(id))
                        {
                            clients.Add(id, new BankAccount(id));
                        }
                        else
                        {
                            Console.WriteLine("Account already exists");
                        }
                        break;
                    case "Deposit":
                        if (clients.ContainsKey(id))
                        {
                            decimal amount = decimal.Parse(tokens[2]);
                            clients[id].Deposit(amount);
                        }
                        else
                        {
                            Console.WriteLine("Account does not exist");
                        }
                        break;
                    case "Withdraw":
                        if (clients.ContainsKey(id))
                        {
                            decimal amount = decimal.Parse(tokens[2]);
                            if (clients[id].Balance < amount)
                            {
                                Console.WriteLine("Insufficient balance");
                            }
                            else
                            {
                                clients[id].Withdraw(amount);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Account does not exist");
                        }
                        break;
                    case "Print":
                        if (clients.ContainsKey(id))
                        {
                            Console.WriteLine(clients[id].ToString());
                        }
                        else
                        {
                            Console.WriteLine("Account does not exist");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
