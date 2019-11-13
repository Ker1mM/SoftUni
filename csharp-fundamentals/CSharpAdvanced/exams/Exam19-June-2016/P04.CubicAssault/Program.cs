using System;
using System.Collections.Generic;
using System.Linq;

namespace P04.CubicAssault
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;

            Dictionary<string, Region> regions = new Dictionary<string, Region>();
            while ((input = Console.ReadLine()) != "Count em all")
            {
                string[] tokens = input.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                string type = tokens[1];
                int count = int.Parse(tokens[2]);

                if (!regions.ContainsKey(name))
                {
                    Region tempReg = new Region();
                    tempReg.Name = name;
                    regions.Add(name, tempReg);
                }

                switch (type)
                {
                    case "Black":
                        regions[name].BlackMeteors += count;
                        break;
                    case "Red":
                        regions[name].RedMeteors += count;
                        break;
                    case "Green":
                        regions[name].GreenMeteors += count;
                        break;
                    default:
                        break;
                }
            }

            regions = regions
                .OrderByDescending(x => x.Value.BlackMeteors)
                .ThenBy(x => x.Key.Length)
                .ThenBy(x => x.Key)
                .ToList()
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var item in regions)
            {
                Console.WriteLine(item.Key);
                PrintRegionMeteors(item.Value);
            }
        }



        public class Region
        {
            public string Name { get; set; }
            private long greenMeteors { get; set; }
            private long redMeteors { get; set; }
            private long blackMeteors { get; set; }

            public Region()
            {
                this.greenMeteors = 0;
                this.redMeteors = 0;
                this.blackMeteors = 0;
            }


            public long GreenMeteors
            {
                get
                {
                    return greenMeteors;
                }
                set
                {
                    this.greenMeteors = value;
                    if (this.greenMeteors >= 1_000_000)
                    {
                        this.RedMeteors += this.greenMeteors / 1_000_000;
                        this.greenMeteors = this.greenMeteors % 1_000_000;
                    }
                }
            }

            public long RedMeteors
            {
                get
                {
                    return redMeteors;
                }
                set
                {
                    this.redMeteors = value;
                    if (this.redMeteors >= 1_000_000)
                    {
                        this.BlackMeteors += this.redMeteors / 1_000_000;
                        this.redMeteors = this.redMeteors % 1_000_000;
                    }
                }
            }

            public long BlackMeteors
            {
                get
                {
                    return blackMeteors;
                }
                set
                {
                    this.blackMeteors = value;
                }
            }
        }

        public static void PrintRegionMeteors(Region region)
        {
            Dictionary<string, long> meteors = new Dictionary<string, long>();
            meteors.Add("Green", region.GreenMeteors);
            meteors.Add("Red", region.RedMeteors);
            meteors.Add("Black", region.BlackMeteors);

            meteors = meteors
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToList()
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var item in meteors)
            {
                Console.WriteLine("-> {0} : {1}", item.Key, item.Value);
            }
        }
    }
}
