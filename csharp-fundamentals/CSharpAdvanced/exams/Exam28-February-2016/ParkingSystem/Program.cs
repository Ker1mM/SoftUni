using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimensions = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Dictionary<int, List<int>> parkingLot = new Dictionary<int, List<int>>();

            string input;
            while ((input = Console.ReadLine()) != "stop")
            {
                int[] tokens = input.Split().Select(int.Parse).ToArray();
                int entryRow = tokens[0];
                int spotRow = tokens[1];
                int spotCol = tokens[2];

                if (parkingLot.ContainsKey(spotRow) == false)
                {
                    parkingLot.Add(spotRow, new List<int>());
                    parkingLot[spotRow].Add(spotCol);
                }
                else
                {
                    if (parkingLot[spotRow].Count() == dimensions[1] - 1)
                    {
                        Console.WriteLine("Row {0} full", spotRow);
                        continue;
                    }
                    else
                    {
                        for (int i = 0; i < dimensions[1]; i++)
                        {
                            if ((spotCol - i > 0 && spotCol - i < dimensions[1]) && parkingLot[spotRow].Contains(spotCol - i) == false)
                            {
                                spotCol -= i;
                                parkingLot[spotRow].Add(spotCol);
                                break;

                            }
                            else if ((spotCol + i > 0 && spotCol + i < dimensions[1]) && parkingLot[spotRow].Contains(spotCol + i) == false)
                            {
                                spotCol += i;
                                parkingLot[spotRow].Add(spotCol);
                                break;
                            }
                        }
                    }
                }
                int totalDistance = Math.Abs(entryRow - spotRow);
                totalDistance += spotCol + 1;

                Console.WriteLine(totalDistance);
            }
        }
    }
}
