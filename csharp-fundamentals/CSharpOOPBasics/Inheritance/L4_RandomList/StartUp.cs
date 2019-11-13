using System;
using System.Collections.Generic;

namespace CustomRandomList
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var strings = new RandomList();
            strings.Add("One");
            strings.Add("Two");
            strings.Add("Three");
            strings.Add("Four");

            Console.WriteLine(strings.RandomString());

            List<int> ints = new List<int>();
            ints.Add(1);
            ints.Add(2);
            ints.Add(3);
            ints.Add(4);
        }
    }
}
