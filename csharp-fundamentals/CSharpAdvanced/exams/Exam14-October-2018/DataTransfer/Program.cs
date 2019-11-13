using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"s:([^;]+);r:([^;]+);m--""([a-zA-Z\s]+)""";
            int count = int.Parse(Console.ReadLine());

            long totalData = 0;
            for (int i = 0; i < count; i++)
            {
                string input = Console.ReadLine();

                Match rgx = Regex.Match(input, pattern);
                if (rgx.Success)
                {
                    string sender = rgx.Groups[1].Value;
                    string receiver = rgx.Groups[2].Value;
                    string message = rgx.Groups[3].Value;
                    totalData += sender
                        .ToCharArray()
                        .Where(x => Char.IsDigit(x))
                        .Select(x => int.Parse(Convert.ToString(x)))
                        .ToList()
                        .Sum();

                    totalData += receiver
                        .ToCharArray()
                        .Where(x => Char.IsDigit(x))
                        .Select(x => int.Parse(Convert.ToString(x)))
                        .ToList()
                        .Sum();

                    sender = Regex.Replace(sender, @"[^a-zA-Z\s]", "");
                    receiver = Regex.Replace(receiver, @"[^a-zA-Z\s]", "");

                    Console.WriteLine($"{sender} says \"{message}\" to {receiver}");
                }
            }
            Console.WriteLine($"Total data transferred: {totalData}MB");
        }
    }
}
