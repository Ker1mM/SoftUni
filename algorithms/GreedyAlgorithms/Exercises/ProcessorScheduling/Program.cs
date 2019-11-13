using System;
using System.Linq;

namespace ProcessorScheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            int taskCount = int.Parse(Console.ReadLine().Split()[1]);

            var tasks = new Task[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                var input = Console.ReadLine().Split();
                int value = int.Parse(input[0]);
                int deadline = int.Parse(input[2]);

                tasks[i] = new Task(value, deadline, i + 1);
            }

            var maxSteps = tasks.Max(x => x.Deadline);

            var orderedTasks = tasks
                .OrderByDescending(x => x.Value)
                .Take(maxSteps)
                .OrderBy(x => x.Deadline);

            Console.WriteLine("Optimal schedule: {0}", string.Join(" -> ", orderedTasks));
            Console.WriteLine("Total value: {0}", orderedTasks.Sum(x => x.Value));
        }
    }

    internal class Task
    {
        public int Value { get; set; }
        public int Deadline { get; set; }
        public int Order { get; set; }

        public Task(int value, int deadline, int order)
        {
            this.Value = value;
            this.Deadline = deadline;
            this.Order = order;
        }

        public override string ToString()
        {
            return this.Order.ToString();
        }
    }
}
