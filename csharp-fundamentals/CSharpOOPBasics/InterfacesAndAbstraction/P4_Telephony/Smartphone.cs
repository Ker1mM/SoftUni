using System;

namespace P4_Telephony
{
    public class Smartphone : ICall, IBrowse
    {
        public string Model { get; private set; }
        public Smartphone(string model)
        {
            Model = model;
        }

        public void Call(string number)
        {
            Predicate<char> isDigit = x => char.IsDigit(x);
            if (!IsCorrect(number, isDigit))
            {
                throw new ArgumentException("Invalid number!");
            }

            Console.WriteLine($"Calling... {number}");
        }

        public void Browse(string url)
        {
            Predicate<char> isNotDigit = x => !char.IsDigit(x);
            if (!IsCorrect(url, isNotDigit))
            {
                throw new ArgumentException("Invalid URL!");
            }

            Console.WriteLine($"Browsing: {url}!");
        }

        private bool IsCorrect(string input, Predicate<char> condition)
        {
            bool result = true;
            foreach (var symbol in input)
            {
                if (!condition(symbol))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
