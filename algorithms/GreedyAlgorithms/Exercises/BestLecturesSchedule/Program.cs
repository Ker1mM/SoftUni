using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestLecturesSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            int lectureCount = int.Parse(Console.ReadLine().Split()[1]);

            var lectures = new Dictionary<string, int[]>();

            for (int i = 0; i < lectureCount; i++)
            {
                string[] inputArgs = Console.ReadLine().Split(':');
                string[] timeArgs = inputArgs[1].Trim().Split('-');

                string name = inputArgs[0];
                int start = int.Parse(timeArgs[0].Trim());
                int finish = int.Parse(timeArgs[1].Trim());

                lectures.Add(name, new int[] { start, finish });
            }

            lectures = lectures
                .OrderBy(x => x.Value[1])
                .ToDictionary(x => x.Key, x => x.Value);

            var bestLectures = new List<string>();
            var sb = new StringBuilder();

            while (lectures.Count > 0)
            {
                var lecture = lectures.FirstOrDefault();
                bestLectures.Add($"{lecture.Value[0]}-{lecture.Value[1]} -> {lecture.Key}");

                lectures = lectures
                    .Where(x => x.Value[0] >= lecture.Value[1])
                    .ToDictionary(x => x.Key, x => x.Value);
            }

            Console.WriteLine($"Lectures ({bestLectures.Count}):");
            Console.WriteLine(string.Join(Environment.NewLine, bestLectures));
        }
    }
}
