using System;
using System.Collections.Generic;

namespace CalculateSequenceWithQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            long inputNumber = long.Parse(Console.ReadLine());

            Queue<long> queue = new Queue<long>();
            queue.Enqueue(inputNumber);

            int counter = 1;
            Console.Write(inputNumber + " ");
            while (counter++ < 17)
            {
                long currentNumber = queue.Dequeue();
                long number1 = currentNumber + 1;
                long number2 = 2 * currentNumber + 1;
                long number3 = currentNumber + 2;
                queue.Enqueue(number1);
                queue.Enqueue(number2);
                queue.Enqueue(number3);

                Console.Write(number1 + " ");
                Console.Write(number2 + " ");
                Console.Write(number3 + " ");

            }
            Console.Write((queue.Dequeue() + 1));
            Console.WriteLine();
        }
    }
}
