using System;
using System.Collections.Generic;
using System.Linq;

namespace TheVLogger
{
    class Program
    {
        static void Main(string[] args)
        {

            Dictionary<string, VloggerData> vLogger = new Dictionary<string, VloggerData>();

            string input;

            while ((input = Console.ReadLine()) != "Statistics")
            {
                string[] tokens = input.Split(" ");
                string command = tokens[1];

                if (command.Equals("joined"))
                {
                    string user = tokens[0];
                    if (!vLogger.ContainsKey(user))
                    {
                        vLogger.Add(user, new VloggerData());
                        vLogger[user].Followed = 0;
                    }
                }
                else if (command.Equals("followed"))
                {
                    string follower = tokens[0];
                    string followed = tokens[2];

                    if ((vLogger.ContainsKey(follower) && vLogger.ContainsKey(followed))
                        && (!follower.Equals(followed))
                        && (!vLogger[followed].Followers.Contains(follower)))
                    {
                        vLogger[follower].Followed++;
                        vLogger[followed].Followers.Add(follower);
                    }
                }
            }
            Console.WriteLine("The V-Logger has a total of {0} vloggers in its logs.", vLogger.Count);

            vLogger = vLogger
            .OrderByDescending(x => x.Value.Followers.Count)
            .ThenBy(x => x.Value.Followed)
            .ToList()
            .ToDictionary(x => x.Key, x => x.Value);

            var firstVlogger = vLogger.First();

            Console.WriteLine("1. {0} : {1} followers, {2} following",
                firstVlogger.Key, firstVlogger.Value.Followers.Count, firstVlogger.Value.Followed);
            Console.WriteLine("*  {0}", String.Join("\n*  ", firstVlogger.Value.Followers.OrderBy(x => x)));

            vLogger.Remove(firstVlogger.Key);

            int counter = 2;
            foreach (var current in vLogger)
            {
                Console.WriteLine("{0}. {1} : {2} followers, {3} following",
                counter, current.Key, current.Value.Followers.Count, current.Value.Followed);
                counter++;
            }
        }

        public class VloggerData
        {
            public List<string> Followers;
            public int Followed;

            public VloggerData()
            {
                this.Followers = new List<string>();
            }
        }
    }
}
