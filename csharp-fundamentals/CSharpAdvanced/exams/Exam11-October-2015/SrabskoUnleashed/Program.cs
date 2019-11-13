using System;
using System.Collections.Generic;
using System.Linq;

namespace SrabskoUnleashed
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            var singerLog = new Dictionary<string, Dictionary<string, int>>();
            while (input.Equals("End") == false)
            {
                if (IsInputCorrect(input))
                {
                    string singer = input.Split(new string[] { " @" }, StringSplitOptions.None)[0];
                    List<string> concertInfo = input.Split(new string[] { " @" }, StringSplitOptions.None)[1].Split().ToList();
                    int ticketsCount = int.Parse(concertInfo[concertInfo.Count - 1]);
                    concertInfo.RemoveAt(concertInfo.Count - 1);
                    int ticketsPrice = int.Parse(concertInfo[concertInfo.Count - 1]);
                    concertInfo.RemoveAt(concertInfo.Count - 1);
                    string place = string.Join(" ", concertInfo);

                    if (singerLog.ContainsKey(place) == false)
                    {
                        singerLog.Add(place, new Dictionary<string, int>());
                    }
                    if (singerLog[place].ContainsKey(singer) == false)
                    {
                        singerLog[place].Add(singer, 0);
                    }
                    int totalPrice = ticketsCount * ticketsPrice;
                    singerLog[place][singer] += totalPrice;
                }
                input = Console.ReadLine();
            }

            foreach (var current in singerLog)
            {
                Console.WriteLine("{0}", current.Key);
                var tempDict = new Dictionary<string, int>(current.Value);
                tempDict = tempDict
                    .OrderByDescending(x => x.Value)
                    .ToDictionary(x => x.Key, x => x.Value);
                foreach (var inner in tempDict)
                {
                    Console.WriteLine("#  {0} -> {1}", inner.Key, inner.Value);
                }

            }
        }

        static bool IsInputCorrect(string input)
        {
            bool result = true;
            try
            {
                string[] temp = input.Split(new string[] { " @" }, StringSplitOptions.None);
                temp = temp[1].Split();
                int num = int.Parse(temp[temp.Length - 1]);
                num = int.Parse(temp[temp.Length - 2]);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
