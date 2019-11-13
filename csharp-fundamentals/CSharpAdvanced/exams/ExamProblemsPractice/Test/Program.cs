using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime presentTime = DateTime.Parse(Console.ReadLine());
            string input;
            Dictionary<string, DateTime> messages = new Dictionary<string, DateTime>();
            while ((input = Console.ReadLine()) != "END")
            {
                string[] tokens = Regex.Split(input, " / ");
                DateTime time = DateTime.Parse(tokens[1]);
                messages.Add(tokens[0], time);
            }

            messages = messages
                .OrderBy(x => x.Value)
                .ToList()
                .ToDictionary(x => x.Key, x => x.Value);

            DateTime lastMessage = messages.Last().Value;

            foreach (var message in messages)
            {
                Console.WriteLine("<div>{0}</div>", System.Security.SecurityElement.Escape(message.Key));
            }

            string lastActive;

            if (lastMessage.AddDays(1).Date < presentTime.Date)
            {
                lastActive = lastMessage.Date.ToString("dd-MM-yyyy");
            }
            else if (lastMessage.AddDays(1).Date == presentTime.Date)
            {
                lastActive = "yesterday";
            }
            else
            {
                var difference = presentTime - lastMessage;
                if (difference.Hours >= 1)
                {
                    lastActive = difference.Hours + " hour(s) ago";
                }
                else if (difference.Minutes >= 1)
                {
                    lastActive = difference.Minutes + " minute(s) ago";
                }
                else
                {
                    lastActive = "a few moments ago";
                }
            }

            Console.WriteLine("<p>Last active: <time>{0}</time></p>", lastActive);
        }
    }
}
