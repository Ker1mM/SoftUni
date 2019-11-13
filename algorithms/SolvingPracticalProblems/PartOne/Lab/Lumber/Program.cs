using System;
using System.Collections.Generic;
using System.Linq;

namespace Lumber
{
    class Program
    {
        private static List<short>[] logGraph;
        private static short[] components;
        private static bool[] visited;
        private static List<Log> logs;

        static void Main(string[] args)
        {
            var inputArgs = Console.ReadLine().Split().Select(short.Parse).ToArray();
            int logCount = inputArgs[0];
            int queryCount = inputArgs[1];
            var logs = new List<Log>();
            logGraph = new List<short>[logCount + 1];

            for (short i = 1; i <= logCount; i++)
            {
                logGraph[i] = new List<short>();
                inputArgs = Console.ReadLine().Split().Select(short.Parse).ToArray();
                var nextLog = new Log(inputArgs[0], inputArgs[1], inputArgs[2], inputArgs[3], i);

                logGraph[nextLog.Number].Add(nextLog.Number);
                foreach (var oldLog in logs)
                {
                    if (nextLog.Intersects(oldLog))
                    {
                        logGraph[nextLog.Number].Add(oldLog.Number);
                        logGraph[oldLog.Number].Add(nextLog.Number);
                    }
                }

                logs.Add(nextLog);
            }

            visited = new bool[logCount + 1];
            components = new short[logCount + 1];
            short componentCode = 1;
            for (int i = 1; i <= logCount; i++)
            {
                if (visited[i] == false)
                {
                    DFS(i, componentCode);
                    componentCode++;
                }
            }

            for (int i = 0; i < queryCount; i++)
            {
                var query = Console.ReadLine().Split().Select(int.Parse).ToArray();

                if (components[query[0]] == components[query[1]])
                {
                    Console.WriteLine("YES");
                }
                else
                {
                    Console.WriteLine("NO");
                }
            }
        }

        public static void DFS(int start, short componentNumber)
        {
            visited[start] = true;
            components[start] = componentNumber;
            foreach (var child in logGraph[start])
            {
                if (visited[child] == false)
                {
                    DFS(child, componentNumber);
                }
            }
        }
    }

    class Log
    {
        public short LeftX { get; set; }
        public short RightX { get; set; }
        public short TopY { get; set; }
        public short BottomY { get; set; }
        public short Number { get; set; }

        public Log(short lX, short tY, short rX, short bY, short number)
        {
            this.LeftX = lX;
            this.RightX = rX;
            this.TopY = tY;
            this.BottomY = bY;
            this.Number = number;
        }

        public bool Intersects(Log log)
        {
            var xIntersection = this.LeftX <= log.RightX && log.LeftX <= this.RightX;
            var yIntersection = this.BottomY <= log.TopY && log.BottomY <= this.TopY;

            return xIntersection && yIntersection;
        }
    }
}
