using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingZones
{
    class Program
    {
        private static int zoneCount;
        private static Zone[] zones;
        private static List<ParkingSpot> freeSpots;
        private static int[] target;
        private static int timePerBlock;

        static void Main(string[] args)
        {
            ReadInput();

            foreach (var spot in freeSpots)
            {
                spot.GetZone(zones, target, timePerBlock);
            }

            var bestZone = zones
                .OrderBy(x => x.BestSpot.Price)
                .ThenBy(x => x.BestSpot.TimeToTarget)
                .FirstOrDefault();

            Console.WriteLine($"Zone Type: {bestZone.Name}; X: {bestZone.BestSpot.X}; Y: {bestZone.BestSpot.Y}; Price: {bestZone.BestSpot.Price:f2}");
        }

        private static void ReadInput()
        {
            zoneCount = int.Parse(Console.ReadLine());
            zones = new Zone[zoneCount];
            for (int i = 0; i < zoneCount; i++)
            {
                var inputArgs = Console.ReadLine().Replace(",", "").Replace(":", "").Split().ToArray();
                string name = inputArgs[0];
                int x = int.Parse(inputArgs[1]);
                int y = int.Parse(inputArgs[2]);
                int width = int.Parse(inputArgs[3]);
                int height = int.Parse(inputArgs[4]);
                decimal price = decimal.Parse(inputArgs[5]);
                var zone = new Zone(name, x, y, width, height, price);
                zones[i] = zone;
            }

            freeSpots = new List<ParkingSpot>();
            var spots = Console.ReadLine().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            foreach (var spot in spots)
            {
                var spotArgs = spot
                    .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                freeSpots.Add(new ParkingSpot(spotArgs[0], spotArgs[1]));
            }

            target = Console.ReadLine().Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            timePerBlock = int.Parse(Console.ReadLine());
        }
    }

    class ParkingSpot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string ZoneName { get; set; }
        public int BlocksToTarget { get; set; }
        public long TimeToTarget { get; set; }
        public decimal Price { get; set; }

        public ParkingSpot(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.ZoneName = null;
        }

        public void GetZone(Zone[] zones, int[] target, int timePerBlock)
        {
            var zone = zones.Where(z => z.X1 <= this.X && z.X2 >= this.X && z.Y1 <= this.Y && z.Y2 >= this.Y).FirstOrDefault();

            this.BlocksToTarget = 2 * (Math.Abs(X - target[0]) + Math.Abs(Y - target[1]) - 1);
            if (zone.BestSpot == null || zone.BestSpot.BlocksToTarget > this.BlocksToTarget)
            {
                this.ZoneName = zone.Name;
                this.TimeToTarget = this.BlocksToTarget * timePerBlock;
                this.Price = TimeToTarget / 60 * zone.Price;
                if (TimeToTarget % 60 != 0)
                {
                    this.Price += zone.Price;
                }

                zone.BestSpot = this;
            }
        }
    }

    class Zone
    {
        public string Name { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public decimal Price { get; set; }
        public ParkingSpot BestSpot { get; set; }

        public Zone(string name, int x, int y, int width, int height, decimal price)
        {
            this.Name = name;
            this.X1 = x;
            this.Y1 = y;
            this.X2 = x + width;
            this.Y2 = y + height;
            this.Price = price;
            this.BestSpot = null;
        }
    }
}
