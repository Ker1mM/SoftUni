using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace P03.JediCodeX
{
    class Program
    {
        static void Main(string[] args)
        {
            int lineCount = int.Parse(Console.ReadLine());
            var messages = new StringBuilder();
            while (lineCount-- > 0)
            {
                messages.Append(Console.ReadLine());
            }
            string namePattern = Console.ReadLine();
            string messagePattern = Console.ReadLine();
            List<int> indexes = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            string nameRegex = @"(?<=" + namePattern + @")([a-zA-Z]{" + namePattern.Length.ToString() + @"})(?=[^a-zA-Z]|$)";
            string messageRegex = @"(?<=" + messagePattern + @")([a-zA-Z0-9]{" + messagePattern.Length.ToString() + @"}(?=[^a-zA-Z0-9]))";

            List<string> decodedNames = new List<string>();
            List<string> decodedMessages = new List<string>();

            MatchCollection matchedNames = Regex.Matches(messages.ToString(), nameRegex);
            MatchCollection matchedMessages = Regex.Matches(messages.ToString(), messageRegex);
            foreach (Match name in matchedNames)
            {
                decodedNames.Add(name.ToString());
            }
            foreach (Match message in matchedMessages)
            {
                decodedMessages.Add(message.ToString());
            }

            for (int i = 0; i < decodedNames.Count; i++)
            {
                if (i < indexes.Count && indexes[i] - 1 >= 0 && indexes[i] - 1 < decodedMessages.Count)
                {
                    Console.WriteLine("{0} - {1}", decodedNames[i], decodedMessages[indexes[i] - 1]);
                }
            }

        }
    }
}
