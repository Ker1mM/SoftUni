using System;
using System.Collections;

namespace CustomStack
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Stack test = new Stack();
            StackOfStrings testt = new StackOfStrings();
            testt.Push("PUSHED");
            Console.WriteLine(testt.Peek());
            Console.WriteLine(testt.IsEmpty());
            Console.WriteLine(testt.Pop());
            Console.WriteLine(testt.IsEmpty());
        }
    }
}
