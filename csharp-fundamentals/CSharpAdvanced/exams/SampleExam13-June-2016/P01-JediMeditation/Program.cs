using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.JediMeditation
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputCount = int.Parse(Console.ReadLine());

            List<string> jedis = new List<string>();
            while (inputCount-- > 0)
            {
                string input = Console.ReadLine();
                jedis.AddRange(input.Split());
            }

            Func<string, int> Comparator = x =>
            {
                if (x[0] == 'm')
                {
                    return -1;
                }
                else if (x[0] == 'p')
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            };

            jedis = jedis
                .OrderBy(x => Comparator(x)).ToList();

            var ourJedis = jedis.Where(x => x[0] == 's' || x[0] == 't').ToList();
            jedis.RemoveAll(x => x[0] == 's' || x[0] == 't');
            if (jedis.RemoveAll(x => x[0] == 'y') == 0)
            {
                jedis.InsertRange(0, ourJedis);
            }
            else
            {
                int index = jedis.FindIndex(x => x[0] == 'p');
                jedis.InsertRange(index, ourJedis);
            }
            Console.WriteLine(String.Join(" ", jedis));
        }
    }
}
