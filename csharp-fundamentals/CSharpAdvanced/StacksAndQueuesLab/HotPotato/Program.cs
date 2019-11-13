using System;
using System.Collections.Generic;

namespace HotPotato
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int count = int.Parse(Console.ReadLine());

            Queue<string> players = new Queue<string>(input.Split(" "));

            int counter = 1;
            while(players.Count > 1)
            {
                if(counter == count)
                {
                    Console.WriteLine("Removed "+players.Dequeue());
                    counter = 1;
                }
                else
                {
                    players.Enqueue(players.Dequeue());
                    counter ++;
                }
            }
            Console.WriteLine("Last is " + players.Dequeue());
        }
    }
}

