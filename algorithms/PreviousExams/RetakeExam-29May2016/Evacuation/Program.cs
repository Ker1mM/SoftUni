using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Evacuation
{
    class Program
    {
        static Dictionary<int, Dictionary<int, TimeSpan>> building;
        static TimeSpan?[] bestTimes;

        static void BFS(int index)
        {
            var queue = new Queue<int>();
            queue.Enqueue(index);

            while (queue.Count > 0)
            {
                var next = queue.Dequeue();

                foreach (var child in building[next])
                {

                    var newTime = bestTimes[next] + child.Value;
                    if (bestTimes[child.Key] == null || newTime < bestTimes[child.Key])
                    {
                        bestTimes[child.Key] = newTime;
                        queue.Enqueue(child.Key);
                    }

                }
            }
        }

        static void Main(string[] args)
        {
            int roomCount = int.Parse(Console.ReadLine());
            var exitRooms = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var connectionCount = int.Parse(Console.ReadLine());

            building = new Dictionary<int, Dictionary<int, TimeSpan>>();
            bestTimes = new TimeSpan?[roomCount];

            for (int i = 0; i < roomCount; i++)
            {
                building.Add(i, new Dictionary<int, TimeSpan>());
            }


            for (int i = 0; i < connectionCount; i++)
            {
                var inputArgs = Console.ReadLine().Split().ToArray();

                int start = int.Parse(inputArgs[0]);
                int end = int.Parse(inputArgs[1]);
                var time = TimeSpan.ParseExact(inputArgs[2], "mm\\:ss", CultureInfo.InvariantCulture);

                if (!building[start].ContainsKey(end))
                {
                    building[start].Add(end, time);
                }
                else
                {
                    building[start][end] = time;
                }

                if (!building[end].ContainsKey(start))
                {
                    building[end].Add(start, time);
                }
                else
                {
                    building[end][start] = time;
                }
            }

            var evacTime = TimeSpan.ParseExact(Console.ReadLine(), "mm\\:ss", CultureInfo.InvariantCulture);

            foreach (var exit in exitRooms)
            {
                bestTimes[exit] = new TimeSpan(0);
                BFS(exit);
            }

            var invalidRooms = new List<string>();
            TimeSpan? maxEvacTime = new TimeSpan(-1);
            int maxRoomId = 0;

            for (int i = 0; i < roomCount; i++)
            {
                if (exitRooms.Contains(i))
                {
                    continue;
                }

                if (bestTimes[i] == null)
                {
                    invalidRooms.Add($"{i} (unreachable)");
                }
                else if (bestTimes[i] > evacTime)
                {
                    invalidRooms.Add($"{i} ({bestTimes[i]})");
                }
                else if (bestTimes[i] > maxEvacTime)
                {
                    maxEvacTime = bestTimes[i];
                    maxRoomId = i;
                }
            }

            if (invalidRooms.Count > 0)
            {
                Console.WriteLine("Unsafe");
                Console.WriteLine(string.Join(", ", invalidRooms));
            }
            else
            {
                Console.WriteLine("Safe");
                Console.WriteLine($"{maxRoomId} ({maxEvacTime})");
            }
        }
    }
}
