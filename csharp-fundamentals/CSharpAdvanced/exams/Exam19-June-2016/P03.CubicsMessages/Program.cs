using System;
using System.Text;
using System.Text.RegularExpressions;

namespace P03.CubicsMessages
{
    class Program
    {
        static void Main(string[] args)
        {

            string message;
            while ((message = Console.ReadLine()) != "Over!")
            {
                int length = int.Parse(Console.ReadLine());
                string messagePattern = @"(?<=^[0-9]*)([a-zA-Z]{" + length + "})(?=[^a-zA-Z]*$)";
                Regex mRgx = new Regex(messagePattern);

                Match matchedMessage = mRgx.Match(message);
                if (matchedMessage.Success)
                {
                    string code = GetCode(message.Replace(matchedMessage.ToString(), ""), matchedMessage.ToString());
                    Console.WriteLine("{0} == {1}", matchedMessage.ToString(), code);

                }
            }
        }

        public static string GetCode(string message, string matchedMessage)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char element in message)
            {
                if (Char.IsDigit(element))
                {
                    int index = int.Parse(element.ToString());
                    if (index >= 0 && index < matchedMessage.Length)
                    {
                        sb.Append(matchedMessage[index]);
                    }
                    else
                    {
                        sb.Append(" ");
                    }

                }
            }
            return sb.ToString();
        }
    }
}
