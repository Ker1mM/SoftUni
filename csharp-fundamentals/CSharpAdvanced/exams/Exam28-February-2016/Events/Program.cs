using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"^#(?<name>[a-zA-Z]+):\s*@(?<location>[a-zA-Z]+)\s*(?<hour>[0-9]{1,2}):(?<minutes>[0-9]{1,2})$";
            var locations = new Dictionary<string, Dictionary<string, List<int[]>>>();
            int count = int.Parse(Console.ReadLine());
            for (int i = 0; i < count; i++)
            {
                string input = Console.ReadLine();
                Match rgx = Regex.Match(input, pattern);

                if (rgx.Success)
                {
                    string name = rgx.Groups["name"].ToString();
                    string city = rgx.Groups["location"].ToString();
                    int hour = int.Parse(rgx.Groups["hour"].ToString());
                    int min = int.Parse(rgx.Groups["minutes"].ToString());
                    if ((hour >= 0 && hour <= 23) && (min >= 0 && min <= 59))
                    {
                        if (!locations.ContainsKey(city))
                        {
                            locations.Add(city, new Dictionary<string, List<int[]>>());
                        }

                        if (!locations[city].ContainsKey(name))
                        {
                            locations[city].Add(name, new List<int[]>());
                        }

                        locations[city][name].Add(new int[] { hour, min });
                    }
                }
            }
            List<string> reqLoc = Console.ReadLine().Split(',').ToList();
            locations = locations
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var city in locations)
            {
                if (reqLoc.Contains(city.Key))
                {
                    Console.WriteLine($"{city.Key}:");
                    int counter = 1;
                    foreach (var person in city.Value.OrderBy(x => x.Key))
                    {
                        Console.WriteLine("{0}. {1} -> {2}", counter, person.Key,
                            String.Join(", ",
                            GetHours(person.Value.OrderBy(x => x[0]).ThenBy(x => x[1]).ToList())));
                        counter++;
                    }
                }
            }
        }

        public static List<string> GetHours(List<int[]> hours)
        {
            var result = new List<string>();
            foreach (var hour in hours)
            {
                string temp = "";
                if (hour[0] >= 0 && hour[0] <= 9)
                {
                    temp += "0";
                }
                temp += hour[0] + ":";
                if (hour[1] >= 0 && hour[1] <= 9)
                {
                    temp += "0";
                }
                temp += hour[1];
                result.Add(temp);
            }
            return result;
        }
    }
}
