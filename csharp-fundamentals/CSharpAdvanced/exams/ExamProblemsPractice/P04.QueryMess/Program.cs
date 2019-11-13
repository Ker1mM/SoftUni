using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace P04.QueryMess
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();


            while (input.Equals("END") == false)
            {
                var output = new Dictionary<string, List<string>>();
                string[] queries = input.Split("?&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string query in queries)
                {
                    string[] keyValue = query
                        .Split('=')
                        .Select(x => Regex.Replace(x, "%20", " "))
                        .ToArray()
                        .Select(x => Regex.Replace(x, "\\+", " "))
                        .ToArray()
                        .Select(x => Regex.Replace(x, "\\s+", " "))
                        .ToArray()
                        .Select(x => x.Trim())
                        .ToArray();

                    if (keyValue.Length > 1)
                    {
                        string key = keyValue[0];
                        string value = keyValue[1];

                        if (output.ContainsKey(key) == false)
                        {
                            output.Add(key, new List<string>());
                        }
                        output[key].Add(value);
                    }
                }
                if (output.Count > 0)
                {
                    foreach (var item in output)
                    {
                        Console.Write("{0}=[{1}]", item.Key, string.Join(", ", item.Value));
                    }
                    Console.WriteLine();
                }
                input = Console.ReadLine();
            }
        }
    }
}
