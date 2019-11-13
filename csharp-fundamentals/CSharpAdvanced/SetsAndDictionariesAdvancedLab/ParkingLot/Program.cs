using System;
using System.Collections.Generic;

namespace ParkingLot
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> parking = new HashSet<string>();
            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] tokens = input.Split(", ");
                if (tokens[0].Equals("IN"))
                {
                    parking.Add(tokens[1]);
                }
                else if (tokens[0].Equals("OUT"))
                {
                    parking.Remove(tokens[1]);
                }
            }

            if (parking.Count == 0)
            {
                Console.WriteLine("Parking Lot is Empty");
            }
            else
            {
                Console.WriteLine(String.Join("\n", parking));
            }
        }
    }
}
