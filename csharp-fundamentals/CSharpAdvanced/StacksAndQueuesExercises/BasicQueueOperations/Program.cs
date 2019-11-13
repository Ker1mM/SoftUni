using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicQueueOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputParameters = Console.ReadLine();

            int[] tokens = inputParameters.Split(" ").Select(int.Parse).ToArray();
            int elementsToQueue = tokens[0];
            int elemetsToDequeue = tokens[1];
            int elementToFind = tokens[2];

            if (elementsToQueue == 0 || elementsToQueue <= elemetsToDequeue)
            {
                Console.WriteLine("0");
            }
            else
            {
                string elements = Console.ReadLine();

                Queue<int> queue = new Queue<int>(elements.Split(" ").Select(int.Parse));

                while (elemetsToDequeue > 0)
                {
                    queue.Dequeue();
                    elemetsToDequeue--;
                }

                if (queue.Contains(elementToFind))
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine(queue.ToArray().Min());
                }
            }
        }
    }
}
