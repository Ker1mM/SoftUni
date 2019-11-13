using System;
using System.Collections.Generic;
using System.Linq;

namespace Tagram
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new Dictionary<string, Dictionary<string, long>>();
            string input;
            while ((input = Console.ReadLine()) != "end")
            {
                string[] tokens = input.Split(" -> ");
                if (tokens.Length == 1)
                {
                    string username = input.Split().Last();
                    if (users.ContainsKey(username))
                    {
                        users.Remove(username);
                    }
                }
                else
                {
                    string username = tokens[0];
                    string tag = tokens[1];
                    int likes = int.Parse(tokens[2]);

                    if (!users.ContainsKey(username))
                    {
                        users.Add(username, new Dictionary<string, long>());
                    }
                    if (!users[username].ContainsKey(tag))
                    {
                        users[username].Add(tag, 0);
                    }
                    users[username][tag] += likes;
                }
            }

            users = users
                .OrderByDescending(x => x.Value.Values.Sum())
                .ThenBy(x => x.Value.Count)
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var user in users)
            {
                Console.WriteLine(user.Key);
                foreach (var tag in user.Value)
                {
                    Console.WriteLine($"- {tag.Key}: {tag.Value}");
                }
            }
        }
    }
}
