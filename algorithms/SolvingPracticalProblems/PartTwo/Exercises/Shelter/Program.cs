using System;
using System.Collections.Generic;
using System.Linq;

namespace Shelter
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputArgs = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int soldierCount = inputArgs[0];
            int shelterCount = inputArgs[1];
            int capacity = inputArgs[2];


            var soldiers = new Soldier[soldierCount];
            for (int i = 0; i < soldierCount; i++)
            {
                inputArgs = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var soldier = new Soldier(inputArgs[0], inputArgs[1]);
                soldiers[i] = soldier;
            }

            var shelters = new Shelter[shelterCount];
            for (int i = 0; i < shelterCount; i++)
            {
                inputArgs = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var shelter = new Shelter(inputArgs[0], inputArgs[1], capacity);
                shelters[i] = shelter;
            }

            var points = new List<IPoint>();
            points.AddRange(soldiers);
            points.AddRange(shelters);

            points = points.OrderBy(x => x.X).ToList();

            var stackSoldiers = new Stack<Soldier>();
            var stackShelters = new Stack<Shelter>();
            double maxDistance = 0;
            foreach (var point in points)
            {
                if (point.GetType().Name == "Soldier")
                {
                    stackSoldiers.Push((Soldier)point);
                }
                else
                {
                    stackShelters.Push((Shelter)point);
                }

                while (stackSoldiers.Count > 0 && stackShelters.Count > 0)
                {
                    var shelter = stackShelters.Pop();
                    while (shelter.Capcity > 0 && stackSoldiers.Count > 0)
                    {
                        var soldier = stackSoldiers.Pop();
                        var distance = soldier.DistanceToShelter(shelter);
                        if (distance > maxDistance)
                        {
                            maxDistance = distance;
                        }

                        shelter.Capcity--;
                    }
                    if (shelter.Capcity > 0)
                    {
                        stackShelters.Push(shelter);
                    }
                }
            }

            Console.WriteLine($"{maxDistance:f6}");
        }
    }

    interface IPoint
    {
        int X { get; set; }
        int Y { get; set; }
    }

    class Shelter : IPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Capcity { get; set; }

        public Shelter(int x, int y, int cap)
        {
            this.X = x;
            this.Y = y;
            this.Capcity = cap;
        }
    }

    class Soldier : IPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Soldier(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public double DistanceToShelter(Shelter shelter)
        {
            var x = Math.Abs(this.X - shelter.X);
            x *= x;

            var y = Math.Abs(this.Y - shelter.Y);
            y *= y;

            var result = Math.Sqrt(x + y);
            return result;
        }
    }
}
